using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EVEStandard.Models;

namespace EveMailHelper.ServiceLayer.Models
{
    public class BlueprintComponentIterator : IEnumerator<BlueprintComponent>
    {
        protected class NavTree
        {
            public NavTree(BlueprintComponent component, NavTree? prevLevel = null, NavTree? nextLevel = null)
            {
                item = component;
                this.prevLevel = prevLevel;
                this.nextLevel = nextLevel;
            }

            public BlueprintComponent item;
            public int SubComponentIndex = -1;

            public NavTree? prevLevel;
            public NavTree? nextLevel;
        }

        private NavTree? _navTree;

        private BlueprintComponent _rootComponent;
        private BlueprintComponent _currentComponent;

        private BlueprintComponent _previousComponent;

        public BlueprintComponentIterator(BlueprintComponent root) 
        {
            _rootComponent = root;
            _currentComponent = _rootComponent;
            _previousComponent = _rootComponent;

            _navTree = new NavTree(root);
        }

        public BlueprintComponent Current
        {
            get { return _currentComponent; }
        }

        object IEnumerator.Current
        { get { return _currentComponent; } }

        public void Dispose()
        {
            //throw new NotImplementedException();
            // consumed memory is 'thrown away' on the fly
        }

        public bool MoveNext()
        {
            bool wasRefreshed = false;
            _navTree = TraverseTree(_navTree, out wasRefreshed);
            return wasRefreshed;
        }

        protected NavTree? TraverseTree(NavTree? nav, out bool currentWasRefreshed)
        {
            currentWasRefreshed = false;
            bool wasRefreshedInNextLevel = false;
            if (nav == null)
                return null;
            if (nav.nextLevel != null)
            {
                nav.nextLevel = TraverseTree(nav.nextLevel, out wasRefreshedInNextLevel);
                currentWasRefreshed = wasRefreshedInNextLevel;
            }
            if (nav.nextLevel == null)
            {
                if (nav.SubComponentIndex == -1) { 
                    // ich bins
                    _currentComponent = nav.item;
                    currentWasRefreshed = true;
                    nav.SubComponentIndex++;
                }
                else // oder meine subkomponente wars
                {
                    if (nav.item.SubComponents.Count == 0 ||
                        nav.item.SubComponents.Count <= nav.SubComponentIndex)
                    {
                        // keine subkomponenten mehr
                        return null;
                    }
                    else
                    {
                        nav.nextLevel = new NavTree(nav.item.SubComponents[nav.SubComponentIndex], nav);
                        nav.SubComponentIndex++;
                        nav.nextLevel = TraverseTree(nav.nextLevel, out wasRefreshedInNextLevel);
                        currentWasRefreshed = wasRefreshedInNextLevel;
                    }
                }
            }
            
            return nav;
        }

        protected NavTree? TraverseTreeNoTop(NavTree? nav, out bool currentWasRefreshed)
        {
            currentWasRefreshed = false;
            bool wasRefreshedInNextLevel = false;
            if (nav == null)
                return null;
            if (nav.nextLevel != null)
            {
                nav.nextLevel = TraverseTree(nav.nextLevel, out wasRefreshedInNextLevel);
            }
            if (!wasRefreshedInNextLevel)
            {
                if (nav.item.SubComponents.Count == 0 || nav.item.SubComponents.Count -1 <= nav.SubComponentIndex)
                {
                    // dann eine ebene hoch
                    if (nav.prevLevel != null)
                        return null;
                }
                else
                {
                    _currentComponent = nav.item.SubComponents[nav.SubComponentIndex];
                    nav.SubComponentIndex++;
                    nav.nextLevel = new NavTree(_currentComponent, nav);
                    currentWasRefreshed = true;
                } 
            }
            return nav;
        }
        public void Reset()
        {
            _currentComponent = _rootComponent;
            _navTree = new NavTree(_rootComponent);
        }
    }
}
