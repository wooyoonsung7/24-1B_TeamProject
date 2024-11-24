using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{

    public string soundname;

    public int soundLevel;

    public GameObject soundGameObject;

    public AudioClip clip;

    public float spatialBlend = 1.0f;

    public float volume = 1.0f;

    public float pitch = 1.0f;
    
    public bool loop;

    public bool isGetComp;

    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<Sound> sounds = new List<Sound>();

    //public float soundValue = 1f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SetSound();
    }

    private void SetSound()
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.isGetComp) sound.source = sound.soundGameObject.AddComponent<AudioSource>();
            else sound.source = sound.soundGameObject.GetComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            SoundData soundData = sound.soundGameObject.GetComponent<SoundData>();
            if(sound.soundname == "Walk" || sound.soundname == "Run") soundData.soundname.Add(sound.soundname);
            if (sound.soundname == "Walk" || sound.soundname == "Run") soundData.soundLevel.Add(sound.soundLevel);
        }
    }


    public void PlaySound(string name)
    {

        Sound soundToPlay = sounds.Find(sound => sound.soundname == name);
        if (soundToPlay != null)
        {
            soundToPlay.source.Play();
        }

    }

    public void PauseSound(string name)
    {
        Sound soundToPause = sounds.Find(sound => sound.soundname == name);
        //Debug.Log(soundToPause.soundname);
        if (soundToPause != null)
        {
            soundToPause.source.Stop();
        }

    }
    public void StopSound(string name)
    {
        Sound soundToStop = sounds.Find(sound => sound.soundname == name);

        if (soundToStop != null)
        {
            if (soundToStop.source.isPlaying)
            {
                soundToStop.source.Stop();
            }
        }
    }
}
