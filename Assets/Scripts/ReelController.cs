using UnityEngine;
using System.Collections;

public class ReelController : MonoBehaviour 
{
	public float rotSpeed = 1.0f;
	int numIcons = 5;

	public bool rotating = false;
	public Renderer meshRenderer;
	public float finalAngle;
	public Quaternion initRot;
	Vector3 center;
	// Use this for initialization
	void Start () 
	{
		initRot = transform.localRotation;

		meshRenderer = GetComponent<Renderer> ();

		center = meshRenderer.bounds.center;
	}

	public void Rotate(int iconNumber)
	{
        if (!rotating)
            return;
		finalAngle = (360.0f / numIcons) * iconNumber + 0.25f * (360.0f / numIcons);
		transform.localRotation = initRot;
		transform.localPosition = Vector3.zero;
		transform.RotateAround (center, Vector3.right, finalAngle);
		rotating = false;
	}



	// Update is called once per frame
	void Update () 
	{
		//transform.localRotation *= Quaternion.Euler (rotSpeed * Time.deltaTime, 0.0f, 0.0f);
		if(rotating)
			transform.RotateAround (center, Vector3.right, Time.deltaTime * rotSpeed);
	}
}
