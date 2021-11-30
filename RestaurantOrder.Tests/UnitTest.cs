using NUnit.Framework;
using RestaurantOrder.DTOs;
using RestaurantOrder.Models.Abstracts;
using System.Collections.Generic;

namespace RestaurantOrder.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Factory factory = new Factory("Morning");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 2, 3 });
            if(dishDto.OrderSummary == "eggs, toast, coffee")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test2()
        {
            Factory factory = new Factory("Morning");
            DishDto dishDto = factory.GetDishes(new List<int> { 2, 1, 3 });
            if (dishDto.OrderSummary == "eggs, toast, coffee")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test3()
        {
            Factory factory = new Factory("Morning");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 2, 3, 4 });
            if (dishDto.OrderSummary == "eggs, toast, coffee, error")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test4()
        {
            Factory factory = new Factory("Morning");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 2, 3, 3, 3 });
            if (dishDto.OrderSummary == "eggs, toast, coffee(x3)")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test5()
        {
            Factory factory = new Factory("night");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 2, 3, 4 });
            if (dishDto.OrderSummary == "steak, potato, wine, cake")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test6()
        {
            Factory factory = new Factory("night");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 2, 2, 4 });
            if (dishDto.OrderSummary == "steak, potato(x2), cake")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test7()
        {
            Factory factory = new Factory("night");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 2, 3, 5 });
            if (dishDto.OrderSummary == "steak, potato, wine, error")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test8()
        {
            Factory factory = new Factory("night");
            DishDto dishDto = factory.GetDishes(new List<int> { 1, 1, 2, 3, 5 });
            if (dishDto.OrderSummary == "steak, error")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test9()
        {
            Factory factory = new Factory("night");
            DishDto dishDto = factory.GetDishes(new List<int> { 6 });
            if (dishDto.OrderSummary == "error")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void Test10()
        {
            Factory factory = new Factory("night");
            DishDto dishDto = factory.GetDishes(new List<int> { 2, 2, 2, 2, 2, 1, 3 });
            if (dishDto.OrderSummary == "steak, potato(x5), wine")
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
    }
}