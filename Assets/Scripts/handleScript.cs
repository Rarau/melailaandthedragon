using UnityEngine;
using System.Collections;

public class handleScript : MonoBehaviour {
    public SlotMachineController sluts;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        sluts.OnMouseDown();
    }
}
