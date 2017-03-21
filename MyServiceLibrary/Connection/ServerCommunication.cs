using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MyServiceLibrary.Services;
using NLog;

namespace MyServiceLibrary.Connection
{
    public class ServerCommunication : MarshalByRefObject
    {
        #region Private Filds
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly TcpListener tcpListener;
        private readonly List<TcpClient> listClients = new List<TcpClient>();
        private IMasterServce serviceMaster;
        private  string IpAddress;
        private  int Port;
        private bool StateServer;
#endregion
        
        #region ctors
        public ServerCommunication()
            : this(13000,"127.0.0.1")
        {
            
        }
        public ServerCommunication(string _IpAdress)
            : this(13000,_IpAdress)
        {
            
        }
        public ServerCommunication(int port)
            : this(port,"127.0.0.1")
        {
            
        }
        public ServerCommunication(int _port, string _IpAdress)
        {
             Port = _port;
             IpAddress = _IpAdress;
             tcpListener = new TcpListener(IPAddress.Parse(IpAddress), Port);
        }
        
#endregion

        #region Public Methods
        public void Connection(IMasterServce userServiceMaster)
        {
            serviceMaster = userServiceMaster;
            serviceMaster.AddNewUser += GetNotificationOfAdding;
            serviceMaster.RemoveUser += GetNotificationOfRemoving;
           
        }

        public void RunServer()
        {
            StateServer = true;
            tcpListener.Start();
            while (StateServer)
            {
                var tcpClient = tcpListener.AcceptTcpClient();
                listClients.Add(tcpClient);
            }
            logger.Trace("Server is running");
            tcpListener.Stop();

        }

        public void StopServer()
        {
            StateServer = false;
            logger.Trace("Server stopped");
        }
       
        #endregion

        #region Private Methods
        private void GetNotificationOfAdding(object sender, UserEventArgs eventArgs)
        {
            SendMessageToClients("Add", eventArgs);
        }
        private void GetNotificationOfRemoving(object sender, UserEventArgs eventArgs)
        {
            SendMessageToClients("Remove", eventArgs);
        }
        private void SendMessageToClients(string message, UserEventArgs eventArgs)
        {
            foreach (var client in listClients)
            {
                var strem = client.GetStream();
                 Message _message = new Message();
                if (eventArgs.Users !=null)
                {
                    _message = new Message(eventArgs.Users, message);
                   
                }
                else
                {
                     _message = new Message(eventArgs.user, message);
                    
                }
               
                 byte[] data = Serialize(_message);
                strem.Write(BitConverter.GetBytes(data.Length), 0, 4);
                strem.Write(data, 0, data.Length);
            }
        }
        private static byte[] Serialize(Message obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }
        #endregion
        
    }
}
