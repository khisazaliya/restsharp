using RestSharp;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace RestAPIExample
{
    public class Tests
    {


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckSuccessfulResponse_WhenGetUsersPost()
        {
            RestClient client = new RestClient("http://localhost:3000/");
            RestRequest request = new RestRequest("posts/1", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        [Test]
        public void CheckContent_WhenGetUsersPost()
        {
            RestClient client = new RestClient("http://localhost:3000/");
            RestRequest request = new RestRequest("posts/1", Method.Get);

            RestResponse response = client.Execute(request);

            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new StringConverter());
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content, serializeOptions);

            Assert.That(result["title"], Is.EqualTo("super title"));
            Assert.That(result["author"], Is.EqualTo("john smith"));

        }
    }
}