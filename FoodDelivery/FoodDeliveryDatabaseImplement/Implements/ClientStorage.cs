﻿using FoodDeliveryBusinnesLogic.BindingModels;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryBusinnesLogic.ViewModels;
using FoodDeliveryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Clients.Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientFIO = rec.ClientFIO,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                return context.Clients
                .Where(rec => rec.Email == model.Email && rec.Password == rec.Password)
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientFIO = rec.ClientFIO,
                    Email = rec.Email,
                    Password = rec.Password
                })
                .ToList();
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new FoodDeliveryDatabase())
            {
                var client = context.Clients
                .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
                return client != null ?
                new ClientViewModel
                {
                    Id = client.Id,
                    ClientFIO = client.ClientFIO,
                    Email = client.Email,
                    Password = client.Password
                } :
                null;
            }
        }

        public void Insert(ClientBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                context.Clients.Add(CreateModel(model, new Client()));
                context.SaveChanges();
            }
        }

        public void Update(ClientBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                var client = context.Clients.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (client == null)
                {
                    throw new Exception("Клиент не найден");
                }
                CreateModel(model, client);
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new FoodDeliveryDatabase())
            {
                var client = context.Clients.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (client != null)
                {
                    context.Clients.Remove(client);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Email = model.Email;
            client.Password = model.Password;
            return client;
        }
    }
}
