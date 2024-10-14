using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;



public class SoundManager : MonoBehaviour
{
    //static 전역으로 가져와서 사용 할 수 있게 해준다.  싱글톤패턴: 어디서든 전역으로 존재하고 접근할 수 있는 장점이 있다.
    public static SoundManager instance;               //싱글톤 인스턴스 화 시틴다.


    private void Awake()
    {
        instance = this;
    }

}

