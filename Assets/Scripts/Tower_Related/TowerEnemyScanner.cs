using System;
using System.Collections.Generic;
using Enemy_Related;
using UnityEngine;

namespace Tower_Related
{
    public class TowerEnemyScanner : TowerAbstract
    {
        [SerializeField] private float scanRange;

        private Transform _target;
        private GameObject rangeImage;

        protected override void Awake()
        {
            base.Awake();

            this.rangeImage = transform.Find("RangeImage").gameObject;
            rangeImage.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();

            this.towerCtrl.TowerStatus.OnCanFightChanged += OnCanFightChanged;
            this.towerCtrl.TowerStatus.OnIsSelectingChanged += OnIsSelectingChanged;
            this.towerCtrl.TowerStatus.CanFight = false;

            InvokeRepeating(nameof(ScanEnemiesInRange), 0f, 0.1f);
        }

        private void OnValidate()
        {
            UpdateScanRange();
        }

        private void UpdateScanRange()
        {
            transform.localScale = Vector3.one * scanRange;
        }

        public void ScanEnemiesInRange()
        {
            Transform targetEnemy = null;

            List<EnemyCtrl> enemies = EnemyManager.Instance.GetEnemiesList();

            foreach (EnemyCtrl enemy in enemies)
            {
                float currDistanceFromEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if (targetEnemy == null && currDistanceFromEnemy <= scanRange)
                {
                    targetEnemy = enemy.transform;
                }
            }

            _target = targetEnemy;
        }

        public bool IsTargetFound()
        {
            return _target != null;
        }

        public Transform GetTarget()
        {
            return _target;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, scanRange);
        }

        public void OnCanFightChanged(bool can)
        {
            // Debug.Log("OnCanFightChanged");
            ToggleRangeImage(!can);
        }

        public void OnIsSelectingChanged(bool isSelecting)
        {
            ToggleRangeImage(isSelecting);
        }

        public void ToggleRangeImage(bool active)
        {
            rangeImage.SetActive(active);
        }

        public void UpdateLevel(TowerLevelData level)
        {
            this.scanRange = level.scanRange;
            UpdateScanRange();
        }
    }
}