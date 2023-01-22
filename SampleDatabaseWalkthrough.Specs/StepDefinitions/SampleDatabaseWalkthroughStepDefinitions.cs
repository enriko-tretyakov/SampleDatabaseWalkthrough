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
        public int _id; // Объявление глобальной переменной для ее последующего использования

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public SampleDatabase(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"Customer Id (.*)")]
        public void GivenCustomerId(int id)
        {
            _sampleDatabase.Id = id;
            // 1. Пример : Использование классических sql запросов
             _sampleDatabase.SampleDatabaseSetId(id);
            // 2. Пример : Использование LINQ
            // _sampleDatabaseLinq.SelectIdSampleDatabaseLinq(id);
            _id = _sampleDatabase.Id;         
        }

        [Given(@"CustomerData")]
        public void GivenCustomerData(Table tableCustomerData)
        {
            // Объявление коллекции Dictionary с вызовом класса TableExtension для получение данных в виде таблицы из фичи 
            var dictionary = TableExtensions.ToDictionary(tableCustomerData);
            // Заполнение строк коллекции данными из фичи
            _sampleDatabase.CompanyName = dictionary["CompanyName"];
            _sampleDatabase.ContactName = dictionary["ContactName"];
            _sampleDatabase.Phone = dictionary["Phone"];
            // Вызов метода SampleDatabaseUpdate() с глобальной пременной, полученной ранее, для обновления данных в SQL таблице
            _sampleDatabase.SampleDatabaseUpdate(_id);
        }

        [Given(@"NewData")]
        public void GivenNewData(Table tableNewData)
        {
            // Объявление коллекции Dictionary с вызовом класса TableExtension для получение данных в виде таблицы из фичи 
            var dictionary = TableExtensions.ToDictionary(tableNewData);
            // Заполнение строк коллекции данными из фичи
            _sampleDatabase.CompanyName = dictionary["CompanyName"];
            _sampleDatabase.ContactName = dictionary["ContactName"];
            _sampleDatabase.Phone = dictionary["Phone"];
            // Вызов метода SampleDatabaseUpdate() с глобальной пременной, полученной ранее, для обновления данных в SQL таблице
            _sampleDatabase.SampleDatabaseUpdate(_id);
        }


        [When(@"If Customer Exist")]
        public void WhenIfCustomerExist()
        {
            // Чтение таблицы Customers
            _sampleDatabase.SampleDatabaseRead(_id);
        }

        [Then(@"CustomerID shoul be ""([^""]*)""")]
        public void ThenCustomerIDShoulBe(string expected)
        {
            // проверка на то, что содержимое столбца [CustomerID] таблицы Customers SQL базы соотвествует ожидаемому результату
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