using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Hit Sounds")]
        [SerializeField] private AudioClip enemyHitSound;
        [SerializeField] private AudioClip enemyDeathSound;
        [SerializeField] private AudioClip playerHitSound;
        [SerializeField] private AudioClip playerDeathSound;

        [Header("Shop Sounds")]
        [SerializeField] private AudioClip towerPlacedSound;
        [SerializeField] private AudioClip towerSoldSound;
        [SerializeField] private AudioClip towerSelectedSound;
        [SerializeField] private AudioClip towerHoverSound;

        [Header("UI Interact Sounds")]
        [SerializeField] private AudioClip buttonClickSound;

        [Header("Background Musics")]
        [SerializeField] private List<AudioClip> backgroundMusics;

        [Header("AudioSources")]
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;

        #region Singleton
        private static AudioManager _instace;
        public static AudioManager Instance => _instace;

        private void Awake()
        {
            _instace = this;
        }
        #endregion

        private void Start()
        {
            _sfxAudioSource.volume = Preferences.GetSFXVolume();
            _musicAudioSource.volume = Preferences.GetMusicVolume();

            PlayRandomMusic();
        }

        public void SetVolumeSFX(float volume)
        {
            _sfxAudioSource.volume = volume;
        }

        public void SetVolumeMusic(float volume)
        {
            _musicAudioSource.volume = volume;
        }

        public void PlayRandomMusic() => _musicAudioSource.PlayOneShot(backgroundMusics[Random.Range(0, backgroundMusics.Count)]);

        public void PlayEnemyHitSfx() => _sfxAudioSource.PlayOneShot(enemyHitSound, 0.12f);

        public void PlayEnemyDeathSfx() => _sfxAudioSource.PlayOneShot(enemyDeathSound, 0.15f);

        public void PlayPlayerHitSfx() => _sfxAudioSource.PlayOneShot(playerHitSound, 0.8f);

        public void PlayPlayerDeathSfx() => _sfxAudioSource.PlayOneShot(playerDeathSound, 1f);

        public void PlayTowerDownSfx() => _sfxAudioSource.PlayOneShot(towerPlacedSound, 0.5f);

        public void PlayTowerSoldSfx() => _sfxAudioSource.PlayOneShot(towerSoldSound, 0.5f);

        public void PlayTowerSelectedSfx() => _sfxAudioSource.PlayOneShot(towerSelectedSound, 0.5f);

        public void PlayTowerHoverSfx() => _sfxAudioSource.PlayOneShot(towerHoverSound, 0.2f);

        public void PlayButtonClickSfx() => _sfxAudioSource.PlayOneShot(buttonClickSound, 0.2f);
    }
}
