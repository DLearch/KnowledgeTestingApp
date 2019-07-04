using ContractLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StartService();
        }

        static void StartService()
        {
            Uri address = new Uri("net.tcp://localhost:4222/IContract");
            NetTcpBinding binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = int.MaxValue;
            Type contract = typeof(IContract);
            ServiceHost host = new ServiceHost(typeof(Service));
            host.AddServiceEndpoint(contract, binding, address);
            host.Open();
            Console.WriteLine("Сервис запущен.");
            Console.ReadKey();
        }

        static void AddUser(int index)
        {
            ModelDB db = new ModelDB();
            db.Users.Add(new Models.User()
            {
                Name = "User" + index,
                Password = "Password",
                Email = "Email" + index + "@mail.com",
                RegDate = DateTime.Now,
                EmailIsVisible = false
            });
            db.SaveChanges();
        }
    }
}
