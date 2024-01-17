using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    [Serializable]
    public abstract class MinMax<T>
    {
        public T min;
        public T max;
        public abstract T GetRandom();
    }

    [Serializable]
    public class MinMaxFloat : MinMax<float>
    {        
        public override float GetRandom()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

    [Serializable]
    public class MinMaxInt : MinMax<int>
    {
        public override int GetRandom()
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
    }
}
