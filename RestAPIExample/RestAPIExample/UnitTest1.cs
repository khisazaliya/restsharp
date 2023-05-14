using RestSharp;
using System.Net;

namespace RestAPIExample
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckSuccessfulResponse_WhenGetUserRepos()
        {
            RestClient client = new RestClient("http://localhost:3000/");
            RestRequest request = new RestRequest("posts/1", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }
    }
}