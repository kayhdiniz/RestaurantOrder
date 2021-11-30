using RestaurantOrder.Models.Abstracts;
using System.Collections.Generic;

namespace RestaurantOrder.DTOs
{
    public class DishDto
    {
        public static string STATUS_SUCCESS = "Success.";

        public static string STATUS_ERROR = "Error.";

        private string _order_summary { get; set; }

        private List<Dish> _listDishes { get; set; }

        private string _status { get; set; }

        private string _messageError { get; set; }

        public DishDto()
        {
            _listDishes = new List<Dish>();
        }

        public string OrderSummary
        {
            get { return _order_summary; }
            set { _order_summary = value; }
        }

        public List<Dish> ListDishes
        {
            get { return _listDishes; }
            set { _listDishes = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string MessageError
        {
            get { return _messageError; }
            set { _messageError = value; }
        }
    }
}
