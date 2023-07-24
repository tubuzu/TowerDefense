﻿using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class DisplayUI : MonoBehaviour {

        private const string X = "X";
        private const string WAVE = "Wave";

        [SerializeField] private TextMeshProUGUI waveCounterTv;
        [SerializeField] private TextMeshProUGUI moneyTv;
        [SerializeField] private TextMeshProUGUI healthTv;
    
        private int _maxWaveCount;

        public void UpdateMoneyUI(int score) {
            moneyTv.text = score.ToString();
        }

        public void UpdateHealthUI(int health) {
            healthTv.text = health.ToString();
        }

        public void UpdateMaxWaveCountUI(int maxWaveCount) {
            _maxWaveCount = maxWaveCount;
            // UpdateCurrentWaveCountUI(currWaveIndex);
        }

        public void UpdateCurrentWaveCountUI(int currWaveCount) {
            waveCounterTv.text = WAVE + "\n" + (currWaveCount + 1) + "/" + _maxWaveCount;
        }
    }
}
