using RestaurantOrder.DTOs;
using System.Collections.Generic;

namespace RestaurantOrder.Models.Abstracts
{
    public abstract class DishFactory
    {
        public abstract DishDto GetDishes(List<int> orders);
    }
}
