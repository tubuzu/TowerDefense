using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tower_Related
{
    public class TowerStatus : TowerAbstract
    {
        public delegate void CanFightChangedDelegate(bool canFight);
        public event CanFightChangedDelegate OnCanFightChanged;

        public delegate void IsSelectingChangedDelegate(bool isSelecting);
        public event IsSelectingChangedDelegate OnIsSelectingChanged;

        private bool _canFight = false;
        private bool _isSelecting = false;

        public bool CanFight
        {
            get => _canFight;
            set
            {
                _canFight = value;
                OnCanFightChanged?.Invoke(value);
                // Debug.Log(value);
            }
        }

        public bool IsSelecting
        {
            get => _isSelecting;
            set
            {
                _isSelecting = value;
                OnIsSelectingChanged?.Invoke(value);
            }
        }
    }
}