using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[System.Serializable]
public class Sound
{

    public string soundname;

    public int soundLevel;

    public int soundType;  //0번이면 환경, 1번 사물, 2번 플레이어, 몬스터

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

    [SerializeField] AudioMixer mixer;

    [SerializeField] private Slider E_slider;
    [SerializeField] private Slider O_slider;
    [SerializeField] private Slider C_slider;
    private static float E_sliderValue = 0;
    private static float O_sliderValue = 0;
    private static float C_sliderValue = 0;

    public bool isEnd = false;
    //public float soundValue = 1f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if(sounds.Count > 0) SetSound();

        SetSliderValue();
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
            if(sound.soundType == 0) sound.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Environment")[0];
            if(sound.soundType == 1) sound.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Objects")[0];
            if(sound.soundType == 2) sound.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Character")[0];

            SoundData soundData = sound.soundGameObject.GetComponent<SoundData>();
            if(sound.soundname == "Walk" || sound.soundname == "Run") soundData.soundname.Add(sound.soundname);
            if (sound.soundname == "Walk" || sound.soundname == "Run") soundData.soundLevel.Add(sound.soundLevel);
        }
    }
    private void SetSliderValue()
    {
        E_slider = GameObject.FindGameObjectWithTag("E_Slider").GetComponent<Slider>();
        O_slider = GameObject.FindGameObjectWithTag("O_Slider").GetComponent<Slider>();
        C_slider = GameObject.FindGameObjectWithTag("C_Slider").GetComponent<Slider>();

        E_slider.value = E_sliderValue;
        O_slider.value = O_sliderValue;
        C_slider.value = C_sliderValue;
    }

    public void SetEnvironmentVolume(float volume)
    {
        mixer.SetFloat("Environment", Mathf.Log10(volume) * 20);

    }
    public void SetObjectsVolume(float volume)
    {
        mixer.SetFloat("Objects", Mathf.Log10(volume) * 20);

    }
    public void SetCharacterVolume(float volume)
    {
        mixer.SetFloat("Character", Mathf.Log10(volume) * 20);
    }

    private void FixedUpdate()
    {
        E_sliderValue = E_slider.value;
        O_sliderValue = O_slider.value;
        C_sliderValue = C_slider.value;
        //Debug.Log(C_sliderValue);
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
    public void PauseAllSound()
    {
        for (int i = 0; i< sounds.Count; i++)
        {
            PauseSound(sounds[i].soundname);
            if (i == sounds.Count - 1)
            {
                isEnd = true;
            }
        }
    }

}
