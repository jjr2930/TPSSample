using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TPSSample
{
    /// <summary>
    /// 여기서 성능저하가 생긴다면 singleton으로 해서 업데이트가 한번만 돌게 해주자.
    /// </summary>
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] float spawnDelay = 30f;
        [SerializeField] AssetReference monster = null;
        [SerializeField] float spawnDistance = 1f;
        [SerializeField] Transform lastSpanwedMonster = null;
        [SerializeField] float delayCheckingTime = 0f;
        [SerializeField] bool isSpawningStarted = false;
        public void Start()
        {
            StartCoroutine(SpawnWithDelay());   
        }

        public void OnDestroy()
        {
            StopAllCoroutines();
        }

        IEnumerator SpawnWithDelay()
        {
            while (true)
            {
                yield return null;
                if (null != lastSpanwedMonster 
                    && MathUtility.GetSqrDistancePlanar(transform.position, lastSpanwedMonster.position) <= spawnDistance * spawnDistance)
                {
                    yield return new WaitForSeconds(1f);
                    delayCheckingTime = Time.time;
                }
                else
                {
                    if (Time.time - delayCheckingTime >= spawnDelay
                        && false == isSpawningStarted)
                    {
                        isSpawningStarted = true;
                        monster.InstantiateAsync(this.transform.position, this.transform.rotation, null).Completed +=
                            (handler) =>
                            {
                                lastSpanwedMonster = handler.Result.transform;
                                isSpawningStarted = false;
                            };                        
                    }
                }
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
            Gizmos.DrawSphere(transform.position, spawnDistance);
        }

        public void OnDrawGizmosSelected()
        {
            if (null == lastSpanwedMonster)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, lastSpanwedMonster.position);
        }
    }
}
