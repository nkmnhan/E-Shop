using Xunit;

namespace EShop.UnitTest.Fixture
{
    [CollectionDefinition("Eshop test collection")]
    public class TestCollection : ICollectionFixture<TestFixture>
    {
    }
}
