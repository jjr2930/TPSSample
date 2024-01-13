using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class VFXPoolObject : IngamePoolObject
    {
        [SerializeField] float delay;
        public override void OnPoped()
        {
            base.OnPoped();
            if (delay > 0)
            {
                StartCoroutine(ReturnWithDelay());
            }
        }

        public override void OnReturned()
        {
            base.OnReturned();
            if (delay > 0)
            {
                StopAllCoroutines();
            }
        }

        IEnumerator ReturnWithDelay()
        {
            yield return new WaitForSeconds(delay);
            IngameObjectPool.Instance.ReturnOne(this.key, this);
        }
    }
}
