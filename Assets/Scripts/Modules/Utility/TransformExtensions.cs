using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void Flip(this Transform transform, int axis)
    {
        var flipScale = transform.localScale;
        flipScale.x = axis;

        transform.localScale = flipScale;
    }


}
