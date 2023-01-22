using LivingDoc.Dtos;
using NUnit.Framework;
using SampleDatabaseWalkthrough.Specs.Utils;
using SampleDatabaseWalkthrough.Specs.Xml;

namespace SampleDatabaseWalkthrough.Specs.StepDefinitions
{
    [Binding]
    public sealed class SampleDatabase
    {
        private readonly SampleDatabaseWalkthrough.SampleDatabase _sampleDatabase = new SampleDatabaseWalkthrough.SampleDatabase();
        private readonly SampleDatabaseWalkthrough.SampleDatabaseLinq _sampleDatabaseLinq = new SampleDatabaseWalkthrough.SampleDatabaseLinq();
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
            // 1. ������ : ������������� ������������ sql ��������
             _sampleDatabase.SampleDatabaseSetId(id);
            // 2. ������ : ������������� LINQ
            // _sampleDatabaseLinq.SelectIdSampleDatabaseLinq(id);
            _id = _sampleDatabase.Id;         
        }

        [Given(@"CustomerData")]
        public void GivenCustomerData(Table tableCustomerData)
        {
            // ���������� ��������� Dictionary � ������� ������ TableExtension ��� ��������� ������ � ���� ������� �� ���� 
            var dictionary = TableExtensions.ToDictionary(tableCustomerData);
            // ���������� ����� ��������� ������� �� ����
            _sampleDatabase.CompanyName = dictionary["CompanyName"];
            _sampleDatabase.ContactName = dictionary["ContactName"];
            _sampleDatabase.Phone = dictionary["Phone"];
            // ����� ������ SampleDatabaseUpdate() � ���������� ���������, ���������� �����, ��� ���������� ������ � SQL �������
            _sampleDatabase.SampleDatabaseUpdate(_id);
        }

        [Given(@"NewData")]
        public void GivenNewData(Table tableNewData)
        {
            // ���������� ��������� Dictionary � ������� ������ TableExtension ��� ��������� ������ � ���� ������� �� ���� 
            var dictionary = TableExtensions.ToDictionary(tableNewData);
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

        [Then(@"RestSum should be")]
        public void RestSumCheck(Table tableRestSum)
        {
            var dictRestSum = TableExtensions.ToDictionary(tableRestSum);
            ClientRestSum clientRestSum = new ClientRestSum();
            string searchName = dictRestSum["searchNameXml"];
            string searchValue = dictRestSum["searchValueXml"];

            XmlUtility xmlUtility = new XmlUtility();
            string expectedValue = xmlUtility.SerchChildElement(searchName, searchValue);

            Assert.AreEqual(expectedValue, searchValue);

        }

    }
}