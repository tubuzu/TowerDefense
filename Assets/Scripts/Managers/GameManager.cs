// using System;
// using System.Collections;
using Enemy_Related;
using Scenes;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wave_Related;
using Shop_Related;

namespace Managers
{
    public class GameManager : MyMonoBehaviour
    {
        #region Singleton

        private static GameManager _instance;

        public static GameManager Instance => _instance;

        protected override void Awake()
        {
            base.Awake();

            _instance = this;

            //finding objects
            _enemyManager = FindObjectOfType<EnemyManager>();
            _waveManager = FindObjectOfType<WaveManager>();
            _dialogManager = FindObjectOfType<DialogManager>();
            _shop = FindObjectOfType<Shop>();
            _displayUI = GetComponent<DisplayUI>();
            _levelLoader = FindObjectOfType<LevelLoader>();
            _exclamation = FindObjectOfType<Exclamation>();

            this.currLevel = Preferences.GetCurrentLvl();
            if (world != null && currLevel - 1 < world.levels.Length)
            {
                this.levelInfo = world.levels[currLevel - 1];
                this.startHealth = levelInfo.startHealth;
                this.startMoney = levelInfo.startMoney;
            }
        }

        #endregion //singleton instace

        [SerializeField] private World world;
        [SerializeField] private int currLevel;
        private Level levelInfo;

        [Header("Player Stats")]
        [SerializeField]
        private int startHealth = 15;

        [SerializeField] private int startMoney;

        //stats
        private int _health;
        private int _money;

        //components
        private DisplayUI _displayUI;

        //managers
        private EnemyManager _enemyManager;
        private WaveManager _waveManager;
        private DialogManager _dialogManager;
        private Shop _shop;

        //scene
        private LevelLoader _levelLoader;
        private Exclamation _exclamation;

        private float _gameSpeed;

        //events
        public delegate void OnMoneyChangedDelegate(int money);
        public event OnMoneyChangedDelegate OnMoneyChanged;

        //Events Subscriptions
        protected override void OnEnable()
        {
            base.OnEnable();
            _enemyManager.OnEnemyKilledEvent += AddToMoney;
            _enemyManager.OnAllEnemiesDeadEvent += WaveFinished;
            _enemyManager.OnAllWavesComplete += WinLevel;
            // _waveManager.OnWaveComplete += WaveFinished;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _enemyManager.OnEnemyKilledEvent -= AddToMoney;
            _enemyManager.OnAllEnemiesDeadEvent -= WaveFinished;
            _enemyManager.OnAllWavesComplete -= WinLevel;
            // _waveManager.OnWaveComplete -= WaveFinished;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _gameSpeed = 1f;
            _money = startMoney;
            OnMoneyChanged?.Invoke(_money);
            _health = startHealth;
            _exclamation.Show(true);
            WaveManager.Instance.SetupWaves(this.levelInfo.waves);
            SetupUI();
        }

        private void SetupUI()
        {
            _displayUI.UpdateHealthUI(_health);
            _displayUI.UpdateMoneyUI(_money);
            _displayUI.UpdateMaxWaveCountUI(_waveManager.GetMaxWaveCount());
            SetCurrentWaveIndex(0);
        }

        private void Update()
        {
            ManageKeyboardInput();
        }

        private void ManageKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnPlayButtonPress();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPausePress();
            }
        }

        //called from button or key code
        public void OnPausePress() => _dialogManager.TogglePauseLabel(_gameSpeed);

        //called also from UI button press
        public void OnPlayButtonPress()
        {
            _waveManager.SpawnCurrentWave();
            _exclamation.Show(false);
        }

        public void TakeDamage(int dmg)
        {
            _health -= dmg;
            if (_health <= 0)
            {
                _health = 0;
                AudioManager.Instance.PlayPlayerDeathSfx();
                LooseLevel();
            }

            _displayUI.UpdateHealthUI(_health);
            AudioManager.Instance.PlayPlayerHitSfx();
        }

        public void AddToMoney(int amount)
        {
            _money += amount;
            _displayUI.UpdateMoneyUI(_money);
            OnMoneyChanged?.Invoke(_money);
        }

        public bool SpendMoney(int amount)
        {
            if (amount > _money)
            {
                return false;
            }

            _money -= amount;
            _displayUI.UpdateMoneyUI(_money);
            OnMoneyChanged?.Invoke(_money);
            return true;
        }

        public int GetCurrentMoney() => _money;

        public void SetCurrentWaveIndex(int currWaveIndex)
        {
            _displayUI.UpdateCurrentWaveCountUI(currWaveIndex);
        }

        //when button press
        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ExitToMainMenu()
        {
            _levelLoader.LoadMainMenuScene();
        }

        //called from button click
        public void RestartLevel()
        {
            _levelLoader.LoadCurrentScene();
        }

        public void SetGameSpeed(float speed)
        {
            _gameSpeed = speed;
            Time.timeScale = speed;
        }

        private void WaveFinished(int waveBonus)
        {
            if (_waveManager.GetCurrentState() == SpawnState.Waiting && _enemyManager.GetEnemyCount() == 0)
            {
                AddToMoney(waveBonus); //each wave finish grants player with money
                ActivateWaveFinishDialog();
            }
        }

        private void ActivateWaveFinishDialog()
        {
            _dialogManager.ActivateWaveFinishLabel();
        }

        private void WinLevel()
        {
            _gameSpeed = 0;
            SetGameSpeed(_gameSpeed);
            if (this._health > 0) GameData.instance.UpdateLevelStar(Mathf.CeilToInt((float)this._health * 3 / this.startHealth));
            UnlockNextLevel();
            _dialogManager.ActivateWinLabel();
        }

        private void UnlockNextLevel()
        {
            int currLvl = Preferences.GetCurrentLvl();
            Preferences.SetMaxLvl(currLvl + 1);
            GameData.instance.SetActiveLevel(currLevel);
        }

        private void LooseLevel()
        {
            _gameSpeed = 0;
            SetGameSpeed(_gameSpeed);
            _dialogManager.ActivateLooseLabel();
        }

        // public int GetCurrentLevel() => currLevel;

        // public Level GetLevelInfo() => levelInfo;
    }
}