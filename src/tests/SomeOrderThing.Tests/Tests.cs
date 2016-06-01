namespace SomeOrderThing.Tests
{
    using Newtonsoft.Json.Linq;
    using SomeOrderThing;
    using Xunit;

    public class Tests
    {
        [Fact]
        public void GetTableNumber()
        {
            var json = Properties.Resources.TestData;
            var jObject = JObject.Parse(json);

            var tableOrder = new TableOrder(jObject);

            Assert.Equal(23, tableOrder.TableNumber);
        }

        [Fact]
        public void SetTableNumber()
        {
            var json = Properties.Resources.TestData;
            var jObject = JObject.Parse(json);

            var tableOrder = new TableOrder(jObject);
            tableOrder.TableNumber = 42;
            Assert.Equal(42, tableOrder.TableNumber);
        }

        [Fact]
        public void GetIngredients()
        {
            var json = Properties.Resources.TestData;
            var jObject = JObject.Parse(json);

            var tableOrder = new TableOrder(jObject);

            Assert.Equal("razor blades, ice cream", tableOrder.Ingredients);
        }
    }
}
