using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager m_Instance;

    [Header("Sounds")]

    [SerializeField]
    AudioClip m_Explosion;
    [SerializeField]
    AudioClip m_BaseTaken;
    [SerializeField]
    AudioClip m_BaseSelection;
    [SerializeField]
    AudioClip m_GeneratorBroken;
    [SerializeField]
    AudioClip m_MoveUnits;
    [SerializeField]
    AudioClip m_ArrowModeStart;
    [SerializeField]
    AudioClip m_Victory;
    [SerializeField]
    AudioClip m_Defeat;

    static float m_SoundsVolume = 1f, m_MusicsVolume = 1f;

    List<Sound> m_Sounds = new List<Sound>();
    List<Music> m_Musics = new List<Music>();

    [Header("Ambiance")]

    [SerializeField]
    AudioSource m_Ambiance;

    void Awake()
    {
        SoundManager foundSoundManager = FindObjectOfType<SoundManager>();
        if (foundSoundManager != null && foundSoundManager != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);    
    }

    void Update()
    {
        TryDeleteFinished();
    }

    void TryDeleteFinished()
    {
        List<Sound> toDel = new List<Sound>();
        List<Sound> soundsAndMusics = new List<Sound>();
        soundsAndMusics.AddRange(m_Sounds);

        foreach (Sound sound in soundsAndMusics)
        {
            if (sound.SoundFinished)
            {
                toDel.Add(sound);
            }
        }

        if (toDel.Count > 0)
        {
            toDel[0].Remove();
            Destroy(toDel[0].gameObject);
        }
    }

    public void PlayExplosion()
    {
        PlaySound(m_Explosion);
    }

    public void PlayBaseTaken()
    {
        PlaySound(m_BaseTaken);
    }

    public void PlayBaseSelection()
    {
        PlaySound(m_BaseSelection);
    }

    public void PlayGeneratorBroken()
    {
        PlaySound(m_GeneratorBroken);
    }

    public void PlayMoveUnits()
    {
        PlaySound(m_MoveUnits);
    }

    public void PlayArrowModeStart()
    {
        PlaySound(m_ArrowModeStart);
    }

    public void PlayVictory()
    {
        PlaySound(m_Victory);
    }

    public void PlayDefeat()
    {
        PlaySound(m_Defeat);
    }

    public void RemoveSound(Sound _Sound)
    {
        if (m_Sounds.Contains(_Sound))
        {
            m_Sounds.Remove(_Sound);
        }
    }

    public void RemoveMusic(Music _Music)
    {
        if (m_Musics.Contains(_Music))
        {
            m_Musics.Remove(_Music);
        }
    }

    public void PlaySound(AudioClip _Sound)
    {
        Sound newSound = new GameObject().AddComponent<Sound>();
        newSound.transform.parent = transform;
        newSound.SetClipAndPlay(_Sound, m_SoundsVolume, false);
        m_Sounds.Add(newSound);
    }

    public void Play3DSound(AudioClip _Sound, Vector3 _Position)
    {
        Sound newSound = new GameObject().AddComponent<Sound>();
        newSound.transform.position = _Position;
        newSound.transform.parent = transform;
        newSound.SetClipAndPlay(_Sound, m_SoundsVolume, true);
        m_Sounds.Add(newSound);
    }

    public void PlayMusic(AudioClip _Music)
    {
        Music newMusic = new GameObject().AddComponent<Music>();
        newMusic.transform.parent = transform;
        newMusic.SetClipAndPlay(_Music, m_MusicsVolume, false);
        m_Musics.Add(newMusic);
    }

    public void SetSoundsVolume(float _Volume)
    {
        m_SoundsVolume = Mathf.Clamp(_Volume, 0f, 1f);
        foreach (Sound sound in m_Sounds)
        {
            sound.Volume = m_SoundsVolume;
        }
        m_Ambiance.volume = m_SoundsVolume;
    }

    public void IncreaseSoundsVolume(float _Amount)
    {
        m_SoundsVolume = Mathf.Clamp(m_SoundsVolume + _Amount, 0f, 1f);
        foreach (Sound sound in m_Sounds)
        {
            sound.Volume = m_SoundsVolume;
        }
        m_Ambiance.volume = m_SoundsVolume;
    }

    public void SetMusicsVolume(float _Volume)
    {
        m_MusicsVolume = Mathf.Clamp(_Volume, 0f, 1f);
        foreach (Music music in m_Musics)
        {
            music.Volume = m_MusicsVolume;
        }
        m_Ambiance.volume = m_SoundsVolume;
    }

    public void IncreaseMusicVolume(float _Amount)
    {
        m_MusicsVolume = Mathf.Clamp(m_MusicsVolume + _Amount, 0f, 1f);
        foreach (Music music in m_Musics)
        {
            music.Volume = m_MusicsVolume;
        }
        m_Ambiance.volume = m_SoundsVolume;
    }

    public void ForceStopSounds()
    {
        foreach (Sound sound in m_Sounds)
        {
            sound.Stop();
        }

        m_Sounds.Clear();
    }

    public void ForceStopMusics()
    {
        foreach (Music music in m_Musics)
        {
            music.Stop();
        }

        m_Musics.Clear();
    }

    public void ForceStopAmbiance()
    {
        m_Ambiance.Stop();
    }

    public void PauseSounds()
    {
        foreach (Sound sound in m_Sounds)
        {
            sound.Pause();
        }
    }

    public void PauseMusics()
    {
        foreach (Music music in m_Musics)
        {
            music.Pause();
        }
    }

    public void Pause()
    {
        PauseSounds();
        PauseMusics();
    }

    public void ResumeSounds()
    {
        foreach (Sound sound in m_Sounds)
        {
            sound.Resume();
        }
    }

    public void ResumeMusics()
    {
        foreach (Music music in m_Musics)
        {
            music.Resume();
        }
    }

    public void Resume()
    {
        ResumeSounds();
        ResumeMusics();
    }

    public void Clear()
    {
        ClearMusics();
        ClearSounds();
    }

    public void ClearMusics()
    {
        while (m_Musics.Count > 0)
        {
            Destroy(m_Musics[0].gameObject);
            m_Musics.RemoveAt(0);
        }
    }

    public void ClearSounds()
    {
        while (m_Sounds.Count > 0)
        {
            Destroy(m_Sounds[0].gameObject);
            m_Sounds.RemoveAt(0);
        }
    }

    #region Getters/Setters

    public static SoundManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<SoundManager>();
            }
            return m_Instance;
        }
    }

    public float SoundsVolume
    {
        get
        {
            return m_SoundsVolume;
        }

        set
        {
            SetSoundsVolume(value);
        }
    }

    public float MusicsVolume
    {
        get
        {
            return m_MusicsVolume;
        }

        set
        {
            SetMusicsVolume(value);
        }
    }

    #endregion
}
