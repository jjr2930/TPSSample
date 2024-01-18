using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

namespace TPSSample.Editor
{
    [CustomEditor(typeof(LootingRateTable))]
    public class LootingTableEditor : UnityEditor.Editor  
    {
        LootingRateTable Script { get => target as LootingRateTable;  }
        public override void OnInspectorGUI()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();

                var count = Script.Count;
                for (var i = 0; i<count; ++i)
                {
                    var one = Script.GetOne(i);
                    int totalRate = 0;
                    for(int j = 0; j< one.chanceInfos.Count; ++j)
                    {
                        totalRate += one.chanceInfos[j].rate;
                    }

                    one.totalRate = totalRate;
                }

                EditorUtility.SetDirty(target);
            }
        }
    }
}
