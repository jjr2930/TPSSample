using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class DragAndDropItem : MonoBehaviour
    {
        public static DragAndDropItem current;

        [SerializeField] int panelIndex;
        [SerializeField] DragAndDropPanel panel;

        DragAndDropContainer hoveredContainer;
        Transform originParent;
        int originSiblingIndex;


        // Update is called once per frame
        void Update()
        {
            if (this != current)
            {
                if (IsDragStarted())
                {
                    if(null == panel)
                    {
                        panel = DragAndDropPanelManager.Instance.GetPanelByIndex(panelIndex);
                    }

                    current = this;
                    originParent = transform.parent;
                    originSiblingIndex = transform.GetSiblingIndex();
                    hoveredContainer = null;
                    
                }
            }
            else
            {
                transform.position = GetLastDragingWorldPoint();
                IsHoveredContainer(ref hoveredContainer);

                if (IsDragFinihsed())
                {
                    if (null != hoveredContainer)
                    {
                        this.transform.SetParent(hoveredContainer.transform);
                    }
                    else
                    {
                        this.transform.SetParent(originParent);
                        this.transform.SetSiblingIndex(originSiblingIndex);
                    }
                }
            }
        }

        private bool IsDragFinihsed()
        {
            throw new NotImplementedException();
        }

        private void IsHoveredContainer(ref DragAndDropContainer hoveredContainer)
        {
            throw new NotImplementedException();
        }

        private Vector3 GetLastDragingWorldPoint()
        {
            throw new NotImplementedException();
        }

        private bool IsDragStarted()
        {
            throw new NotImplementedException();
        }
    }
}
