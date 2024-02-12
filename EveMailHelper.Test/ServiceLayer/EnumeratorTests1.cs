using EveMailHelper.ChatLogParser;
using EveMailHelper.ServiceLayer.Models;

using EVEStandard.Models;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

using Shouldly;

using System.IO;
using System.Linq;
using System.Collections.Generic;

using Xunit;

namespace EveMailHelper.Test.ServiceLayer
{
    public partial class EnumeratorTests1
    {
        public EnumeratorTests1()
        {

        }

        [Fact]
        
        public void Test1()
        {
            var sut = new BlueprintComponentTree()
            {
                Name = "TopLevel",
                SubComponents = new[]
                {
                    new BlueprintComponentTree()
                    {
                        Name = "L1 item1",
                        SubComponents = new[]
                        {
                            new BlueprintComponentTree()
                            {
                                Name = "L2-1 item1"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-1 item2"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-1 item3"
                            }
                        }
                    },
                    new BlueprintComponentTree()
                    {
                        Name = "L1 item2",
                        SubComponents = new[]
                        {
                            new BlueprintComponentTree()
                            {
                                Name = "L2-2 item1"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-2 item2"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-2 item3"
                            }
                        }
                    }
                }
            };

            int i = 0;
            foreach (var Item in sut)
            {
                if (i == 0) Item.ShouldBeEquivalentTo(sut);
                if (i == 1) Item.ShouldBeEquivalentTo(sut.SubComponents[0]);
                if (i == 2) Item.ShouldBeEquivalentTo(sut.SubComponents[0].SubComponents[0]);
                if (i == 3) Item.ShouldBeEquivalentTo(sut.SubComponents[0].SubComponents[1]);
                if (i == 4) Item.ShouldBeEquivalentTo(sut.SubComponents[0].SubComponents[2]);
                if (i == 5) Item.ShouldBeEquivalentTo(sut.SubComponents[1]);
                if (i == 6) Item.ShouldBeEquivalentTo(sut.SubComponents[1].SubComponents[0]);
                if (i == 7) Item.ShouldBeEquivalentTo(sut.SubComponents[1].SubComponents[1]);
                if (i == 8) Item.ShouldBeEquivalentTo(sut.SubComponents[1].SubComponents[2]);
                i++;
            }

            //sut.ParseFile(fileName);
        }

        
        [Fact]
       
        public void TestCount()
        {
            BlueprintComponentTree sut = new BlueprintComponentTree()
            {
                Name = "TopLevel",
                SubComponents = new[]
                {
                    new BlueprintComponentTree()
                    {
                        Name = "L1 item1",
                        SubComponents = new[]
                        {
                            new BlueprintComponentTree()
                            {
                                Name = "L2-1 item1"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-1 item2"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-1 item3"
                            }
                        }
                    },
                    new BlueprintComponentTree()
                    {
                        Name = "L1 item2",
                        SubComponents = new[]
                        {
                            new BlueprintComponentTree()
                            {
                                Name = "L2-2 item1"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-2 item2"
                            },
                            new BlueprintComponentTree()
                            {
                                Name = "L2-2 item3"
                            }
                        }
                    }
                }
            };

            var result = sut.Count();
            result.ShouldBeEquivalentTo(9);
        }
    }
}
