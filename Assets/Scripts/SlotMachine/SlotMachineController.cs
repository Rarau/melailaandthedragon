using UnityEngine;
using System;
using System.Collections;

public struct SlotMachineResult
{
    public int numAttacks;
    public int attackType;
    public bool critical;
}

public class SlotMachineController : MonoBehaviour
{
    public event Action<SlotMachineResult> reelsStoppedEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Slot machine reels stopped");
            if (reelsStoppedEvent != null)
            {
                SlotMachineResult r;
                r.numAttacks = 3;
                r.critical = false;
                r.attackType = 1;
                reelsStoppedEvent(r);
            }
        }
	}
}
