using UnityEngine;
using System.Collections;

public class HandleController : MonoBehaviour
{
	public ReelController[] reels;

	Animation animation;
	public int curReel = 0;

	// Use this for initialization
	void Start () 
	{

		animation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space) && curReel < 3) {
			reels[curReel].Rotate(Random.Range(0, 5));
			curReel ++;
		}
	}

	void OnMouseDown()
	{
		Debug.Log ("Spinning To Winning!");
		animation.Play ();
		foreach (ReelController reel in reels) {
			reel.rotating = true;
		}
		curReel = 0;

	}

	public void HandleDownAnimation()
	{
		//animation.Stop();
	}
}
