using Code.GamePlays;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UIs
{
    public class GameModeSelector : MonoBehaviour
    {
        [SerializeField] private Button[] modeButtons; 
    
        private EventBus _eventBus;
    
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
        
            for (int i = 0; i < modeButtons.Length; i++)
            {
                var index = i;
            
                modeButtons[i].onClick.RemoveAllListeners();
                modeButtons[i].onClick.AddListener(() => SelectMode(index));
            }
        }

        private void SelectMode(int modeIndex)
        {
            _eventBus.OnGameModeSelected?.Invoke((GameMode)modeIndex);
        }
    }
}
