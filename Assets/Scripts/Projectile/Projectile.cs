using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage { get; private set; }
    public float speed;
    public int direction { get; private set; }
    public float timeToLive = -1;
    ProjectileBehavior[] behaviors;

    public void SetUp(float damage, float direction, LayerMask detectType)
    {
        this.direction = (int)direction;
        this.damage = damage;
        gameObject.layer = detectType;
    }

    private void Start()
    {
        behaviors = GetComponents<ProjectileBehavior>();
    }

    private void Update()
    {
        foreach (var behavior in behaviors)
        {
            behavior.Behaving();
        }
    }
}
