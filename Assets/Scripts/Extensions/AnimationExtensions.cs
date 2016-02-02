using UnityEngine;
using System;
using System.Collections;

public static class AnimationExtensions
{
    public static IEnumerator WhilePlaying(this Animation animation, Action onEnd = null)
    {
        Debug.Log("ANimation playing");
        animation.Play();
        do
        {
            Debug.Log("ANimation playing");
            yield return null;
        } while (animation.isPlaying);

        if (onEnd != null)
            onEnd();
    }
}