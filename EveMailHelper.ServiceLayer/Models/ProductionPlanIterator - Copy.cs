
using System.Collections;

namespace EveMailHelper.ServiceLayer.Models
{
    //public class BlueprintComponentTreeIterator : IEnumerator<BlueprintComponentTreeNode>
    //{
    //    protected class NavTree
    //    {
    //        public NavTree(BlueprintComponentTreeNode component, NavTree? prevLevel = null, NavTree? nextLevel = null)
    //        {
    //            item = component;
    //            this.prevLevel = prevLevel;
    //            this.nextLevel = nextLevel;
    //        }

    //        public BlueprintComponentTreeNode item;
    //        public int SubComponentIndex = -1;

    //        public NavTree? prevLevel;
    //        public NavTree? nextLevel;
    //    }

    //    private NavTree? _navTree;

    //    private BlueprintComponentTreeNode _rootComponent;
    //    private BlueprintComponentTreeNode _currentComponent;

    //    public BlueprintComponentTreeIterator(BlueprintComponentTreeNode root)
    //    {
    //        // root node is the top level blueprint itself
    //        _rootComponent = root;
    //        _currentComponent = _rootComponent;

    //        // initially ignores the root node in the enumeration
    //        _navTree = new NavTree(root)
    //        {
    //            SubComponentIndex = 0
    //        };
    //    }

    //    public BlueprintComponentTreeNode Current
    //    {
    //        get { return (BlueprintComponentTreeNode)_currentComponent; }
    //    }

    //    object IEnumerator.Current
    //    { get { return (BlueprintComponentTreeNode)_currentComponent; } }

    //    /// <summary>
    //    /// starts in the lowest level in the endnodes
    //    /// </summary>
    //    /// <returns></returns>
    //    public bool MoveNext()
    //    {
    //        bool wasRefreshed = false;
    //        _navTree = TraverseTree(_navTree, out wasRefreshed);
    //        return wasRefreshed;
    //    }

    //    private NavTree? TraverseTree(NavTree? nav, out bool wasRefreshed)
    //    {
    //        wasRefreshed = false;
    //        bool wasRefreshedInNextLevel = false;
    //        if (nav == null)
    //            return null;
    //        if (nav.nextLevel != null)
    //        {
    //            nav.nextLevel = TraverseTree(nav.nextLevel, out wasRefreshedInNextLevel);
    //            wasRefreshed = wasRefreshedInNextLevel;
    //        }
    //        if (nav.nextLevel == null)
    //        {
    //            if (nav.SubComponentIndex == -1)
    //            {
    //                // ich bins
    //                _currentComponent = nav.item;
    //                wasRefreshed = true;
    //                nav.SubComponentIndex++;
    //            }
    //            else // oder meine subkomponente wars
    //            {
    //                if (nav.item.SubComponents.Count == 0 ||
    //                    nav.item.SubComponents.Count <= nav.SubComponentIndex)
    //                {
    //                    // keine subkomponenten mehr
    //                    return null;
    //                }
    //                else
    //                {
    //                    nav.nextLevel = new NavTree(nav.item.SubComponents[nav.SubComponentIndex], nav);
    //                    nav.SubComponentIndex++;
    //                    nav.nextLevel = TraverseTree(nav.nextLevel, out wasRefreshedInNextLevel);
    //                    wasRefreshed = wasRefreshedInNextLevel;
    //                }
    //            }
    //        }

    //        return nav;
    //    }


    //    public void Reset()
    //    {
    //        _currentComponent = _rootComponent;
    //        _navTree = new NavTree(_rootComponent)
    //        {
    //            SubComponentIndex = 0
    //        };
    //    }

    //    public void Dispose()
    //    {
    //        // not necessary
    //    }
    //}
}