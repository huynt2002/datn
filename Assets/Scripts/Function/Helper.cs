using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public enum ObjPosition
    {
        Top, Bottom, Center
    }
    public static float Map(float value, float min1, float max1, float min2, float max2, bool clamp = false)
    {
        float val = min2 + (max2 - min2) * (value - min1) / (max1 - min1);

        return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
    }
    public static void Flip(Transform obj, float x)
    {
        float direction = Mathf.Sign(x);
        obj.localScale = new Vector3(direction, 1, 1);
    }
    public static Vector2 GetPos(GameObject obj, ObjPosition position = ObjPosition.Center)
    {
        var collider = GetEntityCollider(obj);
        var pos = new Vector2(obj.transform.position.x, obj.transform.position.y);
        if (collider != null)
        {
            pos = new Vector2(collider.transform.position.x, collider.transform.position.y);
            switch (position)
            {
                case ObjPosition.Top:
                    return new Vector2(pos.x, pos.y + collider.bounds.size.y / 2);
                case ObjPosition.Bottom:
                    return new Vector2(pos.x, pos.y - collider.bounds.size.y / 2);
            }
        }
        return pos;
    }

    static Collider2D GetEntityCollider(GameObject obj)
    {
        var colliders = obj.GetComponentsInChildren<Collider2D>();
        foreach (var collider in colliders)
        {
            if (collider.tag == "Enemy" || collider.tag == "Player")
            {
                return collider;
            }
        }
        foreach (var collider in colliders)
        {
            if (!collider.isTrigger)
            {
                return collider;
            }
        }

        return null;
    }

}
