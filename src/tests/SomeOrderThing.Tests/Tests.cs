namespace SomeOrderThing.Tests
{
    using Newtonsoft.Json.Linq;
    using Xunit;

    public class Tests
    {
        [Fact]
        public void SerializeAndDeserialize()
        {
            var json = Properties.Resources.TestData;
            var jObject = JObject.Parse(json);

            var tableOrder = new TableOrder(jObject);

        }
    }
}
