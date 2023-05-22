using System.Net.Http;
using WebApplicationOld.Controllers;
using WebApplicationOld.Models;
using WebApplicationOld.Contexto;
using NUnit.Framework;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DBContextTeste
{
    [TestFixture]
    public class ProdutoTeste
    {
        [SetUp]
        public void Init()
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

        private void PopulateDB()
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE produto RESTART IDENTITY");
                dbContext.Produtos.Add(new Produto { nome = "Dummy1", tipo = "DummyType", id_fabricante = 1 });
                dbContext.Produtos.Add(new Produto { nome = "Dummy2", tipo = "DummyType", id_fabricante = 1 });
                dbContext.Produtos.Add(new Produto { nome = "Dummy3", tipo = "DummyType", id_fabricante = 1 });
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
            var dbContext = new TestDBContext();
            var controller = new ProdutoController();
            var result = controller.Get() as HttpResponseMessage;
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);
        }

        [Test]
        public void Get_HasContent_ReturnContent()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var result = controller.Get() as HttpResponseMessage;
            //Assert.AreEqual(200, (int)result.StatusCode);
            //Assert.IsNotNull(result.Content);
            result.StatusCode.Should().HaveValue(200);
            result.Content.Should().NotBeNull();
            string jsonContent = result.Content.ReadAsStringAsync().Result;
            List<Produto> prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            prods.FirstOrDefault(x => x.id.Equals(1) && x.nome.Equals("Dummy1")).Should().NotBeNull();
            prods.FirstOrDefault(x => x.id.Equals(2) && x.nome.Equals("Dummy2")).Should().NotBeNull();
            prods.FirstOrDefault(x => x.id.Equals(3) && x.nome.Equals("Dummy3")).Should().NotBeNull();
            ClearDB();
        }

        [Test]
        public void Post_FabAdded_OK()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var dummy = new Produto()
            {
                nome = "Dummy4",
                tipo = "DummyType",
                id_fabricante = 3
            };
            var result = controller.Post(dummy) as HttpResponseMessage;
            //Assert.AreEqual(200, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(200);
            var result2 = controller.Get();
            result2.StatusCode.Should().HaveValue(200);
            result2.Content.Should().NotBeNull();
            string jsonContent = result2.Content.ReadAsStringAsync().Result;
            List<Produto> prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            prods.FirstOrDefault(x => x.nome.Equals("Dummy4")).Should().NotBeNull();
            ClearDB();
        }

        [Test]
        public void Post_ProdutoNull_NoContent()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var result = controller.Post(null) as HttpResponseMessage;
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);
            ClearDB();
        }
        [Test]
        public void Post_InfoIncompleta_BadRequest()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var dummy = new Produto();
            var result = controller.Post(dummy) as HttpResponseMessage;
            //Assert.AreEqual(400, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(400);
            ClearDB();
        }

        [Test]
        public void Put_ProdAdded_OK()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var dummy = new Produto()
            {
                id = 3,
                nome = "Dummy0",
                tipo = "DummyN",
                id_fabricante = 3
            };
            var result = controller.Put(dummy) as HttpResponseMessage;
            //Assert.AreEqual(200, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(200);
            var result2 = controller.Get();
            result2.StatusCode.Should().HaveValue(200);
            result2.Content.Should().NotBeNull();
            string jsonContent = result2.Content.ReadAsStringAsync().Result;
            List<Produto> prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            prods.FirstOrDefault(x => x.nome.Equals("Dummy0") && x.tipo.Equals("DummyN")).Should().NotBeNull();
            ClearDB();
        }
        [Test]
        public void Put_ProdutoNull_NoContent()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var result = controller.Put(null) as HttpResponseMessage;
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);
            ClearDB();
        }
        [Test]
        public void Put_InfoIncompleta_BadRequest()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var dummy = new Produto()
            {
                tipo = "DummyN",
            };
            var result = controller.Put(dummy) as HttpResponseMessage;
            //Assert.AreEqual(400, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(400);
            ClearDB();
        }

        [Test]
        public void Put_NoExist_NotFound()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var dummy = new Produto()
            {
                id = 5,
                nome = "Dummy0",
                tipo = "DummyType",
                id_fabricante = 5
            };
            var result = controller.Put(dummy) as HttpResponseMessage;
            //Assert.AreEqual(404, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(404);
            ClearDB();
        }

        [Test]
        public void Delete_IsDeleted_OK()
        {
            PopulateDB();

            var controller = new ProdutoController();
            var result = controller.Delete(3) as HttpResponseMessage;
            //Assert.AreEqual(200, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(200);
            var result2 = controller.Get();
            result2.StatusCode.Should().HaveValue(200);
            result2.Content.Should().NotBeNull();
            string jsonContent = result2.Content.ReadAsStringAsync().Result;
            List<Produto> fabs = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            fabs.FirstOrDefault(x => x.id.Equals(3)).Should().BeNull();
            ClearDB();
        }

        [Test]
        public void Delete_NoExist_NotFound()
        {
            var controller = new ProdutoController();

            var result = controller.Delete(new Produto() { id = 5 });
            //Assert.AreEqual(204, (int)result.StatusCode);
            result.StatusCode.Should().HaveValue(204);

            PopulateDB();

            var result2 = controller.Delete(new Produto() { id = 6 });
            //Assert.AreEqual(404, (int)result2.StatusCode);
            result2.StatusCode.Should().HaveValue(404);
            ClearDB();
        }

        [TearDown]
        public void ClearFab()
        {
            using(var dbContext = new EFContext())
            {
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
                dbContext.SaveChanges();
            }
        }
    }
}
