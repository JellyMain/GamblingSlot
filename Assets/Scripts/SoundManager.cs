using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundPackSO soundsPack;

    public static SoundManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    public void PlaySound(AudioClip sound, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, position, volume);
    }


    public void PlaySound(AudioClip[] soundsArray, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(soundsArray[Random.Range(0, soundsArray.Length)], position, volume);
    }


    public void PlayChangeSymbolSound()
    {
        PlaySound(soundsPack.changeSymbolSound, Camera.main.transform.position, 0.3f);
    }

}
