// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace Tower_Related
{
    public class TowerAbstract : MyMonoBehaviour
    {
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
    }
}
