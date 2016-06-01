namespace SomeOrderThing
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public class TableOrder
    {
        private readonly List<LineItem> lineItems = new List<LineItem>();

        private readonly JObject joey;

        public TableOrder(Guid id)
        {
            this.joey = new JObject();
            this.Id = id;
        }

        public TableOrder(JObject joey)
        {
            this.joey = joey;

            var jarray = (JArray)this.joey["lineItems"];

            foreach (var j in jarray)
            {
                var lineItem = j.ToObject<LineItem>();
                this.lineItems.Add(lineItem);
            }
        }

        public Guid Id
        {
            get { return (Guid)this.joey["id"]; }
            set { this.joey["id"] = value; }
        }

        public int TableNumber
        {
            get { return (int)this.joey["tableNumber"]; }
            set { this.joey["tableNumber"] = value; }
        }

        public List<LineItem> LineItems
        {
            get
            { 
                return this.lineItems;
            }
        }

        public decimal Tax
        {
            get { return (decimal)this.joey["tax"]; }
            set { this.joey["tax"] = value; }
        }

        public decimal Total
        {
            get { return (decimal)this.joey["total"]; }
            set { this.joey["total"] = value; }
        }

        public bool Paid
        {
            get { return (bool)this.joey["paid"]; }
            set { this.joey["paid"] = value; }
        }

        public JObject Serialize()
        {
            this.joey["lineItems"] = JObject.FromObject(this.lineItems);
            return this.joey;
        }

        public string Ingredients
        {
            get { return (string)this.joey["ingredients"]; }
            set { this.joey["ingredients"] = value; }
        }

        public class LineItem
        {
            public int Quantity { get; set; }

            public string Item { get; set; }

            public decimal Price { get; set; }
        }
    }
}
