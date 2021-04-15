using System.Threading.Tasks;
using Net.Packages;

namespace Net.Interfaces
{

    public interface IPackageHandler
    {
        Task Handle(AbstractPackage pack);
    }
}
