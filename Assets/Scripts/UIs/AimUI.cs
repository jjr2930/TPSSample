using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TPSSample
{
    public class AimUI : MonoBehaviour
    {
        [SerializeField] RectTransform linesAnchor;
        [SerializeField] MinMaxFloat minMax;
        [SerializeField] float velocity;
        [SerializeField] float wideSpeed;
        [SerializeField] float narrowSpeed;
        [SerializeField] MinMaxInt widthMinMax;
        [SerializeField] MinMaxInt heightMinMax;

        public void Start()
        {
            EventContainer.onShotSuccessful += OnShotSuccessful;
        }

        public void Update()
        {
            velocity -= narrowSpeed * Time.deltaTime;
            var nextWidth = linesAnchor.sizeDelta.x + velocity;
            var nextHeight = linesAnchor.sizeDelta.y + velocity;
            
            nextWidth = Mathf.Clamp(nextWidth, widthMinMax.min, widthMinMax.max);
            nextHeight = Mathf.Clamp(nextHeight, heightMinMax.min, heightMinMax.max);

            linesAnchor.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nextWidth);
            linesAnchor.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, nextHeight);
        }

        public void OnDestroy()
        {
            EventContainer.onShotSuccessful -= OnShotSuccessful;
        }

        public void OnShotSuccessful(InventoryItem currentItem)
        {
            velocity = (Mathf.Abs(velocity) <= 0.001f) ? 1f : velocity + wideSpeed * Time.deltaTime;
            velocity = Mathf.Clamp(wideSpeed, minMax.min, minMax.max);
        }

        public void OnShotSuccessful()
        {
            velocity = (Mathf.Abs(velocity) <= 0.001f) ? 1f : velocity + wideSpeed * Time.deltaTime;
            velocity = Mathf.Clamp(wideSpeed, minMax.min, minMax.max);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AimUI))]
    public class AimUIEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Shot!"))
            {
                (target as AimUI).OnShotSuccessful();
            }
        }
    }
#endif
}
