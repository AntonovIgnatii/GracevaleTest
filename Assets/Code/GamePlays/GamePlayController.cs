using System.Collections.Generic;
using Code.Characters;
using UnityEngine;

namespace Code.GamePlays
{
    public class GamePlayController : MonoBehaviour
    {
        [field: SerializeField] public Character[] Characters { get; private set; }
    
        public void Initialize(List<CharacterData> charactersData, EventBus eventBus)
        {
            eventBus.OnCallPlayerAttack -= CallCharacterAttack;
            eventBus.OnCallPlayerAttack += CallCharacterAttack;
        
            for (int i = 0; i < charactersData.Count; i++)
            {
                var index = i;
                Characters[i].Initialize(charactersData[i], eventBus, index);
            }
        }

        private void CallCharacterAttack(CharacterTeam characterTeam)
        {
            if (!Characters[characterTeam.AllyId].IsAlive || !Characters[characterTeam.EnemyId].IsAlive) return;
        
            var baseDamage = Characters[characterTeam.AllyId].GetBaseDamage();
            var realDamage = Characters[characterTeam.EnemyId].CalculateRealDamage(baseDamage);
            var vampireDamage = Characters[characterTeam.EnemyId].CalculateVampire(realDamage);
        
            Characters[characterTeam.AllyId].CallAttack();
            Characters[characterTeam.AllyId].TakeDamage(vampireDamage);
            Characters[characterTeam.EnemyId].TakeDamage(-realDamage);
        }
    }
}
