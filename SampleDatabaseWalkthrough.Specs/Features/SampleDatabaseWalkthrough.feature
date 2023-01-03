Feature: SampleDatabaseWalkthrough

Sample Database Walkthrough Testing to adding data
Данный сценарий демонстрирует автотест по обновлению данных в локальной SQL базе
при условии, уже существует запись с указанным Id и осуществляется проверка на соотвествие CustomerID

Link to a feature: [SampleDatabaseWalkthrough](SampleDatabaseWalkthrough.Specs/Features/SampleDatabaseWalkthrough.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario Outline: Add data for existing customer
	Given Customer Id 3
	And CustomerData
	| Key         | Value           |
	| CompanyName | ООО "ИнфоТек"   |  
	| ContactName | Сидоров И. С.   |
	| Phone       | 111-123         |
	When If Customer Exist
	Then CustomerID shoul be <CustomerID>

Examples: 
	| CustomerID  |
	| "C0003"	  |