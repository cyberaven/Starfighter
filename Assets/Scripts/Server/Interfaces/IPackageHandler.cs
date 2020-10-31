using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

namespace Server.Interfaces
{

    public interface IPackageHandler
    {
        //should inovke events about accept\decline if it's necessary
        Task Handle(IPackage pack);
    }
}
