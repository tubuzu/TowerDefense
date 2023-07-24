using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nodes;
// using Managers;
using Tower_Related;
using Spawners;

namespace Managers
{
    public class BuildManager : MyMonoBehaviour
    {
        #region Singleton

        private static BuildManager _instance;
        public static BuildManager Instance => _instance;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        #endregion

        [SerializeField] private TowerDialog towerDialog;
        [SerializeField] private bool rememberSelection; //for debugging purposes

        private Node _nodeSelected;
        private TowerType _towerToBuild = TowerType.NONE;
        private float _offset = 0.5f; //to center sprite in square

        private MouseItem _mouseItem;

        protected override void Start()
        {
            base.Start();
            _towerToBuild = TowerType.NONE;
            towerDialog.HideTowerDialog();
            _mouseItem = GetComponent<MouseItem>();
        }

        public void SetNodeSelected(Node node)
        {
            _nodeSelected = node;
        }

        public void DeselectTowerToManage()
        {
            _mouseItem.ReleaseTowerFromMouse();
            _towerToBuild = TowerType.NONE;
        }

        //gets called from the Shop Script (from the UI Buttons)
        public void SetTowerToManage(TowerType tower)
        {
            //can drag 1 item at a time
            _mouseItem.ReleaseTowerFromMouse();
            _towerToBuild = tower;
            _mouseItem.LockTowerToMouse(tower); //hover mode true
            towerDialog.HideTowerDialog();
        }

        public void ToggleTowerDialogOn()
        {
            towerDialog.ToggleTowerDialogOn(_nodeSelected);
        }

        //called from Node Script - return if build success or not
        public bool BuildTowerOn()
        {
            if (_towerToBuild != TowerType.NONE)
            {
                GameObject towerGo = TowerSpawner.Instance.GetPrefabByName(_towerToBuild.ToString());
                int towerCost = towerGo.GetComponent<TowerCtrl>().TowerData.GetCost;
                if (GameManager.Instance.SpendMoney(towerCost))
                {
                    SpawnTower();
                    AudioManager.Instance.PlayTowerDownSfx();
                    return true;
                }
            }

            return false;
        }

        //upgrade
        public void UpgradeTowerOn()
        {
            if (this._nodeSelected == null) return;
            TowerCtrl currTower = _nodeSelected.GetCurTower();
            if (currTower == null) return;
            if (!GameManager.Instance.SpendMoney(currTower.TowerData.GetUpgradeCost)) return;
            if (!currTower.TowerData.LevelUp()) return;
            // if (currTower == null || !currTower.TowerData.CanBeUpgrade) return;
            // if (!GameManager.Instance.SpendMoney(currTower.TowerData.GetUpgradeCost)) return;
            // currTower.RemoveTower();
            // Vector2 wantedPos = _nodeSelected.transform.position;
            // wantedPos.y += _offset; //adjust to sit right on square
            // GameObject towerGo = TowerSpawner.Instance.Spawn(currTower.TowerData.GetTowerToUpgrade.ToString(), wantedPos, Quaternion.identity);
            // _nodeSelected.SetCurTower(towerGo.GetComponent<TowerCtrl>());
            AudioManager.Instance.PlayTowerDownSfx();
            towerDialog.HideTowerDialog();
        }

        //called from tower ui button clicked
        public void SellTowerOn()
        {
            TowerCtrl currTower = _nodeSelected.GetCurTower();
            if (currTower != null)
            {
                GameManager.Instance.AddToMoney((int)(currTower.TowerData.GetTotalCost() / 1.5f));
                currTower.RemoveTower();
                _towerToBuild = TowerType.NONE;
                _nodeSelected.ClearNode();
                AudioManager.Instance.PlayTowerSoldSfx();
            }
            towerDialog.HideTowerDialog();
        }

        private void SpawnTower()
        {
            Vector2 wantedPos = _nodeSelected.transform.position;
            wantedPos.y += _offset; //adjust to sit right on square
            GameObject towerGO = _mouseItem.DestroyPreview();
            towerGO.transform.position = wantedPos;
            _nodeSelected.SetCurTower(towerGO.GetComponent<TowerCtrl>());
            if (!rememberSelection)
            {
                _towerToBuild = TowerType.NONE;
            }
        }
    }
}
