using TechTalk.SpecFlow;
using WebApplicationOld.Contexto;
using WebApplicationOld.Models;

namespace SpecFlowProject.Hooks
{
    [Binding]
    public sealed class TestInitialize
    {
        [BeforeFeature("prod")]
        public static void BeforeFeatureProdHook()
        {
            Console.WriteLine("BeforeFeature @prod");
            using (var dbContext = new EFContext())
            {
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
                dbContext.Fabricantes.Add(new Fabricante { nome = "Dummy1" });
                dbContext.Fabricantes.Add(new Fabricante { nome = "Dummy2" });
                dbContext.Fabricantes.Add(new Fabricante { nome = "Dummy3" });
                dbContext.SaveChanges();
            }
        }
        [AfterFeature("prod")]
        public static void AfterFeatureProdHook()
        {
            Console.WriteLine("BeforeFeature @prod");
            using (var dbContext = new EFContext())
            {
                var db = new EFContext();
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE produto RESTART IDENTITY");
                db.SaveChanges();
            }
        }

    }
}