using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElasticSearchApplication.Core;
using Elasticsearch.Net;
using Nest;

namespace ElasticSearchApplication.Tests
{
    [TestClass]
    public class ElasticClientTester
    {
        [TestMethod]
        public void Get_New_Client_Returns_A_New_Client_When_Called()
        {
            var newClient = ElasticSearchClient.GetNewClient();
            Assert.IsNotNull(newClient);
            Assert.IsInstanceOfType(newClient, typeof(ElasticClient));
        }
    }
}
