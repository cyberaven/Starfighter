using ScriptableObjects;
using UnityEngine;

namespace UnitBehavior
{
    public class DangerZone : MonoBehaviour
    {
        public DangerZoneConfig config;
        public Sprite sprite;

        private RectTransform _transform;
        private SpriteRenderer _renderer;

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
            _renderer = GetComponent<SpriteRenderer>();
            _transform = GetComponent<RectTransform>();

            _renderer.color = config.Color;
            _renderer.sprite = Instantiate(sprite);

            _transform.position = config.Center;
            _transform.localScale = Vector3.one * config.Radius;
            _transform.rotation = Quaternion.AngleAxis(90, Vector3.right);

        }

        // Лучше через события конечно менять (только при изменении значения проверять), но я пока не смог. Нужны триггеры какие-то.
        void Update()
        {
            _renderer.color = config.Color;
            _transform.position = config.Center;
            _transform.localScale = Vector3.one * config.Radius;
        }
    }
}
