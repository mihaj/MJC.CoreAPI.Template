using MJC.CoreAPI.Template.WebAPI.Core.Dtos.Dummy;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MJC.CoreAPI.Template.IntegrationTests
{
    public class Dummy2ControllerShould : IClassFixture<TestServerFixture>
    {
        private readonly ITestOutputHelper output;
        private readonly TestServerFixture fixture;

        public Dummy2ControllerShould(ITestOutputHelper output, TestServerFixture fixture)
        {
            this.output = output;
            this.fixture = fixture;
        }

        [Fact]
        public async Task ReturnDummy()
        {
            var response = await fixture.Client.GetAsync("api/dummies/1");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReturnErrorOnInvalidValue()
        {
            var response = await fixture.Client.GetAsync("api/dummies/0");

            var responseString = await response.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ListDummies()
        {
            var response = await fixture.Client.GetAsync("api/dummies");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostDummyShouldBeSuccessful()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "api/dummies");

            var formData = new DummyDtoForCreation()
            {
                Name = "Jackson"
            };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(formData), Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await fixture.Client.SendAsync(postRequest);

            var responseString = await postResponse.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        }

        [Fact]
        public async Task PutDummyShouldBeSuccessful()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Put, "api/dummies/1");

            var formData = new DummyDtoForUpdate()
            {
                Name = "Jackson Rob"
            };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(formData), Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await fixture.Client.SendAsync(postRequest);

            var responseString = await postResponse.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteDummyShouldBeSuccessful()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Delete, "api/dummies/2");

            HttpResponseMessage postResponse = await fixture.Client.SendAsync(postRequest);

            var responseString = await postResponse.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.NoContent, postResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteDummyShouldBeUnSuccessfulBecauseNotFound()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Delete, "api/dummies/500");

            HttpResponseMessage postResponse = await fixture.Client.SendAsync(postRequest);

            var responseString = await postResponse.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.NotFound, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreateDummyShouldBeConflictBecauseAlreadyExist()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "api/dummies/1");

            var formData = new DummyDtoForUpdate()
            {
                Name = "Jackson"
            };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(formData), Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await fixture.Client.SendAsync(postRequest);

            var responseString = await postResponse.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.Conflict, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreateDummyShouldBeNotFoundIfUsingCreateWithId()
        {
            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "api/dummies/1000");

            var formData = new DummyDtoForUpdate()
            {
                Name = "Jackson"
            };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(formData), Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await fixture.Client.SendAsync(postRequest);

            var responseString = await postResponse.Content.ReadAsStringAsync();

            output.WriteLine(responseString);

            Assert.Equal(HttpStatusCode.NotFound, postResponse.StatusCode);
        }
    }
}
