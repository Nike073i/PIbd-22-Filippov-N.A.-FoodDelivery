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
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<Store> Stores { get; set; }

        private DataListSingleton()
        {
            Dishes = new List<Dish>();
            Orders = new List<Order>();
            Sets = new List<Set>();
            Clients = new List<Client>();
            Stores = new List<Store>();
            Implementers = new List<Implementer>();
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
