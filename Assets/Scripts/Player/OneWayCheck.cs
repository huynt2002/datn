using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject pCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerMovement.isOneWay)
        {
            Collider2D platform = GameObject.FindWithTag("OneWayPlatform").GetComponent<Collider2D>();
            foreach (var i in pCollider.GetComponents<Collider2D>()) Physics2D.IgnoreCollision(i, platform, false);
            playerMovement.isOneWay = false;
        }
    }
}
