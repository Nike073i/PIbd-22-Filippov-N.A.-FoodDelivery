using FoodDeliveryBusinnesLogic.Enums;
using FoodDeliveryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FoodDeliveryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string DishFileName = "Dish.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string SetFileName = "Set.xml";
        private readonly string StoreFileName = "Store.xml";
        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Set> Sets { get; set; }
        public List<Store> Stores { get; set; }
        private FileDataListSingleton()
        {
            Dishes = LoadDishes();
            Orders = LoadOrders();
            Sets = LoadSets();
            Stores = LoadStores();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveDishes();
            SaveOrders();
            SaveProducts();
            SaveStores();
        }
        private List<Dish> LoadDishes()
        {
            var list = new List<Dish>();
            if (File.Exists(DishFileName))
            {
                XDocument xDocument = XDocument.Load(DishFileName);
                var xElements = xDocument.Root.Elements("Dish").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Dish
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishName = elem.Element("DishName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SetId = Convert.ToInt32(elem.Element("SetId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                        Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Set> LoadSets()
        {
            var list = new List<Set>();
            if (File.Exists(SetFileName))
            {
                XDocument xDocument = XDocument.Load(SetFileName);
                var xElements = xDocument.Root.Elements("Set").ToList();
                foreach (var elem in xElements)
                {
                    var setDish = new Dictionary<int, int>();
                    foreach (var dish in elem.Element("SetDishes").Elements("SetDish").ToList())
                    {
                        setDish.Add(Convert.ToInt32(dish.Element("Key").Value), Convert.ToInt32(dish.Element("Value").Value));
                    }
                    list.Add(new Set
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SetName = elem.Element("SetName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        SetDishes = setDish
                    });
                }
            }
            return list;
        }

        private List<Store> LoadStores()
        {
            var list = new List<Store>();
            if (File.Exists(StoreFileName))
            {
                XDocument xDocument = XDocument.Load(StoreFileName);
                var xElements = xDocument.Root.Elements("Store").ToList();
                foreach (var elem in xElements)
                {
                    var storeDishes = new Dictionary<int, int>();
                    foreach (var dish in elem.Element("StoreDishes").Elements("StoreDish").ToList())
                    {
                        storeDishes.Add(Convert.ToInt32(dish.Element("Key").Value), Convert.ToInt32(dish.Element("Value").Value));
                    }
                    list.Add(new Store
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StoreName = elem.Element("StoreName").Value,
                        FullNameResponsible = elem.Element("FullNameResponsible").Value,
                        CreationDate = Convert.ToDateTime(elem.Element("CreationDate").Value),
                        StoreDishes = storeDishes
                    });
                }
            }
            return list;
        }

        private void SaveDishes()
        {
            if (Dishes != null)
            {
                var xElement = new XElement("Dishes");
                foreach (var dish in Dishes)
                {
                    xElement.Add(new XElement("Dish",
                    new XAttribute("Id", dish.Id),
                    new XElement("DishName", dish.DishName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(DishFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("SetId", order.SetId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveProducts()
        {
            if (Sets != null)
            {
                var xElement = new XElement("Sets");
                foreach (var set in Sets)
                {
                    var dishElement = new XElement("SetDishes");
                    foreach (var dish in set.SetDishes)
                    {
                        dishElement.Add(new XElement("SetDish",
                        new XElement("Key", dish.Key),
                        new XElement("Value", dish.Value)));
                    }
                    xElement.Add(new XElement("Set",
                     new XAttribute("Id", set.Id),
                     new XElement("SetName", set.SetName),
                     new XElement("Price", set.Price),
                     dishElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SetFileName);
            }
        }

        private void SaveStores()
        {
            if (Stores != null)
            {
                var xElement = new XElement("Stores");
                foreach (var store in Stores)
                {
                    var dishesElement = new XElement("StoreDishes");
                    foreach (var dish in store.StoreDishes)
                    {
                        dishesElement.Add(new XElement("StoreDish",
                            new XElement("Key", dish.Key),
                            new XElement("Value", dish.Value)));
                    }
                    xElement.Add(new XElement("Store", new XAttribute("Id", store.Id),
                        new XElement("StoreName", store.StoreName),
                        new XElement("FullNameResponsible", store.FullNameResponsible),
                        new XElement("CreationDate", store.CreationDate.ToString()), 
                        dishesElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StoreFileName);
            }
        }
    }
}
