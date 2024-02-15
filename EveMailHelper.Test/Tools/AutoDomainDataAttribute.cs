using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

using EveMailHelper.DataModels.Sde;

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

              var fixture = new Fixture();
              fixture.Customize<EveType>(c => c.Without(e => e.IndustryActivities)
                                    .Without(e => e.MarketGroup)
                                    .Without(e => e.Group)
                                    .Without(e => e.Icon)
                                    .Without(e => e.IndustryBlueprint)
                                    .Without(e => e.Race)
                                    .With(e => e.TypeName)
                                    .With(e => e.Volume));
              return fixture.Customize(new AutoMoqCustomization()); 
          })
        {
        }
    }
}
