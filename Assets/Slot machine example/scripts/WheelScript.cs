using UnityEngine;
using System.Collections;

public class WheelScript : MonoBehaviour {

	public float spinTime = 5.0f;
	public float endPosition = 0.0f;
	public int spinSpeed = 500;
	public bool startSpinning = false;
	public bool isSpinning = false;
	public Material idleMaterial;
	public Material moveMaterial;
	public AudioClip stopSpinning;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//If we start the spinning then change the material to a blurry one to give it more effect.
		if(startSpinning){
			startSpinning = false;
			isSpinning = true;
			GetComponent<Renderer>().material = moveMaterial;
			StartCoroutine(spinning(spinTime, endPosition));
		}

		//If we are spinning then keep rotating the model at a high speed to give it more of a fast roll effect.
		if(isSpinning){
			transform.Rotate(new Vector3(spinSpeed * Time.deltaTime, 0.0f, 0.0f));
		}
	}

	//When spinning is over play a sound like the wheel stopped and set the texture back from blurry to normal.
	IEnumerator spinning (float spinningTime, float positionEnd)
	{
		yield return new WaitForSeconds(spinningTime);
		GetComponent<AudioSource>().PlayOneShot(stopSpinning, 1.0F);
		isSpinning = false;
		transform.rotation = Quaternion.Euler(positionEnd, 0.0f, 0.0f);
		GetComponent<Renderer>().material = idleMaterial;
	}

}
