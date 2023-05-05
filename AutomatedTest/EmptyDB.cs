using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using FluentAssertions;
using System.Net;
using WebApplicationOld.Models;
using System.Threading.Tasks;

namespace AutomatedTest
{
    [TestClass]
    public class EmptyDB
    {
        [TestMethod]
        public void Get()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            var response = rClient.Get(request);
            response.StatusCode.Should().HaveValue(204);
            response.Content.Should().Be("");
        }

        [TestMethod]
        public void Post_Null()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddParameter("application/json", null, ParameterType.RequestBody);
            var response = rClient.Post(request);
            response.StatusCode.Should().HaveValue(204);
        }
        [TestMethod]
        public void Post_Incomplete()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddJsonBody(new { id = "", nome = "" });
            var response = rClient.Post(request);
            response.StatusCode.Should().HaveValue(400);
        }
        [TestMethod]
        public void Put_Null()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");

            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            var response = rClient.Put(request);
            response.StatusCode.Should().HaveValue(204);
        }
        [TestMethod]
        public void Put_Incomplete()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddBody(new { id = "", nome = "" });
            var response = rClient.Put(request);
            response.StatusCode.Should().HaveValue(400);
        }
        [TestMethod]
        public void Delete_NotFound()
        {
            var rClient = new RestClient("http://localhost:52777/");
            var request = new RestRequest("api/Fabricante");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Connection", "close");
            request.AddBody(new { id = 1 });
            var response = rClient.Delete<Fabricante>(request);
            response.StatusCode.Should().HaveValue(204);
        }
    }
}
