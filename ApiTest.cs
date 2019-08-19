using System;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Tests
{
    [TestFixture]
    public class ApiTest
    {

        private string apiEndPoint = "https://reqres.in/api";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void getUserTest()
        {
            // Get an user
            var responseString = ApiUtils.getRequest(apiEndPoint + "/users/1");
            // Deserialize the response to a class. In this case ApiData
            ApiData apiData = JsonConvert.DeserializeObject<ApiData>(responseString);
            Console.WriteLine("----------------------apiData " + apiData);
            // Assert not null
            Assert.NotNull(apiData);
            // apiData is an instance of the class ApiData
            Assert.That(apiData, Is.InstanceOf<ApiData>());

            IList<int> idList = new List<int>();
            idList.Add(apiData.data.id);
            IList<Data> userList = DatabaseUtils.getUsers(idList);
            Console.WriteLine("----------------------userList " + userList);
            Assert.NotNull(userList);
            Assert.That(userList.Count == 1, "No user with the matching id is found in the database");
            Data dbUser = userList[0];
            Console.WriteLine("----------------------userList [0]" + userList[0]);
            Assert.NotNull(dbUser);
            // foreach (Data data in userList)
            //     Console.WriteLine(data);
            Data apiUser = apiData.data;
            Assert.That(apiUser.id, Is.EqualTo(dbUser.id), "api id and database id are not same");
            Assert.That(apiUser.email, Is.EqualTo(dbUser.email), "api email and database email are not same");
            Assert.That(apiUser.first_name, Is.EqualTo(dbUser.first_name), "api first_name and database first_name are not same");
            Assert.That(apiUser.last_name, Is.EqualTo(dbUser.last_name), "api last_name and database last_name are not same");
            Assert.That(apiUser.avatar, Is.EqualTo(dbUser.avatar), "api avatar and database avatar are not same");
        }

        [Test]
        public void getUsersTest()
        {
            var responseString = ApiUtils.getAllRequest(apiEndPoint + "/users");
            IList<Data> dataList = JsonConvert.DeserializeObject<IList<Data>>(responseString);
            Console.WriteLine("----------------------dataList " + dataList);
            Assert.NotNull(dataList);
            Assert.That(dataList, Is.InstanceOf<IList<Data>>());
            IList<int> idList = new List<int>();
            Dictionary<int, Data> apiUserDictionary = new Dictionary<int, Data>();
            foreach (Data data in dataList)
            {
                // add the ids  tothe idList to query the users from the database
                idList.Add(data.id);
                // add an id and the corresponding data object to the dictionary
                // This dictionary is useful when we want to compare the objects returned from the database
                // and the objects returned from the API based on the id. By using the TryGetValue method, a particular
                // object can be extracted from the apiUserDictionary
                apiUserDictionary.Add(data.id, data);
            }

            IList<Data> userList = DatabaseUtils.getUsers(idList);
            foreach (Data dbUser in userList)
            {
                //For the given dbUser find out the corresponding apiUser
                Data apiUser = apiUserDictionary[dbUser.id];
                Assert.NotNull(apiUser);
                Assert.That(apiUser.id, Is.EqualTo(dbUser.id), "api id and database id are not same");
                Assert.That(apiUser.email, Is.EqualTo(dbUser.email), "api email and database email are not same");
                Assert.That(apiUser.first_name, Is.EqualTo(dbUser.first_name), "api first_name and database first_name are not same");
                Assert.That(apiUser.last_name, Is.EqualTo(dbUser.last_name), "api last_name and database last_name are not same");
                Assert.That(apiUser.avatar, Is.EqualTo(dbUser.avatar), "api avatar and database avatar are not same");
            }

        }


    }
}