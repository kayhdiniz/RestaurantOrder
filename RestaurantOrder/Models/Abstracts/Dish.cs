using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrder.Models.Abstracts
{
    public abstract class Dish
    {
        public abstract int Order { get; }

        public abstract string Type { get; }

        public abstract string Time { get; }

        public abstract string Name { get; }        
    }
}
