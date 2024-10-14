using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/SoundData", fileName = "SoundData", order = int.MaxValue)]
public class SoundData : ScriptableObject
{
    [SerializeField]
    private string soundname;
    public string SoundName { get { return soundname; } }

    [SerializeField]
    private int soundLevel;
    public int SoundLevel { get { return soundLevel; } }

    [SerializeField]
    private AudioClip clip;
    public AudioClip Clip { get { return clip; } }

    [SerializeField]
    [Range(0f, 1f)]
    private float volume = 1.0f;
    public float Volume { get { return volume; } }

    [SerializeField]
    [Range(0.1f, 3f)]
    private float pitch = 1.0f;
    public float Pitch { get { return pitch; } }

    [SerializeField] private bool loop;
    public bool Loop { get { return loop; } }


    [HideInInspector]
    public AudioSource source;
}
