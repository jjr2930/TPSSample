using System.Collections;
using System.Collections.Generic;
using JLib;
using UnityEngine;
using UnityEngine.Assertions;

namespace TPSSample
{
    public class DragAndDropPanelManager : MonoSingle<DragAndDropPanelManager>
    {
        [SerializeField]
        List<DragAndDropPanel> panels;

        public DragAndDropPanel GetPanelByIndex(int index)
        {
            Debug.Assert(null == panels, $"panels is null");
            Debug.Assert(0 <= index && index <= panels.Count, $"out of range, index :{index}, length :{panels.Count}");

            return panels[index];
        }
    }
}
