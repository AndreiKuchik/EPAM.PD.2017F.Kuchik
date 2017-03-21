using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServiceLibrary.Connection;

namespace MyServiceLibrary.Services
{
    public interface ISlaveService: IService
    {
        void GetNotificationOfAdding(object sender, UserEventArgs args);
        void GetNotificationOfRemoving(object sender, UserEventArgs args);
        void NotificationOnClient(ClientCommunication client);
    }
}
