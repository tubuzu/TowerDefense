using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damage;
using Spawners;

[Serializable]
public class HitEffect
{
    public float duration; // Thời gian hiệu ứng
}

[Serializable]
public class SlowOnHitEffect : HitEffect
{
    public float magnitude; // Độ mạnh của hiệu ứng
}

namespace Tower_Related
{
    public class TowerWeapon : DamageSender
    {
        [SerializeField] private float fireRate;
        [SerializeField] private ProjectileType projectileType;
        [SerializeField] private float hitRadius;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private SlowOnHitEffect slowEffect;
        [SerializeField] private ExplodeEffectType explodeEffectType;

        private float _fireRateCounter = 0;

        [SerializeField] protected TowerCtrl towerCtrl;
        public TowerCtrl TowerCtrl { get { return towerCtrl; } }
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadTowerCtrl();
        }
        protected virtual void LoadTowerCtrl()
        {
            this.towerCtrl = transform.parent.GetComponent<TowerCtrl>();
            if (this.towerCtrl == null) this.towerCtrl = transform.GetComponent<TowerCtrl>();
        }

        private void Update()
        {
            if (_fireRateCounter <= 0)
            {
                if (this.towerCtrl.TowerStatus.CanFight && this.towerCtrl.EnemyScanner.IsTargetFound())
                    Shoot(this.towerCtrl.EnemyScanner.GetTarget());
            }
            else _fireRateCounter -= Time.deltaTime;
        }

        public void Shoot(Transform target)
        {
            if (_fireRateCounter <= 0)
            {
                GameObject projectileGO = ProjectileSpawner.Instance.Spawn(projectileType.ToString(), transform.parent.position, Quaternion.identity);
                Projectile projectile = projectileGO.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.SetTarget(target);
                    projectile.SetDamage(this.GetDamage());
                    projectile.SetHitRadius(this.hitRadius);
                    projectile.SetSpeed(this.projectileSpeed);
                    projectile.SetExplodeEffectType(this.explodeEffectType);
                    if (slowEffect.duration != 0)
                        projectile.AddHitEffects(new List<HitEffect> { slowEffect });
                }
                _fireRateCounter = fireRate;
            }
        }

        public void UpdateLevel(TowerLevelData level)
        {
            this.SetDamage(level.damage);
            this.fireRate = level.fireRate;
            this.hitRadius = level.hitRadius;
            this.projectileSpeed = level.projectileSpeed;
            this.projectileType = level.projectileType;
            this.explodeEffectType = level.explodeEffectType;
            this.slowEffect = level.slowEffect;
        }
    }
}