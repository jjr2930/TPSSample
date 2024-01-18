using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib;
using System;

namespace TPSSample
{
    [Serializable]
    public class LootingRateTableScheme : TableSchemeBase<string>
    {
        [Serializable]
        public class ChanceInfo
        {
            public int rate;
            public int itemTableKey;
            public MinMaxInt countMinMax;
        }

        public List<ChanceInfo> chanceInfos = new List<ChanceInfo>();
        public int totalRate;
    }

    [CreateAssetMenu(menuName = "Tables/Looting Rate Table")]
    public class LootingRateTable : TableBase<string, LootingRateTableScheme>
    {
    }
}
