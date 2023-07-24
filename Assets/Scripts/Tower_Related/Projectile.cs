// using System;
using System.Collections.Generic;
using Enemy_Related;
using UnityEngine;
using Spawners;

namespace Tower_Related
{
    public class Projectile : MyMonoBehaviour
    {
        private float speed = 5f;
        private List<HitEffect> _hitEffects = new List<HitEffect>();
        private float _hitRadius = 0f;

        private Rigidbody2D _rb;
        private Transform _target;
        private int _damage;

        private ExplodeEffectType _explodeEffectType;

        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MoveProjectile();
        }

        public void AddHitEffects(List<HitEffect> hitEffects)
        {
            foreach (HitEffect hitEffect in hitEffects)
            {
                this._hitEffects.Add(hitEffect);
            }
        }

        public void SetHitRadius(float radius) => _hitRadius = radius;
        public void SetSpeed(float speed) => this.speed = speed;
        public void SetTarget(Transform target) => _target = target;
        public void SetDamage(int damage) => _damage = damage;
        public void SetExplodeEffectType(ExplodeEffectType type) => _explodeEffectType = type;

        private void MoveProjectile()
        {
            if (_target.gameObject.activeSelf)
            {
                transform.LookAt(_target.position);
                _rb.velocity = transform.forward * speed;
                Vector2 dir = _target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                ProjectileSpawner.Instance.Despawn(gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyCtrl curEnemy = collision.gameObject.GetComponent<EnemyCtrl>();
            if (curEnemy == null) return;
            SendDamage(curEnemy);

            if (_hitRadius > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _hitRadius);

                foreach (Collider2D collider in colliders)
                {
                    EnemyCtrl enemy = collider.gameObject.GetComponent<EnemyCtrl>();
                    if (enemy == null || enemy == curEnemy) continue;
                    SendDamage(enemy);
                }
            }

            float scale = (_hitRadius > 1) ? _hitRadius : 1;
            GameObject explodeEffect = FXSpawner.Instance.Spawn(_explodeEffectType.ToString(), curEnemy.transform.position, Quaternion.identity);
            explodeEffect.transform.localScale = Vector3.one * scale;
        }

        protected virtual void SendDamage(EnemyCtrl enemy)
        {
            enemy.EnemyStatus.TakeDamage(_damage);
            if (_hitEffects.Count > 0 && enemy.EnemyStatus.IsAlive()) enemy.ApplyHitEffect(_hitEffects);
            ProjectileSpawner.Instance.Despawn(gameObject);
        }
    }
}