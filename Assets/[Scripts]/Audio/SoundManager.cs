using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class SoundManager : MonoBehaviour
{

    public List<AudioSource> audioSources;
    public List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        audioSources = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitializeSoundFX();
    }

    private void InitializeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/jump-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/hurt-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/death-sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/bullet-sound"));

        audioClips.Add(Resources.Load<AudioClip>("Audio/main-soundtrack"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/end-soundtrack"));
    }

    public void PlaySoundFX(SoundFX sound, Channel channel)
    {
        audioSources[(int)channel].clip = audioClips[(int)sound];
        audioSources[(int)channel].Play();
    }

    public void PlayMusic()
    {
        audioSources[(int)Channel.MUSIC].clip = audioClips[(int)SoundFX.MUSIC];
        audioSources[(int)Channel.MUSIC].volume = 0.25f;
        audioSources[(int)Channel.MUSIC].loop = true;
        audioSources[(int)Channel.MUSIC].Play();
    }
}