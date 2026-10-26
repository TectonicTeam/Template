using DekaClub.Api;
using DekaClub.IntegrationTest.Base;
using Dekacore.Abstraction.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace DekaClub.IntegrationTest.AttributeOptions;

[Collection("Attribute Options Test Collection")]
[TestCaseOrderer("DekaClub.Test.IntegrationTest.Base.PriorityOrderer", "DekaClub.Test.IntegrationTest")]
public class AttributeOptionsTests(WebApplicationTestFactory<Program> factory, AttributeOptionsTestsContext context)
    : IClassFixture<AttributeOptionsTestsContext>
{
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        BaseAddress = new Uri("http://localhost:5220"),
    });

    //[Fact, TestPriority(1)]
    //public async Task CreateNewAttributeOption()
    //{
    //    var content = new CreateAnAttributeOptionCommand("TestTitle", "TestValue", "TestTag");
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/CreateAnAttributeOption", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<long>();
    //    Assert.True(result > 0);
    //    context.AttributeOptionId = result;
    //}

    //[Fact, TestPriority(2)]
    //public async Task GetAllAttributeOptions()
    //{
    //    var content = new GetAllAttributeOptionsRequest();
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/GetAllAttributeOptions", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<PagedList<GetAllAttributeOptionsResult>>();
    //    Assert.NotNull(result);
    //    Assert.NotEmpty(result.Items);
    //}

    //[Fact, TestPriority(3)]
    //public async Task EditAttributeOption()
    //{
    //    var content = new EditAttributeOptionCommand(context.AttributeOptionId, "NewTitle", "NewValue", "NewTag");
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/EditAttributeOption", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<long>();
    //    Assert.True(result > 0);
    //}

    //[Fact, TestPriority(4)]
    //public async Task GetAttributeOptionById()
    //{
    //    var content = new GetAttributeOptionByIdRequest(context.AttributeOptionId);
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/GetAttributeOption", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<GetAttributeOptionByIdResult>();
    //    Assert.NotNull(result);
    //    Assert.Equal(context.AttributeOptionId, result.Id);
    //    Assert.Equal("NewTitle", result.Title);
    //    Assert.Equal("NewValue", result.Value);
    //    Assert.Equal("NewTag", result.Tag);
    //}

    //[Fact, TestPriority(5)]
    //public async Task DeleteAttributeOption()
    //{
    //    var content = new DeleteAttributeOptionCommand(context.AttributeOptionId);
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/DeleteAttributeOption", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<long>();
    //    Assert.True(result > 0);
    //}

    //[Fact, TestPriority(6)]
    //public async Task DeleteNotExistsAttributeOptionShouldReturnsError()
    //{
    //    var content = new DeleteAttributeOptionCommand(context.AttributeOptionId);
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/DeleteAttributeOption", content, default);
    //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //    var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();
    //    Assert.NotNull(result);
    //    Assert.Equal("Attribute option was not found", result.Title);
    //}

    //[Fact, TestPriority(7)]
    //public async Task GetExceptionWhenRequestNotExistItem()
    //{
    //    var content = new GetAttributeOptionByIdRequest(context.AttributeOptionId);
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/GetAttributeOption", content, default);
    //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //    var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();
    //    Assert.NotNull(result);
    //    Assert.Equal("Attribute option was not found", result.Title);
    //}

    //[Fact, TestPriority(8)]
    //public async Task GetAllAttributeOptionsWithNoItems()
    //{
    //    var content = new GetAllAttributeOptionsRequest();
    //    var response = await _client.PostAsJsonAsync("AttributeOptions/GetAllAttributeOptions", content, default);
    //    response.EnsureSuccessStatusCode();
    //    var result = await response.Content.ReadFromJsonAsync<PagedList<GetAllAttributeOptionsResult>>();
    //    Assert.NotNull(result);
    //    Assert.Empty(result.Items);
    //}
}

public class AttributeOptionsTestsContext
{
    public long AttributeOptionId { get; set; }
}