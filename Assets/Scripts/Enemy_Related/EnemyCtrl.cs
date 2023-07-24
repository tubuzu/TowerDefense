// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Related
{
    public class EnemyCtrl : MyMonoBehaviour
    {
        [SerializeField] protected Transform model;
        public Transform Model => model;
        [SerializeField] protected EnemyStatus enemyStatus;
        public EnemyStatus EnemyStatus { get => enemyStatus; }
        [SerializeField] protected EnemyDamageSender enemyDamageSender;
        public EnemyDamageSender EnemyDamageSender { get => enemyDamageSender; }
        [SerializeField] protected EnemyMovement enemyMovement;
        public EnemyMovement EnemyMovement => enemyMovement;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadModel();
            this.LoadEnemyStatus();
            this.LoadEnemyDamageSender();
            this.LoadEnemyMovement();
        }
        protected virtual void LoadModel()
        {
            if (this.model != null) return;
            this.model = transform.Find("Model").GetComponent<Transform>();
        }
        protected virtual void LoadEnemyStatus()
        {
            if (this.enemyStatus != null) return;
            this.enemyStatus = transform.Find("Status").GetComponent<EnemyStatus>();
        }
        protected virtual void LoadEnemyDamageSender()
        {
            if (this.enemyDamageSender != null) return;
            this.enemyDamageSender = transform.Find("DamageSender").GetComponent<EnemyDamageSender>();
        }
        protected virtual void LoadEnemyMovement()
        {
            if (this.enemyMovement != null) return;
            this.enemyMovement = transform.Find("Movement").GetComponent<EnemyMovement>();
        }

        protected override void OnEnable()
        {
            EnemyManager.Instance.AddEnemy(this);
        }

        public void ApplyHitEffect(List<HitEffect> hitEffects)
        {
            foreach (HitEffect effect in hitEffects)
            {
                if (effect is SlowOnHitEffect)
                {
                    enemyMovement.ApplySlowEffect(effect as SlowOnHitEffect);
                }
            }
        }
    }
}
