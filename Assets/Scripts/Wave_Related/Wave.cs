using System;
using System.Collections.Generic;
// using UnityEngine;

namespace Wave_Related
{
    [Serializable]
    public class Wave
    {
        public List<SubWave> subWaves;
        public float spawnRate;
        public float timeBetweenSubWaves;
        public int waveBonus;

        //Normal Wave
        public Wave(List<SubWave> subWaves, float spawnRate, float timeBetweenSubWaves)
        {
            this.subWaves = subWaves;
            this.spawnRate = spawnRate;
            this.timeBetweenSubWaves = timeBetweenSubWaves;
        }

        //Boss Wave
        public Wave(SubWave bossSubWave)
        {
            subWaves = new List<SubWave>();
            subWaves.Add(bossSubWave);
            spawnRate = 0;
            timeBetweenSubWaves = 1;
        }

        public float GetSpawnRate()
        {
            return spawnRate;
        }

        public List<SubWave> GetSubWaves()
        {
            return subWaves;
        }

        public float GetTimeBetweenSubWaves()
        {
            return timeBetweenSubWaves;
        }
    }
}


