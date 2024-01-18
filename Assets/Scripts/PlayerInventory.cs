using JLib;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TPSSample
{
    [Serializable]
    public class  InventoryItem
    {
        public CombatMode order;
        public PoolKey poolKey;
        public GameObject prefab;
        public GameObject instance;
        public string socketName;
        public float delay;
        public float range;
    }

    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] List<InventoryItem> items = new List<InventoryItem>();
        [SerializeField] InventoryItem currentItme;

        public UnityEvent<InventoryItem> onCurrentItemChanged;

        public InventoryItem CurrentItem 
        {
            get => currentItme;
            set
            {
                if(currentItme != value) 
                {
                    currentItme = value;
                    onCurrentItemChanged.Invoke(value);   
                }
            }
        }

        public void OnModeChanged(CombatMode mode)
        {
            switch (mode)
            {
                case CombatMode.Neutral:
                    {
                        DisableCurrentItem();
                        CurrentItem = null;
                    }
                    break;

                case CombatMode.Primary:
                    {
                        WearItem(CombatMode.Primary);
                    }
                    break;

                case CombatMode.Secondary:
                    {
                        WearItem(CombatMode.Secondary);
                    }
                    break;

                default:
                    break;
            }
        }

        private void WearItem(CombatMode mode)
        {
            DisableCurrentItem();

            var item = FindItem(mode);
            InstantiateIfNeed(item);

            item.instance.gameObject.SetActive(true);
            CurrentItem = item;
        }

        private void InstantiateIfNeed(InventoryItem item)
        {
            if (null == item.instance)
            {
                var socket = TransformUtility.FindByName(transform, item.socketName);
                if (null == socket)
                {
                    throw new InvalidOperationException($"there is no socket : {item.socketName}");
                }

                item.instance = Instantiate(item.prefab, socket);
                item.instance.transform.localPosition = Vector3.zero;
                item.instance.transform.localRotation = Quaternion.identity;
            }
        }

        private void DisableCurrentItem()
        {
            if (null != CurrentItem && null != CurrentItem.instance)
            {

                CurrentItem.instance.SetActive(false);
            }
        }

        public InventoryItem FindItem(CombatMode order) 
        {
            for (int i = 0; i < items.Count; i++)
            {
                InventoryItem item = items[i];
                if(order == item.order)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
