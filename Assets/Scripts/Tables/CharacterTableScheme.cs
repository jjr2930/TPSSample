using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib;
using System;

namespace TPSSample
{
    [Serializable]
    public class CharacterTableScheme : TableSchemeBase<string>
    {
        public int maxHp;
        public PoolKey hitVFXKey;
        public int attack;
    }
}
