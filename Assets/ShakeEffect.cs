﻿using UnityEngine;
using System.Collections;

public class ShakeEffect : MonoBehaviour {
    public float duration = 0.85f;
    public float magnitude = 0.5f;
    public float frequency = 5.0f;

    public bool playOnAwake;
    public bool selfDisable = true;
	// Use this for initialization
	void OnEnable () {
        if (playOnAwake)
            StartCoroutine(Shake());
	}
	

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Mathf.PerlinNoise(percentComplete * frequency, 0.0f) * 2.0f - 1.0f; //Random.value * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(0.0f, percentComplete * frequency) * 2.0f - 1.0f;//Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;


            transform.position = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z);

            yield return null;
        }

       transform.position = originalCamPos;
       enabled = !selfDisable;
    }

}
