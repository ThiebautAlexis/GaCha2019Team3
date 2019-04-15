using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    protected AudioSource m_AudioSource;
    protected float m_ExistingTime = 0;
    bool m_IsUpdating = false;


    void Awake()
    {
        gameObject.AddComponent<AudioSource>().playOnAwake = false;
        m_AudioSource = GetComponent<AudioSource>();

        Rename();
    }

    void Update()
    {
        if (!m_IsUpdating)
        {
            return;
        }

        m_ExistingTime += Time.deltaTime;
    }

    protected virtual void Rename()
    {
        name = "Sound";
    }

    public virtual void SetClipAndPlay(AudioClip _Clip, float _Volume, bool _Is3D)
    {
        m_AudioSource.volume = _Volume;
        m_AudioSource.clip = _Clip;
        m_AudioSource.Play();
        m_IsUpdating = true;
        m_AudioSource.spatialize = _Is3D;
    }

    public void Stop()
    {
        m_ExistingTime = m_AudioSource.clip.length;
        m_AudioSource.Stop();
    }

    public void Pause()
    {
        m_IsUpdating = false;
        m_AudioSource.Pause();
    }

    public void Resume()
    {
        m_IsUpdating = true;
        m_AudioSource.UnPause();
    }

    public virtual void Remove()
    {
        SoundManager.Instance.RemoveSound(this);
    }


    public float Volume
    {
        get
        {
            return m_AudioSource.volume;
        }

        set
        {
            m_AudioSource.volume = value;
        }
    }

    public bool SoundFinished
    {
        get
        {
            if (m_ExistingTime >= m_AudioSource.clip.length)
            {
                return true;
            }
            return false;
        }
    }
}
