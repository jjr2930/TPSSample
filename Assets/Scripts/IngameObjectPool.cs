using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib;
using UnityEngine.Pool;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using JLib.ObjectPool;
using JLib.ObjectPool.Addressables;

namespace TPSSample
{
    public enum PoolKey
    {
        G28MuzzleFire,
        BulletHoleConcrete1,
        BulletHoleConcrete2,
        BulletHoleConcrete3,
        BulletHoleConcrete4,
        VFXZombieDamagedByBullet,
    }

    public class IngameObjectPool : DefaultObjectPoolWithAddressables<PoolKey, IngameObjectPool>
    {
        public DefaultPoolObject<PoolKey> PopOne(PoolKey key, Vector3 position, Quaternion rotation, Transform parent, bool isLocal = false)
        {
            var one = PopOne(key);
            one.transform.SetParent(parent);
            if (isLocal)
            {
                one.transform.localPosition = position;
                one.transform.localRotation = rotation;
            }
            else
            {
                one.transform.position = position;
                one.transform.rotation = rotation;
            }

            return one; 
        }
    }
}
