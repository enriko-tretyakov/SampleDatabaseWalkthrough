using NUnit.Framework;
using SampleDatabaseWalkthrough.Specs.Utils;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SampleDatabaseWalkthrough.Specs.StepDefinitions
{
    [Binding]
    public sealed class SampleDatabase
    {
        private readonly SampleDatabaseWalkthrough.SampleDatabase _sampleDatabase = new SampleDatabaseWalkthrough.SampleDatabase();
        private readonly ScenarioContext _scenarioContext;
        public bool result = false;
        public string? CustomerID = null;
        public int _id; // ���������� ���������� ���������� ��� �� ������������ �������������

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public SampleDatabase(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"Customer Id (.*)")]
        public void GivenCustomerId(int id)
        {
            _sampleDatabase.Id = id;
            _sampleDatabase.SampleDatabaseSetId(id);
            _id = _sampleDatabase.Id;
        }

        [Given(@"CustomerData")]
        public void GivenCustomerData(Table table)
        {
            // ���������� ��������� Dictionary � ������� ������ TableExtension ��� ��������� ������ � ���� ������� �� ���� 
            var dictionary = TableExtensions.ToDictionary(table);
            // ���������� ����� ��������� ������� �� ����
            _sampleDatabase.CompanyName = dictionary["CompanyName"];
            _sampleDatabase.ContactName = dictionary["ContactName"];
            _sampleDatabase.Phone = dictionary["Phone"];
            // ����� ������ SampleDatabaseUpdate() � ���������� ���������, ���������� �����, ��� ���������� ������ � SQL �������
            _sampleDatabase.SampleDatabaseUpdate(_id);
        }

        [When(@"If Customer Exist")]
        public void WhenIfCustomerExist()
        {
            // ������ ������� Customers
            _sampleDatabase.SampleDatabaseRead(_id);
        }

        [Then(@"CustomerID shoul be ""([^""]*)""")]
        public void ThenCustomerIDShoulBe(string expected)
        {
            // �������� �� ��, ��� ���������� ������� [CustomerID] ������� Customers SQL ���� ������������ ���������� ����������
            Assert.AreEqual(expected, _sampleDatabase.SampleDatabaseRead(_id));
        }

    }
}