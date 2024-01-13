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
        [SerializeField] bool primaryPressed;
        [SerializeField] bool secondaryPressed;
        [SerializeField] bool neutralPressed;

        public UnityEvent<bool> OnPrimaryChanged;
        public UnityEvent<bool> OnSecondaryChanged;
        public UnityEvent<bool> OnNeutralChanged;

        public bool PrimaryPressed
        {
            get => primaryPressed;
            set
            {
                if (value != primaryPressed )
                {
                    primaryPressed = value;
                    OnPrimaryChanged.Invoke(value);
                }
            }
        }
        public bool SecondaryPressed
        {
            get => secondaryPressed;
            set
            {
                if (value != secondaryPressed )
                {
                    secondaryPressed = value;
                    OnSecondaryChanged?.Invoke(value);
                }                
            }
        }
        public bool NeutralPressed
        {
            get => neutralPressed;
            set
            {
                if (value != neutralPressed)
                {
                    neutralPressed = value;
                    OnNeutralChanged.Invoke(value);
                }
            }
        }
    }
}
