// using System;
using UnityEngine;
using Managers;
// using Shop;
using Tower_Related;

namespace Nodes
{
    public class Node : MyMonoBehaviour
    {
        private NodeStyle _nodeStyle;

        private bool _isPlaced = false;

        private TowerCtrl _curTower;

        protected override void Start()
        {
            _nodeStyle = GetComponent<NodeStyle>();
        }

        public void SetCurTower(TowerCtrl tower)
        {
            // Debug.Log(tower.gameObject.name);
            this._curTower = tower;
        }
        // public TowerCtrl CurTower => _curTower;
        public TowerCtrl GetCurTower() => _curTower;

        public void ClearNode()
        {
            _isPlaced = false;
            _nodeStyle.SetFreeColor(); //free color
        }

        public void OnNodeMouseUp()
        {
            BuildManager.Instance.SetNodeSelected(this); //set this Node as selected first

            if (!_isPlaced)
            {
                TryBuildTower();
            }
            else
            {
                //pop sell dialog
                BuildManager.Instance.ToggleTowerDialogOn();
            }
        }

        private void TryBuildTower()
        {
            if (BuildManager.Instance.BuildTowerOn())
            {
                _isPlaced = true; //cannot build anymore
                _nodeStyle.SetPlacedColor(); //occupied color
            }
        }
    }
}