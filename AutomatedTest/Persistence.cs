using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedTest
{
    [TestClass]
    public class Persistence
    {
        [TestMethod]
        public void Post_FabbAdded()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddJsonBody(new { nome = "Positivo" });
            var response = rClient.Post(request);
            response.StatusCode.Should().HaveValue(200);
        }
        [TestMethod]
        public void Get_HasContent()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddJsonBody(new { nome = "AIWA" });
            var response = rClient.Post(request);
            response.StatusCode.Should().HaveValue(200);
            var request2 = new RestRequest("api/Fabricante");
            request2.RequestFormat = DataFormat.Json;
            request2.AddHeader("Accept", "*/*");
            request2.AddHeader("Connection", "close");
            var response2 = rClient.Get(request2);
            var ja = JArray.Parse(response2.Content);
            var fablist = new List<JObject>(JsonConvert.DeserializeObject<List<JObject>>(ja.ToString()));
            var fab = fablist.FirstOrDefault(x => x["nome"].ToString().Equals("AIWA"));
            response2.StatusCode.Should().HaveValue(200);
            fab["nome"].ToString().Should().Be("AIWA");
        }
        [TestMethod]
        public void Put_FabChange()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddJsonBody(new { id = "1", nome = "Foston" });
            var response = rClient.Put(request);
            response.StatusCode.Should().HaveValue(200);
            var request2 = new RestRequest("api/Fabricante");
            request2.RequestFormat = DataFormat.Json;
            request2.AddHeader("Accept", "*/*");
            request2.AddHeader("Connection", "close");
            var response2 = rClient.Get(request2);
            var ja = JArray.Parse(response2.Content);
            var fablist = new List<JObject>(JsonConvert.DeserializeObject<List<JObject>>(ja.ToString()));
            var fab = fablist.FirstOrDefault(x => x["nome"].ToString().Equals("Foston"));
            response2.StatusCode.Should().HaveValue(200);
            fab["nome"].ToString().Should().Be("Foston");
        }
        [TestMethod]
        public void Z_Delete()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddJsonBody(new { id = 1 });
            var response = rClient.Delete(request);
            response.StatusCode.Should().HaveValue(200);
            var request2 = new RestRequest("api/Fabricante");
            request2.RequestFormat = DataFormat.Json;
            request2.AddHeader("Accept", "*/*");
            request2.AddHeader("Connection", "close");
            var response2 = rClient.Get(request2);
            var ja = JArray.Parse(response2.Content);
            var fablist = new List<JObject>(JsonConvert.DeserializeObject<List<JObject>>(ja.ToString()));
            var fab = fablist.FirstOrDefault(x => (int)x["id"] == 1);
            //var pass = (int)fab["id"];
            response2.StatusCode.Should().HaveValue(200);
            fab.Should().BeNull();
        }

    }
}
