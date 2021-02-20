using FoodDeliveryBusinnesLogic.BusinessLogics;
using FoodDeliveryBusinnesLogic.Interfaces;
using FoodDeliveryListImplement.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


namespace FoodDeliveryView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IDishStorage, DishStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISetStorage, SetStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStoreStorage, StoreStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<DishLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<SetLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<StoreLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
