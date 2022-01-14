using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ML.Scripts
{
    public class DNA : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer = default;

        [SerializeField]
        private Collider2D _collider2D = default;

        [SerializeField, Space]
        private float _r = 0;

        [SerializeField]
        private float _g = 0;

        [SerializeField]
        private float _b = 0;

        [SerializeField, Space]
        public float _timeToDie = 0;
        
        private bool _isDead = false;
        void OnMouseDown()
        {
            _isDead = true;
            Destroy(gameObject);
        }

        public void Init()
        {
            _r = Random.Range(0.0f, 1.0f);
            _g = Random.Range(0.0f, 1.0f);
            _b = Random.Range(0.0f, 1.0f);

            _spriteRenderer.color = new Color(_r, _g, _b);
        }
    }
}
