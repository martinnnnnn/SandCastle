using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundManager : Singleton<SoundManager>
{
    


    public AudioClip[] clips;


    private AudioSource sound;
    private AudioSource music;

    private Coroutine MusicCoroutine;
    private Coroutine IntroCoroutine;

    void Awake()
    {
        sound = gameObject.AddComponent<AudioSource>();
        music = gameObject.AddComponent<AudioSource>();
        StartBuildMusic();
    }


    public void StartBuildMusic()
    {
        if (MusicCoroutine != null)
        {
            StopCoroutine(MusicCoroutine);
            MusicCoroutine = null;
        }
        if (IntroCoroutine != null)
        {
            StopCoroutine(IntroCoroutine);
            IntroCoroutine = null;
        }
        music.Stop();

        AudioClip intro = null;
        AudioClip loop = null;

        foreach (AudioClip clip in clips)
        {
            if (clip.name == "Musique_Intro_Construct")
            {
                intro = clip;
            }
            if (clip.name == "Musique_Loop_Construct")
            {
                loop = clip;
            }
        }
        IntroCoroutine = StartCoroutine(playSound(music, intro));
        MusicCoroutine = StartCoroutine(playSound(music, loop, 0, true));

    }



    public void StartFightMusic()
    {
        if (MusicCoroutine != null)
        {
            StopCoroutine(MusicCoroutine);
            MusicCoroutine = null;
        }
        if (IntroCoroutine != null)
        {
            StopCoroutine(IntroCoroutine);
            IntroCoroutine = null;
        }
        music.Stop();

        AudioClip fighttheme = null;
        
        foreach (AudioClip clip in clips)
        {
            Debug.Log("clip:" + clip.name);
            if (clip.name == "Musique_Defend")
            {
                fighttheme = clip;
                break;
            }
        }
        MusicCoroutine = StartCoroutine(playSound(music, fighttheme,0, true));

    }

    public AudioClip GetAudioClip(string name)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }


    public void PlaySound(string name, float delay = 0f)
    {

        StartCoroutine(playSound(name, delay));

    }


    IEnumerator playSound(string name, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                sound.PlayOneShot(clip);
            }
        }
    }

    IEnumerator playSound(AudioSource source, AudioClip clip, float delay = 0f, bool loop = false)
    {
        yield return new WaitForSeconds(delay);

        
        source.loop = loop;
        source.clip = clip;
        source.Play();
    }



}
