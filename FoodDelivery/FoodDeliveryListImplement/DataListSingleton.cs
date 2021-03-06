﻿using FoodDeliveryListImplement.Models;
using System.Collections.Generic;

namespace FoodDeliveryListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Set> Sets { get; set; }

        private DataListSingleton()
        {
            Dishes = new List<Dish>();
            Orders = new List<Order>();
            Sets = new List<Set>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
