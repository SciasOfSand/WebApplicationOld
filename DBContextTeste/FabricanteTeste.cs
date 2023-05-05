using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using WebApplicationOld.Controllers;
using WebApplicationOld.Models;
using WebApplicationOld;
using WebApplicationOld.Aplicacao;
using System.Runtime.Remoting.Contexts;
using WebApplicationOld.Contexto;
using System.Data.Entity;

namespace DBContextTeste
{
    [TestClass]
    public class FabricanteTeste
    {
        [TestInitialize]
        public void Init()
        {

        }

        private void PopulateDB()
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
                dbContext.Fabricantes.Add(new Fabricante { id = 1, nome = "Dummy1" });
                dbContext.Fabricantes.Add(new Fabricante { id = 2, nome = "Dummy2" });
                dbContext.Fabricantes.Add(new Fabricante { id = 3, nome = "Dummy3" });
                dbContext.SaveChanges();
            }
        }

        private void ClearDB()
        {
            var db = new EFContext();
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
            db.SaveChanges();
        }

        [TestMethod]
        public void Get_IsNull_NoContent()
        {
            var controller = new FabricanteController();
            var result = controller.Get() as HttpResponseMessage;
            Assert.AreEqual(204, (int)result.StatusCode);
        }

        [TestMethod]
        public void Get_HasContent_ReturnContent()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Get() as HttpResponseMessage;
            Assert.AreEqual(200, (int)result.StatusCode);
            Assert.IsNotNull(result.Content);
            //Assert.AreNotEqual(204, (int)result.StatusCode);
            ClearDB();
        }

        [TestMethod]
        public void Post_FabAdded_OK()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante() { nome = "Dummy4"};
            var result = controller.Post(dummy) as HttpResponseMessage;
            Assert.AreEqual(200, (int)result.StatusCode);
            //Assert.AreNotEqual(204, (int)result.StatusCode);
            ClearDB();
        }
        [TestMethod]
        public void Post_FabricanteNull_NoContent()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Post(null) as HttpResponseMessage;
            Assert.AreEqual(204, (int)result.StatusCode);
            ClearDB();
        }
        [TestMethod]
        public void Post_InfoIncompleta_BadRequest()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante();
            var result = controller.Post(dummy) as HttpResponseMessage;
            Assert.AreEqual(400, (int)result.StatusCode);
            ClearDB();
        }
        
        [TestMethod]
        public void Put_FabAdded_OK()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante() { id = 3, nome = "Dummy0" };
            var result = controller.Put(dummy) as HttpResponseMessage;
            Assert.AreEqual(200, (int)result.StatusCode);
            //Assert.AreNotEqual(204, (int)result.StatusCode);
            ClearDB();
        }

        [TestMethod]
        public void Put_FabricanteNull_NoContent()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Put(null) as HttpResponseMessage;
            Assert.AreEqual(204, (int)result.StatusCode);
            ClearDB();
        }

        [TestMethod]
        public void Put_InfoIncompleta_BadRequest()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante();
            var result = controller.Put(dummy) as HttpResponseMessage;
            Assert.AreEqual(400, (int)result.StatusCode);
            ClearDB();
        }

        [TestMethod]
        public void Put_NoExist_NotFound()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var dummy = new Fabricante() { id = 5, nome = "Dummy0" };
            var result = controller.Put(dummy) as HttpResponseMessage;
            Assert.AreEqual(404, (int)result.StatusCode);
            ClearDB();
        }

        [TestMethod]
        public void Delete_IsDeleted_OK()
        {
            PopulateDB();

            var controller = new FabricanteController();
            var result = controller.Delete(new Fabricante(){ id = 3}) as HttpResponseMessage;
            Assert.AreEqual(200, (int)result.StatusCode);
            //Assert.AreNotEqual(204, (int)result.StatusCode);
            ClearDB();
        }

        [TestMethod]
        public void Delete_NoExist_NotFound()
        {
            var controller = new FabricanteController();
            
            var result = controller.Delete(new Fabricante() { id = 5 });
            Assert.AreEqual(204, (int)result.StatusCode);

            PopulateDB();

            var result2 = controller.Delete(new Fabricante() { id = 6 });
            Assert.AreEqual(404, (int)result2.StatusCode);
            ClearDB();
        }
    }
}
