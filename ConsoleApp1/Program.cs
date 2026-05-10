using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("=== Каталог услуг ===");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Просмотр всех услуг");
                Console.WriteLine("2. Добавить услугу");
                Console.WriteLine("3. Удалить услугу по Id");
                Console.WriteLine("4. Выйти");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAllServices();
                        break;
                    case "2":
                        AddNewService();
                        break;
                    case "3":
                        DeleteService();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void ShowAllServices()
        {
            List<Service> services = ServiceManager.GetAll();
            if (services.Count == 0)
            {
                Console.WriteLine("Список услуг пуст.");
                return;
            }
            Console.WriteLine("\nВсе услуги");
            foreach (Service s in services)
            {
                Console.WriteLine(s);
            }
        }
        private static void AddNewService()
        {
            string name;
            string category;
            decimal price;
            int duration;

            while (true)
            {
                Console.Write("Введите название: ");
                name = Console.ReadLine();
                try
                {
                    Service.ValidateName(name);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Введите категорию: ");
                category = Console.ReadLine();
                try
                {
                    Service.ValidateCategory(category);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Введите цену (руб): ");
                string input = Console.ReadLine();
                if (!decimal.TryParse(input, out price))
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                    continue;
                }
                try
                {
                    Service.ValidatePrice(price);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            while (true)
            {
                Console.Write("Введите длительность (минуты): ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out duration))
                {
                    Console.WriteLine("Ошибка: введите целое число.");
                    continue;
                }
                try
                {
                    Service.ValidateDuration(duration);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            Service newService = new Service(0, name, category, price, duration);
            ServiceManager.AddService(newService);
            Console.WriteLine("Услуга успешно добавлена.");
        }

        private static void DeleteService()
        {
            try
            {
                Console.Write("Введите Id услуги для удаления: ");
                int id = int.Parse(Console.ReadLine());
                bool deleted = ServiceManager.DeleteServiceById(id);
                if (deleted)
                {
                    Console.WriteLine("Услуга удалена.");
                }
                else
                {
                    Console.WriteLine("Услуга с таким Id не найдена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}