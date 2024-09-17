using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("SoundSetting")]
    [SerializeField] AudioMixer audioMixer;
    [Header("Music")]
    [SerializeField] AudioSource musicSource;
    [Header("MusicList")]
    [SerializeField] AudioClip mainMusic;
    [SerializeField] AudioClip bossFightMusic;
    [Header("SoundFX")]
    [SerializeField] AudioSource audioSource;
    [Header("SoundFXList")]
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip die;
    [SerializeField] AudioClip dash;
    [SerializeField] AudioClip openChest;
    [SerializeField] AudioClip coin;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayMainMusic();
    }

    public void SetMasterVolumme(float value)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(value) * 20f);
    }
    public void SetMusicVolumme(float value)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(value) * 20f);
    }
    public void SetSoundFXVolumme(float value)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(value) * 20f);
    }

    void PlaySound(AudioClip aC, Transform pos, float volume)
    {
        AudioSource audio = Instantiate(audioSource, pos.position, Quaternion.identity);

        audio.clip = aC;

        audio.volume = volume;

        audio.Play();

        float time = audio.clip.length;

        Destroy(audio.gameObject, time);
    }

    public void PlayJumpSound(Transform pos, float volume = 1f)
    {
        PlaySound(jump, pos, volume);
    }

    public void PlayDashSound(Transform pos, float volume = 0.3f)
    {
        PlaySound(dash, pos, volume);
    }

    public void PlayAttackSound(Transform pos, float volume = 1f)
    {
        PlaySound(attack, pos, volume);
    }

    public void PlayDeathSound(Transform pos, float volume = 0.5f)
    {
        PlaySound(die, pos, volume);
    }

    public void PlayCoinSound(Transform pos, float volume = 1f)
    {
        PlaySound(coin, pos, volume);
    }

    public void PlayOpenChestSound(Transform pos, float volume = 1f)
    {
        PlaySound(openChest, pos, volume);
    }

    public void PlayMainMusic()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void PlayBossFightMusic()
    {
        musicSource.clip = bossFightMusic;
        musicSource.Play();
    }
}
