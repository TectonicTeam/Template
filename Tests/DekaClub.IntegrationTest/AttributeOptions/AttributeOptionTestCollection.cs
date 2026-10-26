using DekaClub.Api;
using DekaClub.IntegrationTest.Base;

namespace DekaClub.IntegrationTest.AttributeOptions;

[CollectionDefinition("Attribute Options Test Collection")]
public class AttributeOptionTestCollection : ICollectionFixture<WebApplicationTestFactory<Program>>;