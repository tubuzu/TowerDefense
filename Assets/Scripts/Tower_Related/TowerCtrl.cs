// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Spawners;

namespace Tower_Related
{
    public class TowerCtrl : MyMonoBehaviour
    {
        [SerializeField] protected Transform model;
        public Transform Model => model;
        [SerializeField] protected TowerEnemyScanner enemyScanner;
        public TowerEnemyScanner EnemyScanner { get => enemyScanner; }
        [SerializeField] protected TowerWeapon towerWeapon;
        public TowerWeapon TowerWeapon => towerWeapon;
        [SerializeField] protected TowerStatus towerStatus;
        public TowerStatus TowerStatus => towerStatus;
        [SerializeField] protected TowerData towerData;
        public TowerData TowerData => towerData;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadModel();
            this.LoadTowerEnemyScanner();
            this.LoadTowerWeapon();
            this.LoadTowerData();
            this.LoadTowerStatus();
        }
        protected virtual void LoadModel()
        {
            if (this.model != null) return;
            this.model = transform.Find("Model").GetComponent<Transform>();
        }
        protected virtual void LoadTowerEnemyScanner()
        {
            if (this.enemyScanner != null) return;
            this.enemyScanner = transform.Find("EnemyScanner").GetComponent<TowerEnemyScanner>();
        }
        protected virtual void LoadTowerWeapon()
        {
            if (this.towerWeapon != null) return;
            this.towerWeapon = transform.Find("Weapon").GetComponent<TowerWeapon>();
        }

        protected virtual void LoadTowerData()
        {
            if (this.towerData != null) return;
            this.towerData = transform.Find("Data").GetComponent<TowerData>();
        }

        protected virtual void LoadTowerStatus()
        {
            if (this.towerStatus != null) return;
            this.towerStatus = transform.Find("Status").GetComponent<TowerStatus>();
        }

        public void RemoveTower()
        {
            TowerSpawner.Instance.Despawn(gameObject);
        }
    }
}