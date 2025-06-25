using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Slider slider;
        [SerializeField] private Transform headTransform;

        private EventBus _eventBus;
    
        private float _health;
        private float _maxHealth;
        private float _armor;
        private float _damage;
        private float _vampire;

        private int _team;
        
        public bool IsAlive => _health > 0;

        public void Initialize(CharacterData characterData, EventBus eventBus, int team)
        {
            _team = team;
            
            var oldHealth = _health;
            
            _health = _maxHealth = characterData.Stats.FirstOrDefault(x => x.id == 0)!.value;
            _armor = characterData.Stats.FirstOrDefault(x => x.id == 1)!.value;
            _damage = characterData.Stats.FirstOrDefault(x => x.id == 2)!.value;
            _vampire = characterData.Stats.FirstOrDefault(x => x.id == 3)!.value;
        
            _eventBus = eventBus;
            slider.maxValue = _maxHealth;

            UpdateHealth();
            CallSendMessage(_health - oldHealth);
        }
    
        public void CallAttack()
        {
            if (animator == null) return;
        
            animator.SetTrigger("Attack");
        }

        public void TakeDamage(float damage)
        {
            if (damage == 0) return;
        
            _health = Mathf.Clamp(_health + damage, 0f, _maxHealth);

            UpdateHealth();
            CallSendMessage(damage);
        }

        private void UpdateHealth()
        {
            slider.value = _health;
            slider.gameObject.SetActive(_health > 0);
            animator.SetInteger("Health", (int)_health);
        }
        
        private void CallSendMessage(float damage)
        {
            if (damage == 0) return;
            
            var damageStatus = damage > 0 ? "+" : "-";
            var message = $"{damageStatus} {Mathf.Abs(Mathf.CeilToInt(damage)).ToString(CultureInfo.InvariantCulture)}";
            _eventBus.OnCallSendMessage?.Invoke(headTransform.transform.position, message, damage > 0 ? Color.green : Color.red);
            _eventBus.OnChangeHealth?.Invoke(_team, _health);
        }

        public float GetBaseDamage() => _damage;
        public float CalculateRealDamage(float damage) => damage - damage * (_armor / 100f);
        public float CalculateVampire(float damage) => damage * (_vampire / 100f);
    }
}
