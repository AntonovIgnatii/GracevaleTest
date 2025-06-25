using System.Collections.Generic;
using Code.Characters;
using UnityEngine;

namespace Code.UIs
{
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField] private PlayerPanelHierarchy[] playerPanels;
        [SerializeField] private GameModeSelector gameModeSelector;
    
        public void Initialize(List<CharacterData> charactersData, EventBus eventBus)
        {
            for (int i = 0; i < charactersData.Count; i++)
            {
                var index = i;
                playerPanels[i].Initialize(charactersData[i], eventBus, index);
            }
        
            gameModeSelector.Initialize(eventBus);
        }
    }
}
