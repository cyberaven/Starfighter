using System.Threading.Tasks;
using Net.Interfaces;
using Net.Packages;

namespace Net.PackageHandlers.ClientHandlers
{

    public class AcceptPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            //TODO: Accept event with pack.Id
        }
    }
}