using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public interface IHP 
    {
        public int GetMaxHP();
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
    }
}
