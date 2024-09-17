using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
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
}
