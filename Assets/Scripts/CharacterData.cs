using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TPSSample
{
    public class CharacterData : MonoBehaviour
    {
        [SerializeField] CharacterTable characterTable;
        [SerializeField] string myKey;
        [SerializeField] CharacterTableScheme data;
        [SerializeField] int currentHP;
        
        public UnityEvent<int> onHpChanged;
        public UnityEvent onDead;

        public CharacterTableScheme Data 
        { 
            get => data; 
        }

        public void Awake()
        {
            data = characterTable[myKey];
        }

        public void OnEnable()
        {
            currentHP = data.maxHp;
        }

        public void OnDamaged(int damage)
        {
            if (damage != 0)
            {
                currentHP -= damage;
                currentHP = Mathf.Clamp(currentHP, 0, data.maxHp);
                if (currentHP <= 0)
                {
                    onDead?.Invoke();
                }
                else
                {
                    onHpChanged?.Invoke(currentHP);
                }
            }
        }
    }
}
