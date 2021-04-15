using System;
using Core;
using Net.Interfaces;
using Net.Packages;

namespace Net.PackageHandlers
{
    public abstract class AbstractHandlerManager: Singleton<AbstractHandlerManager>, IDisposable
    {
        protected IPackageHandler AcceptHandler;
        protected IPackageHandler DeclineHandler;
        protected IPackageHandler EventHandler;
        protected IPackageHandler StateHandler;
        protected IPackageHandler ConnectHandler;
        protected IPackageHandler DisconnectHandler;
        public abstract void HandlePackage(AbstractPackage pack);

        public void Dispose()
        {
            
        }
    }
}