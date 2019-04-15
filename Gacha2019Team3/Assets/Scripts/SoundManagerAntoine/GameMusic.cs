using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField]
    AudioClip m_Music;

    void Awake()
    {
        SoundManager.Instance.PlayMusic(m_Music);    
    }
}
