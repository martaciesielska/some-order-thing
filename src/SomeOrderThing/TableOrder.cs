namespace SomeOrderThing
{
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public class TableOrder
    {
        private readonly List<LineItem> lineItems = new List<LineItem>();
        private readonly List<string> ingredients = new List<string>();

        private readonly JObject joey;

        public TableOrder(JObject joey)
        {
            this.joey = joey;
        }

        public int TableNumber
        {
            get
            {
                return 1;
            }
        }

        public List<LineItem> LineItems
        {
            get { return this.lineItems; }
        }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public bool Paid { get; set; }

        public List<string> Ingredients
        {
            get { return this.ingredients; }
        }

        public class LineItem
        {
            public int Quantity { get; set; }

            public string Item { get; set; }

            public decimal Price { get; set; }
        }
    }
}
