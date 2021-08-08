using UnityEngine;

namespace Client.UI
{
    public class BasePlayerUIElement: MonoBehaviour
    {
        protected PlayerScript PlayerScript;

        public void Init(PlayerScript playerScript)
        {
            PlayerScript = playerScript;
        }
    }
}