using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	public float hp = 100.0f;
	public float currentHp;
	// Use this for initialization
	void Start () {
		currentHp = hp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReceiveAttack(float damage) {
		currentHp = Mathf.Clamp (currentHp - damage, 0.0f, hp);
	}
}
