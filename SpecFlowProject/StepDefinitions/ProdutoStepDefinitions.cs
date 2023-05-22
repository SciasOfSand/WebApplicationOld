using Newtonsoft.Json;
using System;
using System.Data.Entity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.CommonModels;
using WebApplicationOld.Contexto;
using WebApplicationOld.Controllers;
using WebApplicationOld.Models;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class ProdutoStepDefinitions
    {
        ProdutoController? controller;
        Produto? dummy;
        HttpResponseMessage? result;
        string? jsonContent;
        List<Produto>? prods;

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
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE produto RESTART IDENTITY");
            db.SaveChanges();
        }
        [Given(@"an empty Produto table")]
        public void GivenAnEmptyProdutoTable()
        {
            ClearDB();
        }

        [Given(@"a table populated by Produtos")]
        public void GivenATablePopulatedByProds()
        {
            PopulateDB();
        }

        [When(@"I make a Produto GET request")]
        public void WhenIMakeAProdutoGETRequest()
        {
            controller = new ProdutoController();
            result = controller.Get();
        }

        [Then(@"I receive a (.*) status code from Produto request")]
        public void ThenIReceiveAStatusCodeFromProdutoRequest(int p0)
        {
            result.StatusCode.Should().HaveValue(p0);
        }

        [Then(@"I receive a (.*) status code with the following Produto List:")]
        public void ThenIReceiveAStatusCodeWithTheFollowingProdutoList(int p0, Table table)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            result.StatusCode.Should().HaveValue(p0);
            prods.FirstOrDefault(x => x.id.Equals(int.Parse(table.Rows[0]["id"])) && x.nome.Equals(table.Rows[0]["nome"])).Should().NotBeNull();
            prods.FirstOrDefault(x => x.id.Equals(int.Parse(table.Rows[1]["id"])) && x.nome.Equals(table.Rows[1]["nome"])).Should().NotBeNull();
            prods.FirstOrDefault(x => x.id.Equals(int.Parse(table.Rows[2]["id"])) && x.nome.Equals(table.Rows[2]["nome"])).Should().NotBeNull();
        }

        [When(@"I make a POST request for a Produto named ""([^""]*)"" and of Fabricante id (.*):")]
        public void WhenIMakeAPOSTRequestForAProdutoNamedAndOfFabricanteId(string p0, int p1)
        {
            dummy = new Produto() { nome = p0 , id_fabricante = p1};
            controller = new ProdutoController();
            result = controller.Post(dummy);
        }

        [When(@"I GET the Produto data back form the DB")]
        public void WhenIGETTheProdutoDataBackFormTheDB()
        {
            controller = new ProdutoController();
            result = controller.Get();
        }

        [Then(@"there contains a Produto named ""([^""]*)""")]
        public void ThenThereContainsAProdutoNamed(string p0)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            prods.FirstOrDefault(x => x.nome.Equals(p0)).Should().NotBeNull();
        }

        [When(@"I make a Produto POST request using a null JSON")]
        public void WhenIMakeAProdutoPOSTRequestUsingANullJSON()
        {
            controller = new ProdutoController();
            result = controller.Post(null);
        }

        [When(@"I make a Produto POST request using the following JSON:")]
        public void WhenIMakeAProdutoPOSTRequestUsingTheFollowingJSON(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            var prod = table.CreateInstance<Produto>();
            controller = new ProdutoController();
            result = controller.Post(prod);
        }

        [When(@"I make a PUT request for a Produto of id (.*), naming it ""([^""]*)"":")]
        public void WhenIMakeAPUTRequestForAProdutoOfIdNamingIt(int p0, string p1)
        {
            controller = new ProdutoController();
            dummy = new Produto() { id = p0, nome = p1 };
            result = controller.Put(dummy);
        }

        [Then(@"the Produto of id (.*) is now named ""([^""]*)""")]
        public void ThenTheProdutoOfIdIsNowNamed(int p0, string p1)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            prods.FirstOrDefault(x => x.id.Equals(p0)).nome.Should().Be(p1);
        }

        [When(@"I make a Produto PUT request using a null JSON")]
        public void WhenIMakeAProdutoPUTRequestUsingANullJSON()
        {
            controller = new ProdutoController();
            result = controller.Put(null);
        }

        [When(@"I make a Produto PUT request using the following JSON:")]
        public void WhenIMakeAProdutoPUTRequestUsingTheFollowingJSON(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            var prod = table.CreateInstance<Produto>();
            controller = new ProdutoController();
            result = controller.Put(prod);
        }

        [When(@"I make a DELETE request for a Produto of ID (.*)")]
        public void WhenIMakeADELETERequestForAProdutoOfID(int p0)
        {
            dummy = new Produto() { id = p0 };
            controller = new ProdutoController();
            result = controller.Delete(dummy);
        }

        [Then(@"the Produto of name ""([^""]*)"" is not found")]
        public void ThenTheProdutoOfNameIsNotFound(string p0)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            prods = JsonConvert.DeserializeObject<List<Produto>>(jsonContent);
            prods.FirstOrDefault(x => x.nome.Equals(p0)).Should().BeNull();
        }
    }
}
