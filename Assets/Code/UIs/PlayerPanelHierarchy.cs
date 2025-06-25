using System.Collections.Generic;
using Code.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UIs
{
    public class PlayerPanelHierarchy : MonoBehaviour
    {
        [SerializeField] private CharacterTeam characterTeam;
        [SerializeField] private Button attackButton;
        [SerializeField] private Transform statsPanel;
        [SerializeField] private StatPanel originalPanel;

        private List<StatPanel> _createdStatPanels = new ();
    
        private EventBus _eventBus;
    
        public void Initialize(CharacterData characterData, EventBus eventBus, int team)
        {
            _eventBus = eventBus;
        
            attackButton.onClick.RemoveAllListeners();
            attackButton.onClick.AddListener(CallAttack);
        
            foreach (var createdStatPanel in _createdStatPanels)
            {
                eventBus.OnChangeHealth -= createdStatPanel.UpdateByChangeValue;
                
                Destroy(createdStatPanel.gameObject);
            }
        
            _createdStatPanels.Clear();
            
            if (characterData is { Stats: not null })
            {
                foreach (var stat in characterData.Stats)
                {
                    var statPanel = Instantiate(originalPanel, statsPanel);
                    statPanel.gameObject.SetActive(true);
                    statPanel.SetTeam(team);
                    statPanel.UpdateByStat(stat);

                    if (stat.id == 0)
                    {
                        eventBus.OnChangeHealth -= statPanel.UpdateByChangeValue;
                        eventBus.OnChangeHealth += statPanel.UpdateByChangeValue;
                    }
                    
                    _createdStatPanels.Add(statPanel);
                }
            }

            if (characterData is not { Buffs: not null }) return;
           
            foreach (var buff in characterData.Buffs)
            {
                var statPanel = Instantiate(originalPanel, statsPanel);
                statPanel.gameObject.SetActive(true);
                statPanel.SetTeam(team);
                statPanel.UpdateByBuff(buff);
                _createdStatPanels.Add(statPanel);
            }
        }

        private void CallAttack() => _eventBus.OnCallPlayerAttack?.Invoke(characterTeam);
    }
}
