using System.Net;
using Net.Utils;

namespace Net.Interfaces
{
    public interface IPackage
    {
        IPAddress ipAddress { get; set; } //откуда пришел пакет\ обратный адрес
        PackageType PackageType { get; }
        object Data { get; }
    }
}