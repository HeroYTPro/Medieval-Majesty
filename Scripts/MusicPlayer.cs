using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSource;

    void Start()
    {
        introSource.Play();
        PlayMusic();
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }
    public void PlayMusic()
    {
        //introSource.Stop();
        //loopSource.Stop();
        StartCoroutine(AwakeMusic());
    }
    IEnumerator AwakeMusic()
    {
        while (introSource.volume < 0.5f || loopSource.volume < 0.5f)
        {
            introSource.volume += Time.deltaTime * 0.5f;
            loopSource.volume += Time.deltaTime * 0.5f;
            yield return null;
        }
        //while (introSource.volume < 0.5f)
        //{
        //    introSource.volume += Time.deltaTime * 0.5f;
        //    yield return null;
        //}

        //while (loopSource.volume < 0.5f)
        //{
        //    loopSource.volume += Time.deltaTime * 0.5f;
        //    yield return null;
        //}
    }
    public void StopMusic()
    {
        //introSource.Stop();
        //loopSource.Stop();
        StartCoroutine(FadingMusic());
    }
    IEnumerator FadingMusic()
    {
        while (introSource.volume > 0f || loopSource.volume > 0f)
        {
            introSource.volume -= Time.deltaTime * 0.5f;
            loopSource.volume -= Time.deltaTime * 0.5f;
            yield return null;
        }
        //while (introSource.volume > 0f)
        //{
        //    introSource.volume -= Time.deltaTime * 0.5f;
        //    yield return null;
        //}

        //while (loopSource.volume > 0f)
        //{
        //    loopSource.volume -= Time.deltaTime * 0.5f;
        //    yield return null;
        //}
    }

}
