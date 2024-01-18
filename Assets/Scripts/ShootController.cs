using JLib;
using UnityEngine;

namespace TPSSample
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] bool firePressed;
        [SerializeField] InventoryItem currentItem;
        [SerializeField] float lastFiredTime;
        [SerializeField] Vector3 currentCameraPosition;
        [SerializeField] Vector3 currentCameraForward;
        [SerializeField] LayerMask raycastMask;
        [SerializeField] CombatMode combatMode;
        [SerializeField] bool debugMode;

        Transform debugSphere;
        public void Awake()
        {
            if (debugMode)
            {
                var debugObject = GameObject.CreatePrimitive( PrimitiveType.Sphere);
                debugObject.name = "debug sphere";
                debugObject.GetComponent<Renderer>().material.color = Color.red;
                debugObject.layer = LayerMask.NameToLayer("Me");
                debugSphere = debugObject.transform;
            }
        }
        public void Update()
        {
            if (firePressed)
            {
                switch (combatMode)
                {
                    case CombatMode.Primary:
                        if (null == currentItem)
                        {
                            Debug.Log("current item is null");
                            return;
                        }

                        if (Time.time - lastFiredTime < currentItem.delay)
                        {
                            //Debug.Log("it is delay time");
                            return;
                        }

                        lastFiredTime = Time.time;

                        Transform muzzleFlashTransform = null;
                        if (TransformUtility.TryFindByName(currentItem.instance.transform, "Muzzle Flash Position", ref muzzleFlashTransform))
                        {
                            var one = IngameObjectPool.Instance.PopOne(currentItem.poolKey, Vector3.zero, Quaternion.identity, muzzleFlashTransform, true);
                            one.transform.transform.localScale = Vector3.one;
                        }

                        Ray ray = new Ray(currentCameraPosition, currentCameraForward);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, currentItem.range, raycastMask.value))
                        {
                            if(debugMode)
                            {
                                Debug.Log($"hit!! : name : {hit.transform.name}, position : {hit.point}");
                                debugSphere.transform.position = hit.point;
                            }
                            int bulletHoleCount = 4;
                            int randomBulletHole = UnityEngine.Random.Range(0, bulletHoleCount);
                            PoolKey poolKey = PoolKey.BulletHoleConcrete1;
                            switch (randomBulletHole)
                            {
                                case 0:
                                    poolKey = PoolKey.BulletHoleConcrete1;
                                    break;

                                case 1:
                                    poolKey = PoolKey.BulletHoleConcrete2;
                                    break;

                                case 2:
                                    poolKey = PoolKey.BulletHoleConcrete3;
                                    break;

                                case 3:
                                    poolKey = PoolKey.BulletHoleConcrete4;
                                    break;

                                default:
                                    break;
                            }

                            var rotation = Quaternion.LookRotation(hit.normal);
                            IngameObjectPool.Instance.PopOne(poolKey, hit.point, rotation, null);
                            EventContainer.onShotSuccessful?.Invoke(currentItem);

                            //has characterData
                            CharacterData characterData = null;
                            if(hit.transform.TryGetComponent(out characterData))
                            {
                                characterData.OnDamaged(1);

                                Quaternion lookingRotation = Quaternion.LookRotation(-ray.direction, Vector3.up);
                                IngameObjectPool.Instance.PopOne(characterData.Data.hitVFXKey, hit.point, lookingRotation, hit.transform, false);
                            }
                        }
                        else
                        {
                            Debug.Log("not hit");
                        }
                        break;

                    default:
                        //Debug.Log($"{combatMode} not supported yet");
                        break;
                }
            }
        }

        public void OnFirePressed(bool value)
        {
            firePressed = value;
        }

        public void OnCurrentItemChanged(InventoryItem item)
        {
            //it should be neutral
            if(null == item)
            {
                combatMode = CombatMode.Neutral;
                currentItem = null;
            }
            else
            {
                combatMode = item.order;
                currentItem = item;
            }
        }

        public void OnCameraTransformChanged(Transform cameraTransform)
        {
            currentCameraPosition = cameraTransform.localPosition;
            currentCameraForward = cameraTransform.forward;
        }

    }
}
