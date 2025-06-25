using TMPro;
using UnityEngine;

namespace Code.GamePlays
{
    public class TextEffect : MonoBehaviour
    {
        public float lifeTime = 1f;
        public float moveUpSpeed = 40f;
        public TextMeshProUGUI text;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        public void Initialize(Vector3 position, string message, Color color)
        {
            transform.position = position;
            text.text = message;
            text.color = color;
        }

        void Update()
        {
            transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
            Color c = text.color;
            c.a -= Time.deltaTime / lifeTime;
            text.color = c;
        }
    }
}
