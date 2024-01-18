using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class InteractableObject : MonoBehaviour
    {
        /// <summary>
        /// 상호작용하기
        /// </summary>
        public virtual void Interact() { }
        /// <summary>
        /// 상호작용 중지
        /// </summary>
        public virtual void Cancle() { }
    }
}
