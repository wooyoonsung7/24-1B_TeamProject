using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
    //static 전역으로 가져와서 사용 할 수 있게 해준다.  싱글톤패턴: 어디서든 전역으로 존재하고 접근할 수 있는 장점이 있다.
    public static SoundManager instance;               //싱글톤 인스턴스 화 시틴다.

    public List<Sound> sounds = new List<Sound>();

    SoundData soundData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetSound();
    }

    private void SetSound()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = sound.soundGameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            SoundData soundData = sound.soundGameObject.GetComponent<SoundData>();
            soundData.soundname.Add(sound.soundname);
            soundData.soundLevel.Add(sound.soundLevel);
        }
    }

    // 사운드를 재생하는 매서드
    public void PlaySound(string name)                                  //인수 Name 받아서
    {

        Sound soundToPlay = sounds.Find(sound => sound.soundname == name);   //List 안에 있는 name이 같은 것을 검색 후 soundToPlay 에 선언
        if (soundToPlay != null)
        {
            soundToPlay.source.Play();
        }
        else
        {
            Debug.LogWarning("사운드 : " + name + " 없습니다.");
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
}

