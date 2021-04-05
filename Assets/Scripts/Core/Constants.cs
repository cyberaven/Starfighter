namespace Core
{
    public class Constants
    {
        public const string MulticastAddress = "239.255.255.250";
        
        public const int ServerReceivingPort = 40001;
        public const int ServerSendingPort = 40000;

        public const string PathToPrefabs = "Prefabs/";
        public const string PathToAxes = "ScriptableObjects/Axes/";
        public const string PathToAccounts = "ScriptableObjects/Accounts/";

        //объекты, передаваемые в пакетах State от сервера, объекты контролируемые сервером
        public const string DynamicTag = "Dynamic";
        public const string AsteroidTag = "Asteroid";
        
        //Настройки камеры
        public const int ZoomStep = 1000;
    }
}