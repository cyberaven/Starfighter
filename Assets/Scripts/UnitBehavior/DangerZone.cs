using ScriptableObjects;
using UnityEngine;

namespace UnitBehavior
{
    public class DangerZone : MonoBehaviour
    {
        public DangerZoneConfig config;
        public Sprite sprite;

        private RectTransform transform;
        private SpriteRenderer renderer;

        void Start()
        {
            //config.ValueChange.AddListener(OnValueChange);
        }

        //private void OnValueChange(string field)
        //{
        //    switch (field)
        //    {
        //        case "Center":
        //            transform.position = config.Center; break;
        //        case "Radius":
        //            transform.localScale = Vector3.one * config.Radius; break;
        //        case "Color":
        //            renderer.color = config.Color; break;
        //        default: break;
        //    }
        //}

        void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            transform = GetComponent<RectTransform>();

            renderer.color = config.Color;
            renderer.sprite = Instantiate(sprite);

            transform.position = config.Center;
            transform.localScale = Vector3.one * config.Radius;
            transform.rotation = Quaternion.AngleAxis(90, Vector3.right);

        }

        // Лучше через события конечно менять (только при изменении значения проверять), но я пока не смог. Нужны триггеры какие-то.
        void Update()
        {
            renderer.color = config.Color;
            transform.position = config.Center;
            transform.localScale = Vector3.one * config.Radius;
        }
    }
}
