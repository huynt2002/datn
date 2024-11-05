using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyEvent : MonoBehaviour
{
    // Destroy particles when animation has finished playing. 
    // destroyEvent() is called as an event in animations.
    [HideInInspector]
    public SpawnManager.EffectType effectType = SpawnManager.EffectType.None;
    Animator animator;
    float count;
    void Start()
    {
        if (effectType != SpawnManager.EffectType.None)
        {
            animator = GetComponent<Animator>();
            string animationClipName = effectType.ToString();
            animator.Play(animationClipName);
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == animationClipName)
                {
                    count = clip.length;
                    return;
                }
            }
        }
        destroyEvent();
    }

    void Update()
    {
        if (count > 0)
        {
            count -= Time.deltaTime;
        }
        else
        {
            destroyEvent();
        }
    }

    public void destroyEvent()
    {
        Destroy(gameObject);
    }
}
