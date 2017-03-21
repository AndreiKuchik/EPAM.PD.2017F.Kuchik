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
    public class ClientCommunication:MarshalByRefObject
    {
        #region Priavate filds
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private bool StateServer;
        private  string IpAdress;
        private  int Port;
        private readonly TcpClient tcpClient;
       #endregion
        
        #region ctors
        public ClientCommunication()
            : this(13000,"127.0.0.1")
        {
            
        }
        public ClientCommunication(string _IpAdress)
            : this(13000,_IpAdress)
        {
            
        }
        public ClientCommunication(int port)
            : this(port,"127.0.0.1")
        {
            
        }
        public ClientCommunication(int _port, string _IpAdress)
        {
             Port = _port;
             IpAdress = _IpAdress;
             tcpClient = new TcpClient();
        }
        
#endregion

        #region Public Event
        public event EventHandler<UserEventArgs> AddNewUser = delegate { };
        public event EventHandler<UserEventArgs> RemoveUser = delegate { };
#endregion
        
        #region Public methods
        public void StopServer()
        {
            StateServer = false;
            logger.Trace("Client stopped");
        }

      
        public void RunClient()
        {
            StateServer = true;
            tcpClient.Connect(IpAdress, Port);
            Stream stm = tcpClient.GetStream();
            while (StateServer)
            {
                byte[] length = new byte[4];
                stm.Read(length, 0, 4);
                int leng = BitConverter.ToInt32(length, 0);
                byte[] bmsg = new byte[leng];
                stm.Read(bmsg, 0, bmsg.Length);
                Message message = Deserialize(bmsg);
                if ( message.TypeOfMessage == "Add")
                    if (message.ListUsers != null)
                    {
                        OnNewUser(new UserEventArgs(message.ListUsers));
                    }
                else 
                    OnNewUser(new UserEventArgs(message.user));
                if (message.TypeOfMessage == "Remove")
                    OnRemoveUser(new UserEventArgs(message.user));
            }

        }

#endregion

        #region Private methods
        public static Message Deserialize(byte[] message)
        {
            using (var memoryStream = new MemoryStream(message))
                return (Message)(new BinaryFormatter()).Deserialize(memoryStream);
        }
        #endregion

        #region Protected Methods
        protected virtual void OnNewUser(UserEventArgs e)
        {
            AddNewUser(this, e);
        }
        protected virtual void OnRemoveUser(UserEventArgs e)
        {
            RemoveUser(this, e);
        }
#endregion
      

    }
}
