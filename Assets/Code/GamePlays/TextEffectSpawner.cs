using UnityEngine;

namespace Code.GamePlays
{
    public class TextEffectSpawner : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private TextEffect textEffectPrefab;
        [SerializeField] private Canvas canvas;
    
        /// <summary>
        /// Показывает всплывающий текст над персонажем
        /// </summary>
        public void ShowText(Vector3 worldPosition, string message, Color color)
        {
            var screenPosition = camera.WorldToScreenPoint(worldPosition + Vector3.up * 1.5f);
            var textObj = Instantiate(textEffectPrefab, canvas.transform);
        
            textObj.Initialize(screenPosition, message, color);
        }
    }
}
