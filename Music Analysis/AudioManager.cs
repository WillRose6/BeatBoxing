using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource SFXSource;
    [SerializeField]
    private AudioSource noteSource;
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource backgroundSource;
    [SerializeField]
    private AudioSource cheeringSource;

    public BeatSpawner beatSpawner;
    public AudioSource beatSource;
    public static Song[] songs;
    public Song[] loadedSongs;

    private void Awake()
    {
        if(instance)
        {
            Debug.LogError("There is mroe than 1 SFXPlayer in the scene!");
            return;
        }

        instance = this;

        LoadInSong(0);
    }

    public void StopMusic()
    {
        musicSource.Stop();
        noteSource.Stop();
    }

    //We added this uwu
    public void StartMusic()
    {
        StartCoroutine(delayMusic());
        StopBackgroundMusic();
        noteSource.Play();
    }

    private IEnumerator delayMusic()
    {
        yield return new WaitForSeconds(BeatSpawner.delay);
        musicSource.Play();
    }

    public void PlayEffect(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic()
    {
        backgroundSource.Play();
    }
    
    public void StopBackgroundMusic()
    {
        backgroundSource.Stop();
    }

    public void startCheering()
    {
        cheeringSource.Play();
    }

    public void stopCheering()
    {
        cheeringSource.Stop();
    }

    public void LoadInSong(int num)
    {
        loadedSongs = new Song[songs.Length];
        for (int i = 0; i < songs.Length; i++)
        {
            loadedSongs[i] = songs[i];
        }

        beatSource.clip = songs[num].clip;
        musicSource.clip = songs[num].clip;
        beatSpawner.bias = songs[num].bias;
        beatSpawner.timeStep = songs[num].timeStep;
        beatSpawner.NoteSpeed = songs[num].speed;
    }
}
