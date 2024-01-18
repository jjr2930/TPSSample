using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngineInternal;

namespace TPSSample
{
    public class ObjectInterator : MonoBehaviour
    {
        public UnityEvent<InteractableObjectUser> onTriggerEntered;
        public UnityEvent<InteractableObjectUser> onTriggerExit;
        public InteractableObjectUser currentInteractable;

        float elpasedTime;
        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out currentInteractable))
            {
                onTriggerEntered?.Invoke(currentInteractable);
                EventContainer.onInteractableObjectTriggerEntered?.Invoke(currentInteractable);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out currentInteractable))
            {
                onTriggerExit?.Invoke(currentInteractable);
                EventContainer.onInteractableObjectTriggerExit?.Invoke(currentInteractable);
                currentInteractable = null;
            }
        }

        public void OnInteraction(bool value)
        {
            if (null == currentInteractable)
                return;

            if (currentInteractable is LootableObject)
            {
                var lootableObject = currentInteractable as LootableObject;
                if (value)
                    lootableObject.Interact();
                else
                    lootableObject.Cancle();
            }
            //EventContainer.onInteraction?.Invoke(currentInteractable, value);
        }
    }
}
