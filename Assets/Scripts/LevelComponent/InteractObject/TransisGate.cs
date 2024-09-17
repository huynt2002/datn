using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransisGate : PlayerInteract
{
    public GameObject[] transisComponent;
    void Start()
    {
        interactType = InteractType.Gate;
        for (int i = 0; i < transisComponent.Length; i++)
            transisComponent[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance)
            if (LevelManager.instance.checkClear)
                for (int i = 0; i < transisComponent.Length; i++)
                    transisComponent[i].SetActive(true);
    }

    public override void Gate()
    {
        LevelManager.instance.Transis(true);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (LevelManager.instance)
            if (LevelManager.instance.checkClear)
            {
                InfoUIManager.instance.SetInfo(gameObject.transform, Color.white);
            }
    }
}
