using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class FSMEventNames : MonoBehaviour
    {
        public class Zombie
        {
            public const string OnEnemyFound = "OnEnemyFound";
            public const string OnDead = "OnDead";
            public const string OnEnemyLost = "OnEnemyLost";
            public const string OnArrived = "OnArrived";
            public const string OnOutOfRange = "OnOutOfRange";
        }
    }
}
