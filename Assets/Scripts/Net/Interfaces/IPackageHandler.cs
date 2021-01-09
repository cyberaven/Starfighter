using System.Threading.Tasks;
using Net.Packages;

namespace Net.Interfaces
{

    public interface IPackageHandler
    {
        //should invoke events about accept\decline if it's necessary
        Task Handle(AbstractPackage pack);
    }
}
