using UnityEngine;
using System;
using System.Collections;

public static class AnimationExtensions
{
    public static IEnumerator WhilePlaying(this Animation animation, Action onEnd = null)
    {
        animation.Play();
        do
        {
            yield return null;
        } while (animation.isPlaying);

        if (onEnd != null)
            onEnd();
    }
}