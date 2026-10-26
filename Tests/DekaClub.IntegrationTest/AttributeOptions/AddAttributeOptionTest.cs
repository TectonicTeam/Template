using DekaClub.Api;
using DekaClub.IntegrationTest.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace DekaClub.IntegrationTest.AttributeOptions;

public class AddAttributeOptionTest(WebApplicationTestFactory<Program> factory)
    : IClassFixture<WebApplicationTestFactory<Program>>
{
    private readonly WebApplicationTestFactory<Program> _factory = factory;

    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        BaseAddress = new Uri("http://localhost:5220")
    });


    //[Fact]
    //public async Task CreateNewAttributeOption()
    //{
    //    var content = new CreateAnAttributeOptionCommand("TestTitle", "TestValue", "TestTag");
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/CreateAnAttributeOption", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<long>();
    //    Assert.True(result > 0);
    //}
}