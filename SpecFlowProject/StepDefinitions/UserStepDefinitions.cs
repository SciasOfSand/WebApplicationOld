using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    internal class UserStepDefinitions
    {
        [Given(@"I enter random user data")]
        public void GivenIEnterRandomUserData()
        {
            var person = new Fixture()
                .Build<User>()
                .With(x => x.Email, "a@b.cd")
                .Create();
            Console.WriteLine($"The user {person.Name} has email {person.Email} and his address {person.Address} with phone number {person.Phone}.");
        }

        [Given(@"I input dynamic\tdomain for (.* email)")]
        public void GivenIInputDynamicDomainForAB_CdEmail(string email)
        {
            Console.WriteLine($"The random email address is:{email}");
        }


    }

    public record User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
