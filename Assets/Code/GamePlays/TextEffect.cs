using System;
using TMPro;
using UnityEngine;

namespace Code.GamePlays
{
    public class TextEffect : MonoBehaviour
    {
        public event Action<TextEffect> OnEndLifeTime;
        
        public float lifeTime = 1f;
        public float moveUpSpeed = 40f;
        public TextMeshProUGUI text;

        private bool _isEndLife;
        
        public void Initialize(Vector3 position, string message, Color color)
        {
            transform.position = position;
            text.text = message;
            text.color = color;
            
            _isEndLife = false;
        }

        void Update()
        {
            transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
            Color c = text.color;
            c.a -= Time.deltaTime / lifeTime;
            text.color = c;

            if (c.a <= 0f && !_isEndLife)
            {
                _isEndLife = true;
                OnEndLifeTime?.Invoke(this);
            }
        }
    }
}
