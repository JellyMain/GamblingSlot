using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundPackSO soundsPack;
    [SerializeField] Slider volumeSlider;
    private float volumeMultiplier = 1f;

    public static SoundManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        volumeSlider.onValueChanged.AddListener((v) => volumeMultiplier = v);
    }


    public void PlaySound(AudioClip sound, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, position, volume);
    }


    public void PlayChangeSymbolSound()
    {
        PlaySound(soundsPack.changeSymbolSound, Camera.main.transform.position, volumeMultiplier * 0.3f);
    }


    public void PlayWinSound()
    {
        PlaySound(soundsPack.winSound, Camera.main.transform.position, volumeMultiplier * 0.3f);
    }


    public void ChangeVolume(float sliderValue)
    {
        volumeMultiplier = sliderValue;
    }
}
