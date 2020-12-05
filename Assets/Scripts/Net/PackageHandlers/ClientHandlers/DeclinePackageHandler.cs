using System.Threading.Tasks;
using Net.Interfaces;
using Net.Packages;

namespace Net.PackageHandlers.ClientHandlers
{
    public class DeclinePackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            throw new System.NotImplementedException();
        }
    }
}