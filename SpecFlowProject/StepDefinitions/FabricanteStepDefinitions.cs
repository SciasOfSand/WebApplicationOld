using Newtonsoft.Json;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebApplicationOld.Contexto;
using WebApplicationOld.Controllers;
using WebApplicationOld.Models;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class FabricanteStepDefinitions
    {
        FabricanteController? controller;
        Fabricante? dummy;
        HttpResponseMessage? result;
        string? jsonContent;
        List<Fabricante>? fabs;


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

        [Given(@"an empty Fabricante table")]
        public void GivenAnEmptyFabricanteTable()
        {
            ClearDB();
        }

        [When(@"I make a Fabricante GET request")]
        public void WhenIMakeAGETRequest()
        {
            controller = new FabricanteController();
            result = controller.Get();
        }

        [Then(@"I receive a (.*) status code from Fabricante request")]
        public void ThenIReceiveAStatusCode(int p0)
        {
            result.StatusCode.Should().HaveValue(p0);
        }

        [Given(@"a table populated by Fabricantes")]
        public void GivenATablePopulatedByFabs()
        {
            PopulateDB();
        }

        [Then(@"I receive a (.*) status code with the following Fabricante List:")]
        public void ThenIReceiveAStatusCodeWithTheFollowingFabricanteList(int p0, Table table)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            result.StatusCode.Should().HaveValue(p0);
            //Console.WriteLine($"The numbers are {fabs.FirstOrDefault(x => x.id.Equals(2)).nome}");
            fabs.FirstOrDefault(x => x.id.Equals(int.Parse(table.Rows[0]["id"])) && x.nome.Equals(table.Rows[0]["nome"])).Should().NotBeNull();
            fabs.FirstOrDefault(x => x.id.Equals(int.Parse(table.Rows[1]["id"])) && x.nome.Equals(table.Rows[1]["nome"])).Should().NotBeNull();
            fabs.FirstOrDefault(x => x.id.Equals(int.Parse(table.Rows[2]["id"])) && x.nome.Equals(table.Rows[2]["nome"])).Should().NotBeNull();
        }

        [When(@"I make a POST request for a Fabricante named ""([^""]*)"":")]
        public void WhenIMakeAPOSTRequestForAFabricanteNamed(string p0)
        {
            dummy = new Fabricante() { nome = p0 };
            controller = new FabricanteController();
            result = controller.Post(dummy);
        }

        [When(@"I GET the Fabricante data back form the DB")]
        public void WhenIGETTheDataBackFormTheDB()
        {
            controller = new FabricanteController();
            result = controller.Get();
        }

        [Then(@"there contains a Fabricante named ""([^""]*)""")]
        public void ThenThereContainsAnElementNamed(string p0)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.nome.Equals(p0)).Should().NotBeNull();
        }

        [When(@"I make a Fabricante POST request using a null JSON")]
        public void WhenIMakeAPOSTRequestUsingANullJSON()
        {
            controller = new FabricanteController();
            result = controller.Post(null);
        }

        [When(@"I make a Fabricante POST request using the following JSON:")]
        public void WhenIMakeAPOSTRequestUsingTheFollowingJSON(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            var fab = table.CreateInstance<Fabricante>();
            controller = new FabricanteController();
            result = controller.Post(fab);
        }
        [When(@"I make a PUT request for a Fabricante of id (.*), naming it ""([^""]*)"":")]
        public void WhenIMakeAPUTRequestForAFabricanteOfIdNamingIt(int p0, string p1)
        {
            controller = new FabricanteController();
            dummy = new Fabricante() { id = p0, nome = p1 };
            result = controller.Put(dummy);
        }
        [When(@"I make a Fabricante PUT request using a null JSON")]
        public void WhenIMakeAPUTRequestUsingANullJSON()
        {
            controller = new FabricanteController();
            result = controller.Put(null);
        }
        [When(@"I make a Fabricante PUT request using the following JSON:")]
        public void WhenIMakeAPUTRequestUsingTheFollowingJSON(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            var fab = table.CreateInstance<Fabricante>();
            controller = new FabricanteController();
            result = controller.Put(fab);
        }
        [Then(@"the Fabricante of id (.*) is now named ""([^""]*)""")]
        public void ThenTheElementOfIdIsNowNamed(int p0, string p1)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.id.Equals(p0)).nome.Should().Be(p1);
        }
        [When(@"I make a DELETE request for a Fabricante of ID (.*)")]
        public void WhenIMakeADELETERequestForAFabricanteOfID(int p0)
        {
            dummy = new Fabricante() { id = p0 };
            controller = new FabricanteController();
            result = controller.Delete(dummy);
        }
        [Then(@"the Fabricante of name ""([^""]*)"" is not found")]
        public void ThenTheElementOfNameIsNotFound(string p0)
        {
            jsonContent = result.Content.ReadAsStringAsync().Result;
            fabs = JsonConvert.DeserializeObject<List<Fabricante>>(jsonContent);
            fabs.FirstOrDefault(x => x.nome.Equals(p0)).Should().BeNull();
        }

    }
}
