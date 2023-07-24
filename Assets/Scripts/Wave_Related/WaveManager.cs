using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Enemy_Related;
using Managers;
using Spawners;

namespace Wave_Related
{
    public enum EWaveDifficulty
    {
        Easy,
        Medium,
        Hard,
        Boss,
    }

    public class WaveManager : MonoBehaviour
    {
        [Header("Wave base info")]
        private int numOfWaves;
        private List<Wave> waves;
        private int _currWaveIndex;
        [SerializeField] private SpawnState currState;

        //EVENTS
        // public delegate void AllWavesCompleteDelegate();
        // public event AllWavesCompleteDelegate OnAllWavesComplete;
        // public delegate void WaveCompleteDelegate(int waveBonus);
        // public event WaveCompleteDelegate OnWaveComplete;

        private static WaveManager _instance;

        public static WaveManager Instance => _instance;

        private void Awake()
        {
            _instance = this;
            currState = SpawnState.Waiting;
            _currWaveIndex = 0;
        }
        private void Start()
        {
            EnemyManager.Instance.OnAllWavesComplete += OnAllWavesComplete;
        }

        public void SetupWaves(List<Wave> waves)
        {
            this.waves = waves;
            numOfWaves = waves.Count;
        }

        // private void FixedUpdate()
        // {
        //     //TODO some other class needs to do this!
        //     if (_currWaveIndex == numOfWaves && EnemyManager.Instance.GetEnemyCount() == 0 && currState == SpawnState.Waiting)
        //     {
        //         currState = SpawnState.Finish;
        //         OnAllWavesComplete?.Invoke();
        //     }
        // }

        public void OnAllWavesComplete()
        {
            currState = SpawnState.Finish;
        }

        public int GetMaxWaveCount() => numOfWaves;
        public SpawnState GetCurrentState() => currState;
        public int GetCurrentWaveIndex() => _currWaveIndex;

        public void SpawnCurrentWave()
        {
            if (currState == SpawnState.Waiting && EnemyManager.Instance.GetEnemyCount() == 0)
            {
                if (_currWaveIndex < numOfWaves)
                {
                    currState = SpawnState.Spawning;
                    GameManager.Instance.SetCurrentWaveIndex(_currWaveIndex); //for UI count
                    StartCoroutine(SpawnAllSubWaves()); //spawn all sub waves
                }
            }
        }

        //spawn all enemies in the current wave and setup next wave when done
        private IEnumerator SpawnAllSubWaves()
        {
            //Extract all sub waves of current wave
            List<SubWave> subWaves = waves[_currWaveIndex].GetSubWaves();

            for (int i = 0; i < subWaves.Count; i++)
            {
                SubWave currSubWave = subWaves[i];
                // Debug.Log(currSubWave.pathIndex);
                for (int j = 0; j < currSubWave.GetNumOfEnemies(); j++)
                {
                    EnemySpawner.Instance.SpawnEnemy(currSubWave);
                    yield return new WaitForSeconds(waves[_currWaveIndex].GetSpawnRate());
                }

                yield return new WaitForSeconds(waves[_currWaveIndex].GetTimeBetweenSubWaves());
            }
            //Wave Ended
            currState = SpawnState.Waiting;
            // OnWaveComplete?.Invoke(waves[_currWaveIndex].waveBonus); //trigger event at Game Manager
            _currWaveIndex++;
        }

        public int GetWaveBonus() => _currWaveIndex < numOfWaves ? waves[_currWaveIndex].waveBonus : 0;

        public bool IsLastWaveSpawned() => _currWaveIndex == numOfWaves && currState == SpawnState.Waiting;

        // private void IncreaseSubwaveIndex()
        // {
        //     if (_currWaveIndex == numOfWaves && EnemyManager.Instance.GetEnemyCount() == 0 && currState == SpawnState.Waiting)
        //     {
        //         currState = SpawnState.Finish;
        //         OnAllWavesComplete?.Invoke();
        //     }
        // }
    }
}