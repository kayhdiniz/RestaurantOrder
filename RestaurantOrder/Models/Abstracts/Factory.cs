using RestaurantOrder.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.Models.Abstracts
{
    public enum NightDish
    {
        Steak = 1,
        Potato = 2,
        Wine = 3,
        Cake = 4
    }

    public enum MorningDish
    {
        Egg = 1,
        Toast = 2,
        Coffee = 3
    }

    public class Factory : DishFactory
    {
        private Dictionary<int, Type> _dishes;
        private string _timeOfDay;
        private int _orderAllowed;

        public Factory(string timeOfDay)
        {
            _timeOfDay = timeOfDay;
            _dishes = new Dictionary<int, Type>();

            if (_timeOfDay.Equals("morning", StringComparison.CurrentCultureIgnoreCase))
            {
                this.RegisterDish<Egg>((int)MorningDish.Egg);
                this.RegisterDish<Toast>((int)MorningDish.Toast);
                this.RegisterDish<Coffee>((int)MorningDish.Coffee);
                _orderAllowed = (int)MorningDish.Coffee;
            }

            if (_timeOfDay.Equals("night", StringComparison.CurrentCultureIgnoreCase))
            {
                this.RegisterDish<Steak>((int)NightDish.Steak);
                this.RegisterDish<Potato>((int)NightDish.Potato);
                this.RegisterDish<Wine>((int)NightDish.Wine);
                this.RegisterDish<Cake>((int)NightDish.Cake);
                _orderAllowed = (int)NightDish.Potato;
            }
        }

        private void RegisterDish<DishType>(int id) where DishType : Dish
        {
            _dishes.Add(id, typeof(DishType));
        }

        private Dish Create(int id)
        {
            try
            {
                return (Dish)Activator.CreateInstance(_dishes[id]);
            }
            catch
            {
                throw new Exception("The given key was not present in the dictionary");
            }
        }

        private bool ReachedOrderLimit(List<Dish> listDishes, int order)
        {
            if(listDishes == null)
            {
                return false;
            }

            if (order == _orderAllowed)
            {
                return false;
            }

            if (listDishes.Count(l => l.Order == order) == 0)
            {
                return false;
            }

            return true;
        }

        private Dish CreateDish(int order)
        {
            return this.Create(order);
        }

        private string GetOrderSummary(DishDto dishDto)
        {
            List<string> fields = new List<string>();
            
            foreach (var line in dishDto.ListDishes.GroupBy(info => info.Name)
                        .Select(group => new {
                            Metric = group.Key,
                            Count = group.Count()
                        }))
            {
                if (line.Count > 1)
                {
                    fields.Add($"{line.Metric}(x{line.Count})");
                }
                else
                {
                    fields.Add($"{line.Metric}");
                }
            }

            if (dishDto.Status == DishDto.STATUS_ERROR)
            {
                fields.Add("error");
            }

            return string.Join(", ", fields); ;
        }

        public override DishDto GetDishes(List<int> orders)
        {
            DishDto dishDto = new DishDto();
            dishDto.Status = DishDto.STATUS_SUCCESS;

            foreach (int order in orders)
            {
                if (ReachedOrderLimit(dishDto.ListDishes, order))
                {
                    dishDto.Status = DishDto.STATUS_ERROR;
                    dishDto.MessageError = $"Dish {order} reached order limit for the {_timeOfDay} time.";
                    break;
                }
                else
                {
                    try
                    {
                        dishDto.ListDishes.Add(CreateDish(order));
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "The given key was not present in the dictionary")
                        {
                            dishDto.Status = DishDto.STATUS_ERROR;
                            dishDto.MessageError = $"Dish {order} not found.";
                            break;
                        }
                        throw;
                    }
                }
            }

            dishDto.ListDishes = dishDto.ListDishes.OrderBy(x => x.Order).ToList();

            dishDto.OrderSummary = GetOrderSummary(dishDto);
            
            return dishDto;
        }
    }
}
