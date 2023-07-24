// using System;
// using Managers;
using TMPro;
using Tower_Related;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;
using Spawners;
using Managers;

namespace Shop_Related
{
    public class ShopItem : MonoBehaviour
    {

        [SerializeField] private TowerType towerName;

        //Scripts refs
        private TowerCtrl _tower;

        //UI
        private TextMeshProUGUI _costTv;
        private Image _image;

        //states
        private bool _canBuy = false;
        // private bool _isHover = false;

        void Awake()
        {
            _image = GetComponent<Image>();
            _costTv = GetComponentInChildren<TextMeshProUGUI>();
            _tower = FindObjectOfType<TowerSpawner>().GetPrefabByName(towerName.ToString()).GetComponent<TowerCtrl>();
            _costTv.text = _tower.TowerData.GetCost.ToString();
        }

        // Start is called before the first frame update
        // void Start()
        // {
        // }

        //**Called from Event Trigger in Unity**//
        public void OnTowerClick()
        {
            if (_canBuy)
            {
                AudioManager.Instance.PlayTowerSelectedSfx();
                Shop.Instance.SelectTowerToBuild(towerName);
                transform.localScale = new Vector2(1f, 1f);
            }
        }

        public void OnTowerHover()
        {
            if (_canBuy)
            {
                transform.localScale = new Vector2(1.2f, 1.2f);
            }
            AudioManager.Instance.PlayTowerHoverSfx();
            Shop.Instance.SetTowerDisplayName(towerName.ToString());
            // _isHover = true;
        }

        public void OnTowerExit()
        {
            transform.localScale = new Vector2(1f, 1f);
            // _isHover = false;
            Shop.Instance.SetTowerDisplayName("");
        }

        public int GetShopItemCost() => _tower.TowerData.GetCost;

        public void SetCanBuy(bool canBuy) => _canBuy = canBuy;

        public void SetHighlightColor()
        {
            _image.color = Color.white;
        }

        public void SetUnhighlightColor()
        {
            _image.color = Color.black;
        }
    }
}