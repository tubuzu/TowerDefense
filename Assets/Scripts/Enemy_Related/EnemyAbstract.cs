// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Related
{
    public class EnemyAbstract : MyMonoBehaviour
    {
        [SerializeField] protected EnemyCtrl enemyCtrl;
        public EnemyCtrl EnemyCtrl { get { return enemyCtrl; } }
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadEnemyCtrl();
        }
        protected virtual void LoadEnemyCtrl()
        {
            this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
            if (this.enemyCtrl == null) this.enemyCtrl = transform.GetComponent<EnemyCtrl>();
        }
    }
}
