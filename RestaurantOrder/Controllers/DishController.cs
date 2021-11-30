using Microsoft.AspNetCore.Mvc;
using RestaurantOrder.DTOs;
using RestaurantOrder.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RestaurantOrder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DishController : Controller
    {
        [HttpGet]
        public JsonResult Get(string timeOfDay, string orders)
        {
            timeOfDay = timeOfDay.Trim();
            if (!IsValidTimeParameter(timeOfDay))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Invalid parameters, is only allow 'Morning' or 'Night' time.");
            }

            Factory factory = new Factory(timeOfDay);
            List<int> orderIds;

            try
            {
                orderIds = ConvertToListInt(orders);
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Invalid parameters, is only allow numbers like: 1, 2, 3");
            }

            try
            {
                DishDto dishDto = factory.GetDishes(orderIds);
                return Json(dishDto);
            }
            catch
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json("Processing Error.");
            }
        }

        private static List<int> ConvertToListInt(string orders)
        {
            orders = orders.Trim();

            if (orders.EndsWith(','))
            {
                orders = orders.Remove(orders.Length - 1);
            }

            return orders.Split(',').Select(int.Parse).ToList();
        }

        private static bool IsValidTimeParameter(string timeOfDay)
        {
            if (!timeOfDay.Equals("morning", StringComparison.InvariantCultureIgnoreCase) &&
                (!timeOfDay.Equals("night", StringComparison.InvariantCultureIgnoreCase)))
            {
                return false;
            }
            return true;
        }
    }
}
