using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            Console.WriteLine($"{nameof(GivenTheFirstNumberIs)} : {number}");
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic

            Console.WriteLine($"{nameof(GivenTheSecondNumberIs)} : {number}");
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //TODO: implement act (action) logic

            Console.WriteLine($"{nameof(WhenTheTwoNumbersAreAdded)}");
        }

        [Then(@"the result should ""([^""]*)""")]
        public void ThenTheResultShould(string result)
        {
            Console.WriteLine($"{nameof(ThenTheResultShould)} : {result}");
        }

        [Given(@"I input the following numbers to the calculator")]
        public void GivenIInputTheFollowingNumbersToTheCalculator(Table table)
        {
            dynamic data = table.CreateDynamicSet();

            foreach (var item in data) {
                
                Console.WriteLine($"The numbers are {item.Numbers}");
            }
        }

        [Then(@"I see the result and a few more details")]
        public void ThenISeeTheResultAndAFewMoreDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            Console.WriteLine($"The result is going to hold the Value {data.Results} with {data.Logo}");
        }

    }

    public record Calculation
    {
        public int Numbers { get; set; }
    }
}