using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TPSSample
{
    /// <summary>
    /// 좀 더 일반화 시킬 필요가 있어보인다.
    /// </summary>
    public class ZombieAnimatorListener : MonoBehaviour
    {
        public UnityEvent onAttacked;
        public void OnAttack()
        {
            onAttacked?.Invoke();
        }
    }
}
