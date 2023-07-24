using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Wave_Related;
using Enemy_Related;

namespace Spawners
{
    public class EnemySpawner : Spawner
    {
        private static EnemySpawner instance;
        public static EnemySpawner Instance => instance;

        protected override void Awake()
        {
            instance = this;
        }

        public void SpawnEnemy(SubWave currSubWave)
        {
            StartCoroutine(SpawnAnimationEffect(currSubWave));
        }

        public GameObject GetRandomEnemy()
        {
            return this.prefabs[Random.Range(0, this.prefabs.Count)];
        }

        private IEnumerator SpawnAnimationEffect(SubWave currSubWave)
        {
            Vector2 position = PathManager.Instance.paths[currSubWave.pathIndex].startPoint.position;
            FXSpawner.Instance.Spawn(SpawnEffectType.SpawnEffect.ToString(), position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            GameObject enemy = Spawn(currSubWave.GetEnemyType().ToString(), position, Quaternion.identity);
            enemy.GetComponentInChildren<EnemyFollowPath>().SetPath(currSubWave.pathIndex);
        }
    }
}
