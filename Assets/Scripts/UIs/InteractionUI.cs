using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TPSSample
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] Image fillImage;
        [SerializeField] InteractableObject currentInteractable;

        public void Awake()
        {
            EventContainer.onInteractableObjectTriggerEntered += OnInteractTriggerEntered;
            EventContainer.onInteractableObjectTriggerExit += OnInteractTriggerExit;
            EventContainer.onLootingPercentChanged += OnLootingPercentChanged;

            gameObject.SetActive(false);
        }

        public void OnDestroy()
        {
            EventContainer.onInteractableObjectTriggerEntered -= OnInteractTriggerEntered;
            EventContainer.onInteractableObjectTriggerExit -= OnInteractTriggerExit;
            EventContainer.onLootingPercentChanged -= OnLootingPercentChanged;
        }

        private void OnLootingPercentChanged(float obj)
        {
            fillImage.fillAmount = obj;
        }

        public void OnInteractTriggerEntered(InteractableObject interactable)
        {
            currentInteractable = interactable;
            gameObject.SetActive(true);
        }

        public void OnInteractTriggerExit(InteractableObject interactable)
        {
            if (currentInteractable == interactable)
            {
                currentInteractable = null;
                gameObject.SetActive(false);
            }
        }
    }
}
