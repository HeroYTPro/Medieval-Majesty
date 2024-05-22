using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public AudioMixerGroup mixerGroup;

    public float volume = 1f;
    public bool
        playOnEnter = true,
        playOnExit = false,
        playAfterDelay = false;

    public float playDelay = 0.25f;
    private float timeSenceEntered = 0;
    private bool hasDelayedSoundPlayer = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            GameObject gameObject = animator.gameObject;
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = mixerGroup;
            source.PlayOneShot(soundToPlay, volume);
            Destroy(source, soundToPlay.length);
        }

        timeSenceEntered = 0f;
        hasDelayedSoundPlayer = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelayedSoundPlayer)
        {
            timeSenceEntered += Time.deltaTime;

            if (timeSenceEntered > playDelay)
            {
                GameObject gameObject = animator.gameObject;
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = mixerGroup;
                source.PlayOneShot(soundToPlay, volume);
                Destroy(source, soundToPlay.length);
                hasDelayedSoundPlayer = true;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            GameObject gameObject = animator.gameObject;
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = mixerGroup;
            source.PlayOneShot(soundToPlay, volume);
            Destroy(source, soundToPlay.length);
        }
    }

}
