// using System;
// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Tower_Related;
using Managers;
using Nodes;
using Spawners;

public class MouseItem : MonoBehaviour
{
    private GameObject _towerGO;

    private void Update()
    {
        if (_towerGO != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _towerGO.transform.position = mousePos;

            if (Input.GetMouseButtonDown(1))
            {
                BuildManager.Instance.DeselectTowerToManage();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            bool checkIfHitNode = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Node"))
                {
                    Node node = hit.collider.GetComponent<Node>();
                    if (node != null)
                    {
                        checkIfHitNode = true;
                        node.OnNodeMouseUp();
                    }
                }
            }
            if (!checkIfHitNode) TowerDialog.Instance.HideTowerDialog();
        }
    }

    public void LockTowerToMouse(TowerType tower)
    {
        _towerGO = TowerSpawner.Instance.Spawn(tower.ToString(), transform.position, Quaternion.identity);
    }

    public void ReleaseTowerFromMouse()
    {
        if (_towerGO != null)
        {
            TowerSpawner.Instance.Despawn(_towerGO);
        }
    }

    public GameObject DestroyPreview()
    {
        _towerGO.GetComponent<TowerCtrl>().TowerStatus.CanFight = true;
        GameObject result = _towerGO;
        _towerGO = null;
        return result;
    }
}