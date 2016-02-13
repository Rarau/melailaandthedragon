using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RandomClipPlayer : MonoBehaviour {

    public AudioClip[] clips;
    private AudioSource aSource;

    void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void OnEnable () {
        
        int clip = Random.Range(0, clips.Length);
        aSource.clip = clips[clip];
        aSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
