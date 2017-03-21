using System;
using System.Threading.Tasks;
using MyServiceLibrary;
using MyServiceLibrary.Connection;
using MyServiceLibrary.Services;
using NLog;

namespace ServiceApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServerCommunication server = new ServerCommunication();
         
            CreateServiseManager builder = new CreateServiseManager();
            IMasterServce master;

            builder.CreateDomainForMaster(out master);
            server.Connection(master);
            Task.Run(() => server.RunServer()); 
            ClientCommunication client = new ClientCommunication();
            Task.Run(() => client.RunClient());
            ISlaveService slave;
            builder.CreateDomainForSlave(out slave, client);
            User user1 = new  User("Gidfgn","Nudfgn");
            master.Add(user1);
            //master.SetStateStorage();
            Console.WriteLine("Master");
            foreach (var user in master.Search(n => true))
            {
                Console.WriteLine("{0} {1}",user.FirstName, user.LastName);
            }
            Console.WriteLine();
            Console.WriteLine("Slave");
            foreach (var user in slave.Search(n => true))
            {
                Console.WriteLine("{0} {1}",user.FirstName, user.LastName);
            }
            Console.ReadLine();

             master.Remove(user1);
            Console.WriteLine();
            Console.WriteLine("Master");
            foreach (var user in master.Search(n => true))
            {
                Console.WriteLine("{0} {1}",user.FirstName, user.LastName);
            }
            Console.WriteLine();
            Console.WriteLine("Slave");
            foreach (var user in slave.Search(n => true))
            {
                Console.WriteLine("{0} {1}", user.FirstName, user.LastName);
            }
            Console.ReadLine();
            
        }
       
    }
}
