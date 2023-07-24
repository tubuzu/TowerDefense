using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Spawners;
using Managers;

namespace Enemy_Related
{
    public enum EnemyState
    {
        normal,
        hit,
        slow,
        stun,
    }
    public class EnemyStatus : EnemyAbstract
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int points = 50;

        private int _health;

        private HealthBar _healthBar;
        private SpriteRenderer _spriteRenderer;

        private EnemyState state = EnemyState.normal;

        protected override void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            _spriteRenderer = this.enemyCtrl.Model.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.ResetValues();
        }

        protected override void ResetValues()
        {
            base.ResetValues();
            state = EnemyState.normal;
            SetUpHealthBar();
        }

        protected virtual void FixedUpdate()
        {
            switch (state)
            {
                case EnemyState.hit:
                    if (_spriteRenderer.color != EnemyManager.Instance.hitStateColor)
                        _spriteRenderer.color = EnemyManager.Instance.hitStateColor;
                    break;
                case EnemyState.slow:
                    if (_spriteRenderer.color != EnemyManager.Instance.slowStateColor)
                        _spriteRenderer.color = EnemyManager.Instance.slowStateColor;
                    break;
                case EnemyState.stun:
                    if (_spriteRenderer.color != EnemyManager.Instance.stunStateColor)
                        _spriteRenderer.color = EnemyManager.Instance.stunStateColor;
                    break;
                default:
                    if (_spriteRenderer.color != EnemyManager.Instance.normalStateColor)
                        _spriteRenderer.color = EnemyManager.Instance.normalStateColor;
                    break;
            }
        }

        private void SetUpHealthBar()
        {
            _health = maxHealth;
            _healthBar.SetMaxHealthValue(_health);
        }

        //getters setters
        public int GetPoints() => points;

        public void TakeDamage(int dmg)
        {
            _health -= dmg;
            _healthBar.SetHealthBarValue(_health);
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                AudioManager.Instance.PlayEnemyHitSfx();
                StopCoroutine(nameof(FlashDamage));
                StartCoroutine(FlashDamage());
            }
        }

        private IEnumerator FlashDamage()
        {
            state = EnemyState.hit;
            yield return new WaitForSecondsRealtime(0.2f);
            state = EnemyState.normal;
        }

        public void Die()
        {
            FXSpawner.Instance.Spawn(DeathEffectType.BloodEffect.ToString(), transform.parent.position, Quaternion.identity);
            AudioManager.Instance.PlayEnemyDeathSfx();
            EnemyManager.Instance.RemoveEnemy(this.enemyCtrl);
            EnemySpawner.Instance.Despawn(this.enemyCtrl.gameObject);
        }

        public void ReachedEndPath()
        {
            EnemyManager.Instance.RemoveEnemy(this.enemyCtrl);
            EnemySpawner.Instance.Despawn(this.enemyCtrl.gameObject);
        }

        public bool IsAlive() => _health > 0;

        public void SetStatus(EnemyState newState)
        {
            if (this.state != newState && this.state != EnemyState.hit)
                this.state = newState;
        }
    }
}