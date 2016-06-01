namespace SomeOrderThing
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TableOrder
    {
        private readonly List<LineItem> lineItems = new List<LineItem>();

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

        public class LineItem
        {
            public int Quantity { get; set; }

            public string Item { get; set; }

            public decimal Price { get; set; }
        }
    }
}
