// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave_Related;

namespace Enemy_Related
{
    public class EnemyManager : MyMonoBehaviour
    {
        //EVENTS
        public delegate void EnemyKilledDelegate(int amount);
        public event EnemyKilledDelegate OnEnemyKilledEvent;

        public delegate void AllEnemiesDeadDelegate(int waveBonus);
        public event AllEnemiesDeadDelegate OnAllEnemiesDeadEvent;
        public delegate void AllWavesCompleteDelegate();
        public event AllWavesCompleteDelegate OnAllWavesComplete;

        private List<EnemyCtrl> enemies;

        private static EnemyManager _instance;
        public static EnemyManager Instance => _instance;

        private EnemyManager() => enemies = new List<EnemyCtrl>(); //constructor

        [Header("Enemy State Color")]
        [SerializeField] public Color normalStateColor;
        [SerializeField] public Color hitStateColor;
        [SerializeField] public Color slowStateColor;
        [SerializeField] public Color stunStateColor;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        public void AddEnemy(EnemyCtrl enemy) => enemies.Add(enemy);

        public void RemoveEnemy(EnemyCtrl enemy)
        {
            enemies.Remove(enemy);
            OnEnemyKilledEvent?.Invoke(enemy.EnemyStatus.GetPoints());
            if (enemies.Count == 0 && WaveManager.Instance.IsLastWaveSpawned()) OnAllWavesComplete?.Invoke();
            else if (enemies.Count == 0)
            {
                OnAllEnemiesDeadEvent?.Invoke(Wave_Related.WaveManager.Instance.GetWaveBonus());
            }
        }

        public List<EnemyCtrl> GetEnemiesList() => enemies;

        public int GetEnemyCount() => enemies.Count;

        public void ClearEnemiesList() => enemies.Clear();
    }
}
