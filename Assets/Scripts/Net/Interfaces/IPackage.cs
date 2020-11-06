using System.Net;
using Net.Utils;

namespace Net.Interfaces
{
    public interface IPackage
    {
        IPEndPoint IpEndPoint { get; set; }
        PackageType PackageType { get; }
        object Data { get; }
    }
}