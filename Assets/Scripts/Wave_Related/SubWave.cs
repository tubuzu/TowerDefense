using System;
// using UnityEngine;

namespace Wave_Related
{
    [Serializable]
    public class SubWave
    {
        public EnemyType enemyType;
        public int numOfEnemies;
        public int pathIndex;

        public SubWave(EWaveDifficulty difficulty, EnemyType enemyType, int numOfEnemies)
        {
            this.enemyType = enemyType;
            this.numOfEnemies = numOfEnemies;
        }

        public EnemyType GetEnemyType()
        {
            return enemyType;
        }

        public int GetNumOfEnemies()
        {
            return numOfEnemies;
        }
    }
}
