using System;
using System.Collections.Generic;
using Managers;
using Tower_Related;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Shop_Related
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI towerDisplayNameTv;

        private ShopItem[] _items;

        private static Shop _instance;
        public static Shop Instance => _instance;

        private void Awake()
        {
            _instance = this;
            _items = GetComponentsInChildren<ShopItem>();
        }

        // private void OnEnable()
        // {
        // }

        private void Start()
        {
            GameManager.Instance.OnMoneyChanged += UpdateItemsBuyState;
            towerDisplayNameTv.text = "";
        }

        // private void Update()
        // {
        //     UpdateItemsBuyState();
        // }

        private void UpdateItemsBuyState(int money)
        {
            foreach (ShopItem item in _items)
            {
                if (item.GetShopItemCost() <= money)
                {
                    item.SetHighlightColor();
                    item.SetCanBuy(true);
                }
                else
                {
                    item.SetUnhighlightColor();
                    item.SetCanBuy(false);
                }
            }
        }

        public void SelectTowerToBuild(TowerType tower)
        { //called from a button press
            BuildManager.Instance.SetTowerToManage(tower);
        }

        public void SetTowerDisplayName(string towerName)
        {
            towerDisplayNameTv.text = towerName;
        }
    }
}