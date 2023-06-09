﻿using System.Net.Http;
using WebApplicationOld.Controllers;
using WebApplicationOld.Models;
using WebApplicationOld.Contexto;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace DBContextTeste
{
    [TestFixture]
    public class FabricanteTeste
    {
        [SetUp]
        public void Init()
        {

        }

        private void PopulateDB()
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
                dbContext.Fabricantes.Add(new Fabricante { nome = "Dummy1" });
                dbContext.Fabricantes.Add(new Fabricante { nome = "Dummy2" });
                dbContext.Fabricantes.Add(new Fabricante { nome = "Dummy3" });
                dbContext.SaveChanges();
            }
        }

        private void ClearDB()
        {
            var db = new EFContext();
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE produto RESTART IDENTITY");
            db.SaveChanges();
        }

        [Test]
        public void Get_IsNull_NoContent()
        {
            var controller = new FabricanteController();
            var result = controller.Get() as HttpResponseMessage;
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);
        }

        [Test]
        public void Get_HasContent_ReturnContent()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Get() as HttpResponseMessage;
            result.StatusCode.Should().HaveValue(200);
            result.Content.Should().NotBeNull();
            string jsonContent = result.Content.ReadAsStringAsync().Result;
            List<Fabricante> fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.id.Equals(1) && x.nome.Equals("Dummy1")).Should().NotBeNull();
            fabs.FirstOrDefault(x => x.id.Equals(2) && x.nome.Equals("Dummy2")).Should().NotBeNull();
            fabs.FirstOrDefault(x => x.id.Equals(3) && x.nome.Equals("Dummy3")).Should().NotBeNull();
            ClearDB();
        }

        [Test]
        public void Post_FabAdded_OK()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante() { nome = "Dummy4"};
            var result = controller.Post(dummy) as HttpResponseMessage;
            result.StatusCode.Should().HaveValue(200);
            var result2 = controller.Get();
            result2.StatusCode.Should().HaveValue(200);
            result2.Content.Should().NotBeNull();
            string jsonContent = result2.Content.ReadAsStringAsync().Result;
            List<Fabricante> fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.nome.Equals("Dummy4")).Should().NotBeNull();
            ClearDB();
        }
        [Test]
        public void Post_FabricanteNull_NoContent()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Post(null) as HttpResponseMessage;
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);
            ClearDB();
        }
        [Test]
        public void Post_InfoIncompleta_BadRequest()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante();
            var result = controller.Post(dummy) as HttpResponseMessage;
            //Assert.AreEqual(400, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(400);
            ClearDB();
        }
        
        [Test]
        public void Put_FabAdded_OK()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante() { id = 3, nome = "Dummy0" };
            var result = controller.Put(dummy) as HttpResponseMessage;
            //Assert.AreEqual(200, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(200);
            var result2 = controller.Get();
            result2.StatusCode.Should().HaveValue(200);
            result2.Content.Should().NotBeNull();
            string jsonContent = result2.Content.ReadAsStringAsync().Result;
            List<Fabricante> fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.id.Equals(3) && x.nome.Equals("Dummy0")).Should().NotBeNull();
            ClearDB();
        }

        [Test]
        public void Put_FabricanteNull_NoContent()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Put(null) as HttpResponseMessage;
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);
            ClearDB();
        }

        [Test]
        public void Put_InfoIncompleta_BadRequest()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante();
            var result = controller.Put(dummy) as HttpResponseMessage;
            //Assert.AreEqual(400, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(400);
            ClearDB();
        }

        [Test]
        public void Put_NoExist_NotFound()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante() { id = 5, nome = "Dummy0" };
            var result = controller.Put(dummy) as HttpResponseMessage;
            //Assert.AreEqual(404, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(404);
            ClearDB();
        }

        [Test]
        public void Delete_IsDeleted_OK()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Delete(new Fabricante(){ id = 3}) as HttpResponseMessage;
            //Assert.AreEqual(200, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(200);
            var result2 = controller.Get();
            result2.StatusCode.Should().HaveValue(200);
            result2.Content.Should().NotBeNull();
            string jsonContent = result2.Content.ReadAsStringAsync().Result;
            List<Fabricante> fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.id.Equals(3)).Should().BeNull();
            ClearDB();
        }

        [Test]
        public void Delete_NoExist_NotFound()
        {
            var controller = new FabricanteController();
            
            var result = controller.Delete(new Fabricante() { id = 5 });
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);

            PopulateDB();

            var result2 = controller.Delete(new Fabricante() { id = 6 });
            //Assert.AreEqual(404, (int)result2.StatusCode);
            result2.StatusCode.Should().HaveValue(404);
            ClearDB();
        }
    }
}
