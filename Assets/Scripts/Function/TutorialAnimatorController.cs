using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimatorController : MonoBehaviour
{
    [SerializeField] GameObject animatorObj;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] bool disposeWhenExit = false;
    void Start()
    {
        animatorObj.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        animatorObj.SetActive(true);
        animatorObj.GetComponent<Animator>().Play(animationClip.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (disposeWhenExit)
        {
            Destroy(animatorObj);
        }
    }
}
