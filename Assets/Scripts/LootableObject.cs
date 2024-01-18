using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class LootableObject : InteractableObjectUser
    {
        [Tooltip("루팅 정보가 있는 테이블")]
        [SerializeField]
        LootingRateTable table;

        [Tooltip("루팅 테이블에서 가져올 부분의 키 값")]
        [SerializeField]
        string key;

        [Tooltip("루팅하는데 얼마나 걸릴까?")]
        [SerializeField]
        float lootingDuration;

        [SerializeField]
        bool isOpened = false;

        LootingRateTableScheme data;

        float elpasedInteractingTime = 0f;
        public void Awake()
        {
            data = table[key];
        }

        public override void Interact()
        {
            base.Interact();
            elpasedInteractingTime += Time.deltaTime;
            EventContainer.onLootingPercentChanged?.Invoke(elpasedInteractingTime / lootingDuration);
            if(elpasedInteractingTime > lootingDuration) 
            {
                EventContainer.onLooted?.Invoke(this);
                isOpened = true;
                Debug.Log("Opened!");
            }
        }

        public override void Cancle()
        {
            base.Cancle();
            elpasedInteractingTime = 0f;
        }

        /// <summary>
        /// 테이블에 작성된 정보를 이용하여 랜덤한 아이템을 얻어온다.
        /// </summary>
        /// <returns>찾은 아이템의 키</returns>
        public int GetRandomItem()
        {
            var totalRate = data.totalRate;
            var randomRate = Random.Range(0, totalRate);
            var sum = 0;
            for (int i = 0; i < data.chanceInfos.Count; ++i)
            {
                if(sum <= randomRate && randomRate <= sum + data.chanceInfos[i].rate)
                {
                    return data.chanceInfos[i].itemTableKey;
                }
                sum += data.chanceInfos[i].rate;
            }

            return -1;
        }
    }
}