using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.Packages;

namespace Net.PackageHandlers.ClientHandlers
{

    public class AcceptPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            NetEventStorage.GetInstance().acceptPackageRecieved.Invoke(pack);
        }
    }
}