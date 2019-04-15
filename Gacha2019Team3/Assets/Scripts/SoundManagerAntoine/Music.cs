using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : Sound
{
    protected override void Rename()
    {
        name = "Music";
    }

    public override void SetClipAndPlay(AudioClip _Clip, float _Volume, bool _Is3D)
    {
        base.SetClipAndPlay(_Clip, _Volume, false);
        m_AudioSource.loop = true;
    }

    public override void Remove()
    {
        SoundManager.Instance.RemoveMusic(this);
    }
}
