using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrder.Models
{
    public class Toast : Abstracts.Dish
    {
        private int _order;
        private string _type;
        private string _name;
        private string _time;

        public Toast()
        {
            _order = 2;
            _type = "side";
            _name = "toast";
            _time = "morning";
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
