namespace SomeOrderThing
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TableOrder
    {
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
    }
}
