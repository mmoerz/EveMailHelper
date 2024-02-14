using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace EveMailHelper.Test.Tools
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// see https://autofixture.github.io/docs/quick-start/#
        /// </summary>
        public AutoDomainDataAttribute()
          : base(() =>
          {
              // maybe add custom fixtures here?
              // https://blog.ploeh.dk/2009/09/22/CustomizingAType'sBuilderWithAutoFixture/
              return
              new Fixture()
              .Customize(new AutoMoqCustomization()); 
          })
        {
        }
    }
}
