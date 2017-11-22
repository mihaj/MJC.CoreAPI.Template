using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MJC.CoreAPI.Template.IntegrationTests
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;

        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(GetContentRootPath())
                .UseEnvironment("Test")
                .UseStartup<MJC.CoreAPI.Template.WebAPI.Startup>();

            _testServer = new TestServer(builder);

            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }

        private string GetContentRootPath()
        {
            string testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;

            var relativePathToApiProject = @"..\..\..\..\..\MJC.CoreAPI.Template\MJC.CoreAPI.Template.WebAPI";

            return Path.Combine(testProjectPath, relativePathToApiProject);
        }
    }
}
