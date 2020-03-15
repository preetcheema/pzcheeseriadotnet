# pzcheeseriadotnet

###To Run

* Update connection string 'PZCheeseriaConnectionString' in appsettings.json or alternatively update StartUp.cs to use UseInMemoryDatabase
* The launch urls are 'https://localhost:5001;http://localhost:5000'
* Swagger url is : https://localhost:5001/swagger/index.html
* Test data is being created in PZCheeseriaSeedDataCreator.CreateData()

###My Approach
I have structured the project in a way that I normally do in production. Some of the segregations like Infrastructure project
are overkill for this project. If it was just a POC with little functionality, it could be argued that there is no need for even storing
data in database, it could simply be in an in-memory collection.

### What I would have liked to do more
* I have not added Authentication/Authorization in the project. Ideally I would have liked to do more.
* The Integration tests are not exhaustive. I have only added few of the tests to demonstrate my approach.
* The tests are only written for BusinessLogic. Tests could also have been written at API level which would allow us to verify status codes and response etc.
* Some of the class implementations are not exhausitively complete, for example, class ApiExceptionMiddleware in API project only catches and processes some of the exceptions.
