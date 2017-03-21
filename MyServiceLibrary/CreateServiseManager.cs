using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyServiceLibrary.Connection;
using MyServiceLibrary.Services;
using MyServiceLibrary.CreateId;
using MyServiceLibrary.Exceptions;
using NLog;

namespace MyServiceLibrary
{
    public class CreateServiseManager
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private int CountSlave;
        private IMasterServce masterService;
        private int CountMaster;

         public CreateServiseManager()
        {
            CountSlave = int.Parse(ConfigurationManager.AppSettings["CountSlave"]);
            CountMaster = int.Parse(ConfigurationManager.AppSettings["CountMaster"]);
        }
        public void CreateDomainForMaster(out IMasterServce master)
        {
             if (CountMaster > 0)
            {
                AppDomain masterDomain = AppDomain.CreateDomain("Mater's domain");
                var _master = masterDomain.CreateInstanceAndUnwrap("MyServiceLibrary",
                    "MyServiceLibrary.MasterService", false, BindingFlags.Default, null, new object[] { },
                    CultureInfo.CurrentCulture, null);
                master = (IMasterServce)_master;
                CountMaster--;
                masterService = master;
                 logger.Trace("Create master's domain");
            }
             else
             throw new CreateDomainException();
           
        }
        public void CreateDomainForSlave(out ISlaveService slave, ClientCommunication client)
        {
            if (CountSlave > 0 || CountMaster !=0)
            {
                AppDomain slaveDomain = AppDomain.CreateDomain(String.Format("Slave's domain{0}", CountSlave));
                var _slave = slaveDomain.CreateInstanceAndUnwrap("MyServiceLibrary",
                    "MyServiceLibrary.Services.SlaveUserService", false, BindingFlags.CreateInstance, null, null, null,
                    null);
                slave = (ISlaveService) _slave;
                CountSlave--;
                logger.Trace("Create slave's domain");
                slave.NotificationOnClient(client);
            }
            else
            {
                throw new CreateDomainException();
            }

            
            //masterService = null;

        }
    }
}
