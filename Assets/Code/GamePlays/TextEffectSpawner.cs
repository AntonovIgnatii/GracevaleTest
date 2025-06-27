using UnityEngine;
using UnityEngine.Pool;

namespace Code.GamePlays
{
    public class TextEffectSpawner : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private TextEffect textEffectPrefab;
        [SerializeField] private Canvas canvas;
        
        private ObjectPool<TextEffect> _textEffectPool;

        public void Initialize()
        {
            _textEffectPool = new ObjectPool<TextEffect>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, 100);
        }
        
        private TextEffect CreatePooledItem()
        {
            return Instantiate(textEffectPrefab, canvas.transform);
        }
        
        private void OnReturnedToPool(TextEffect textEffect)
        {
            textEffect.gameObject.SetActive(false);
        }
        
        private void OnTakeFromPool(TextEffect textEffect)
        {
            textEffect.gameObject.SetActive(true);
        }
        
        private void OnDestroyPoolObject(TextEffect textEffect)
        {
            Destroy(textEffect.gameObject);
        }

        private void ReturnToPool(TextEffect textEffect)
        {
            _textEffectPool.Release(textEffect);
        }
        
        public void ShowText(Vector3 worldPosition, string message, Color color)
        {
            var textObj = _textEffectPool.Get();
            var screenPosition = camera.WorldToScreenPoint(worldPosition + Vector3.up * 1.5f);
        
            textObj.OnEndLifeTime -= ReturnToPool;
            textObj.OnEndLifeTime += ReturnToPool;
            
            textObj.Initialize(screenPosition, message, color);
        }
    }
}
