using TechTalk.SpecFlow;
using WebApplicationOld.Contexto;

namespace SpecFlowProject.Hooks
{
    [Binding]
    public sealed class TestInitialize
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            //TODO: implement logic that has to run before executing each scenario
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // Example of ordering the execution of hooks
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=order#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario
        }

        [BeforeFeature("Fabricante")]
        public static void FabricanteHook()
        {
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var db = new EFContext();
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE fabricante RESTART IDENTITY CASCADE");
            db.SaveChanges();
        }
    }
}