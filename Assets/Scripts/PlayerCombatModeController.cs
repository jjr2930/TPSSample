using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TPSSample
{
    public enum CombatMode
    {
        Neutral,
        Primary,
        Secondary,
    }

    public class UnityEventBool : UnityEvent<bool> { }
    
    public class PlayerCombatModeController : MonoBehaviour
    {
        [SerializeField] CombatMode mode;

        public UnityEvent<CombatMode> onModeChanged;
        public CombatMode Mode
        {
            set
            {
                if (value != mode)
                {
                    mode = value;
                    onModeChanged?.Invoke(mode);
                }
            }
        }

        public void OnPrimaryPressed(bool pressed)
        {
            if (pressed)
            {
                Mode = CombatMode.Primary;
            }
        }

        public void OnSecondaryPressed(bool pressed)
        {
            if(pressed)
            {
                Mode = CombatMode.Secondary;
            }
        }

        public void OnNeutralPressed(bool pressed)
        {
            if(pressed)
            {
                Mode = CombatMode.Neutral;
            }
        }
    }
}
