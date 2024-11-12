using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] AnimationClip idle;
    [SerializeField] AnimationClip move;
    [SerializeField] AnimationClip jump;
    [SerializeField] AnimationClip getHit;
    [SerializeField] AnimationClip dash;
    [SerializeField] AnimationClip skill;
    [SerializeField] AnimationClip attack1;
    [SerializeField] AnimationClip attack2;
    [SerializeField] AnimationClip attack3;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    public void PlayIdleAnimation()
    {
        PlayAnimation(idle.name);
    }

    public void PlaySkillAnimation()
    {
        PlayAnimation(skill.name);
    }

    public void PlayDashAnimation()
    {
        PlayAnimation(dash.name);
    }

    public void PlayMoveAnimation()
    {
        PlayAnimation(move.name);
    }

    public void PlayJumpAnimation()
    {
        PlayAnimation(jump.name);
    }

    public void PlayGetHitAnimation()
    {
        PlayAnimation(getHit.name);
    }


    public void PlayAttackAnimation(int attackNum)
    {
        switch (attackNum)
        {
            case 1: animator.Play(attack1.name); break;
            case 2: animator.Play(attack2.name); break;
            case 3: animator.Play(attack3.name); break;
        }
    }
}
