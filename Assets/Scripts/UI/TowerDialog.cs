using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Nodes;
using Tower_Related;
using Managers;

public class TowerDialog : MyMonoBehaviour
{
    private static TowerDialog _instance;
    public static TowerDialog Instance => _instance;

    private bool _isSelected = false;
    public GameObject nodeCanvas;
    public Button upgradeButton;
    public Button sellButton;
    public TextMeshProUGUI levelTxt;
    private TextMeshProUGUI upgradeCostTxt;

    private TowerCtrl _currTower;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
        this.upgradeCostTxt = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    // protected override void OnEnable()
    // {
    // }

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnMoneyChanged += UpdateUpgradeButton;
        upgradeButton.onClick.AddListener(BuildManager.Instance.UpgradeTowerOn);
        sellButton.onClick.AddListener(BuildManager.Instance.SellTowerOn);
    }

    public void ToggleTowerDialogOn(Node nodeSelected)
    {
        _isSelected = !_isSelected;
        // Debug.Log("ToggleTowerDialogOn: " + _isSelected);
        if (_isSelected)
        {
            Vector2 nodePosition = nodeSelected.transform.position;
            nodePosition.y += 1.5f; //offset
            this.transform.position = nodePosition;

            _currTower = nodeSelected.GetCurTower();
            if (_currTower == null) return;
            if (_currTower.TowerData.CanBeUpgrade && GameManager.Instance.GetCurrentMoney() >= _currTower.TowerData.GetUpgradeCost)
            {
                upgradeCostTxt.text = "Upgrade (" + _currTower.TowerData.GetUpgradeCost + ")";
                upgradeButton.gameObject.SetActive(true);
            }
            else upgradeButton.gameObject.SetActive(false);
            _currTower.TowerStatus.IsSelecting = true;

            levelTxt.text = (_currTower.TowerData.GetCurrenteLevel + 1).ToString();
            nodeCanvas.SetActive(true);
        }
        else
        {
            if (_currTower != null)
                _currTower.TowerStatus.IsSelecting = false;
            nodeCanvas.SetActive(false);
        }
    }

    public void HideTowerDialog()
    {
        if (_currTower != null)
            _currTower.TowerStatus.IsSelecting = false;
        nodeCanvas.SetActive(false);
        _isSelected = false;
    }

    public void UpdateUpgradeButton(int money)
    {
        if (_currTower == null || !_isSelected) return;
        if (_currTower.TowerData.CanBeUpgrade && money >= _currTower.TowerData.GetUpgradeCost)
        {
            upgradeCostTxt.text = "Upgrade (" + _currTower.TowerData.GetUpgradeCost + ")";
            upgradeButton.gameObject.SetActive(true);
        }
        else upgradeButton.gameObject.SetActive(false);
    }
}
