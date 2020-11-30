using System.Threading.Tasks;

namespace Net.Interfaces
{

    public interface IPackageHandler
    {
        //should inovke events about accept\decline if it's necessary
        Task Handle(IPackage pack);
    }
}
