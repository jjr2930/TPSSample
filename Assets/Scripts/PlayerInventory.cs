using JLib;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
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

        public void OnPrimaryPressed(bool value)
        {
            var item = FindItem(CombatMode.Primary);
            if(null == item.instance)
            {
                var socket = TransformUtility.FindByName(transform, item.socketName);
                if(null == socket )
                {
                    Debug.Log("Can not found socket");
                    return;
                }

                item.instance = Instantiate(item.prefab, socket);
                item.instance.transform.localPosition = Vector3.zero;
                item.instance.transform.localRotation = Quaternion.identity;
            }

            item.instance.gameObject.SetActive(true);
            CurrentItem = item;
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
