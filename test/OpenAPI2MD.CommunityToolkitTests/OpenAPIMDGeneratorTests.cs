using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenAPI2MD.CommunityToolkit;
using OpenAPI2MD.CommunityToolkit.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAPI2MD.CommunityToolkit.Tests
{
    [TestClass()]
    public class OpenAPIMDGeneratorTests
    {
        [TestMethod()]
        public void ReadYamlTest()
        {
            //Arrange 
            var expected = new OpenApimdGenerator().ReadYaml();

            //Act
            var actual = "";

            //Assert
            Assert.AreEqual(expected, actual, "You are wrong!!!");

        }
    }
}