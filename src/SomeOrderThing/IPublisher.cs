using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeOrderThing
{
    public interface IPublisher
    {
        void Publish(string topic, TableOrder order);
    }
}
