using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrder.Models
{
    public class Steak : Abstracts.Dish
    {
        private int _order;
        private string _type;
        private string _name;
        private string _time;

        public Steak()
        {
            _order = 1;
            _type = "entrée";
            _name = "steak";
            _time = "night";
        }

        public override string Name
        {
            get { return _name; }
        }

        public override string Type
        {
            get { return _type; }
        }

        public override string Time
        {
            get { return _time; }
        }

        public override int Order
        {
            get { return _order; }
        }
    }
}
