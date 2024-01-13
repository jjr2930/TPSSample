using JLib.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class IngamePoolObject : DefaultPoolObject<PoolKey>
    {
        public override void OnReturned()
        {
            base.OnReturned();
            transform.SetParent(null);
        }
    }
}
