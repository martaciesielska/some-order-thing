namespace SomeOrderThing.Tests
{
    using Newtonsoft.Json.Linq;
    using SomeOrderThing;
    using Xunit;
    using System.Linq;

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

        [Fact]
        public void GetLineItems()
        {
            var json = Properties.Resources.TestData;
            var jObject = JObject.Parse(json);

            var tableOrder = new TableOrder(jObject);
            var lineItems = tableOrder.LineItems;
            Assert.NotEmpty(tableOrder.LineItems);
        }

        [Fact]
        public void SetLineItems()
        {
            var json = Properties.Resources.TestData;
            var jObject = JObject.Parse(json);

            var tableOrder = new TableOrder(jObject);
            tableOrder.LineItems.Add(new TableOrder.LineItem { Quantity = 4, Item = "Cheese Please", Price = 12.34m });
            Assert.NotEmpty(tableOrder.LineItems);

            var serialized = tableOrder.Serialize();
            var jArray = serialized["lineItems"];
            Assert.Equal(2, jArray.Count());
        }
    }
}
