using RestAPIExample.Model;
using RestSharp;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace RestAPIExample
{
    public class Tests
    {

        private RestClient client;
        private RestResponse<PostsModel> response;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000/");
            response = CreateNewPost();

        }

        [TearDown]
        public void TearDown()
        {
            RestRequest request = new RestRequest("posts/10", Method.Delete);
            try
            {
                client.Execute(request);
            }
            catch (System.Exception)
            {
            }

        }

        private RestResponse<PostsModel> CreateNewPost()
        {
            RestRequest request = new RestRequest("posts", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new PostsModel()
            {
                id = "10",
                title = "test-title",
                author = "Api test"
            });
            response = client.Execute<PostsModel>(request);
            return response;
        }

        [Test]
        public void GET_WhenGetPostsWithId_ResponseShouldContainExpectedValue()
        {
            RestRequest request = new RestRequest("posts/10", Method.Get);

            // act
            response = client.Execute<PostsModel>(request);

            // assert          
            Assert.That(response.Data.title, Is.EqualTo("test-title"));
            Assert.That(response.Data.author, Is.EqualTo("Api test"));
        }

        [Test]
        public void POST_WhenExecutePostModel_ItShouldAppearInResponse()
        {
            Assert.That(response.Data.author, Is.EqualTo("Api test"));
            Assert.That(response.Data.title, Is.EqualTo("test-title"));
        }


        [Test]
        public void PUT_UpdateTypeClassBody()
        {
            RestRequest request = new RestRequest("posts/10", Method.Put);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new PostsModel()
            {
                author = "azaliya"
            });

            // act
            response = client.Execute<PostsModel>(request);

            // assert
            Assert.That(response.Data.author, Is.EqualTo("azaliya"));
        }

        [Test]
        public void DELETE_RemovePostsWithId_ShouldBeSuccessful()
        {
            RestRequest request = new RestRequest("posts/10", Method.Delete);
            RestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}