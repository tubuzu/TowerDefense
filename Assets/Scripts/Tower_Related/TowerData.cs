using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tower_Related
{
    [Serializable]
    public class TowerLevelData
    {
        public float scanRange;

        public int damage;

        public float fireRate;
        public float hitRadius;
        public float projectileSpeed;

        public ProjectileType projectileType;
        public ExplodeEffectType explodeEffectType;

        public SlowOnHitEffect slowEffect;

        public int cost;
        public int upgradeCost;
    }
    public class TowerData : TowerAbstract
    {
        private int _currentLevel = 0;

        [SerializeField] private List<TowerLevelData> levelsData;

        protected override void OnEnable()
        {
            this._currentLevel = 0;
            ApplyLevelData();
        }

        public bool CanBeUpgrade { get => _currentLevel < levelsData.Count - 1; }

        public int GetCost { get => levelsData[_currentLevel].cost; }
        public int GetTotalCost()
        {
            int total = 0;
            for (int i = 0; i <= _currentLevel; i++)
            {
                total += levelsData[i].cost;
            }
            return total;
        }
        public int GetUpgradeCost { get => levelsData[_currentLevel].upgradeCost; }

        public int GetCurrenteLevel { get => _currentLevel; }

        public bool LevelUp()
        {
            if (_currentLevel >= levelsData.Count - 1) return false;
            _currentLevel++;
            ApplyLevelData();
            return true;
        }

        public void ApplyLevelData()
        {
            TowerLevelData newLevel = levelsData[_currentLevel];
            this.towerCtrl.EnemyScanner.UpdateLevel(newLevel);
            this.towerCtrl.TowerWeapon.UpdateLevel(newLevel);
        }
    }
}
