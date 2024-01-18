using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public static class EventContainer 
    {
        public delegate void OnInteractionDelegate(InteractableObject interatable, bool isPressed);

        public static Action<InventoryItem> onShotSuccessful;
        public static Action<InteractableObjectUser> onInteractableObjectTriggerEntered;
        public static Action<InteractableObjectUser> onInteractableObjectTriggerExit;
        public static OnInteractionDelegate onInteraction;
        public static Action<LootableObject> onLooted;
        public static Action<float> onLootingPercentChanged;
    }
}
