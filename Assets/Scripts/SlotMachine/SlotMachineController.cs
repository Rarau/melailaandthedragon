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

    public ReelController[] reels;

    Animation animation;
    public int curReel = 0;

	// Use this for initialization
	void Start () {
        reels = GetComponentsInChildren<ReelController>();
        animation = GetComponentInChildren<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && curReel < 3)
        {
            reels[curReel].Rotate(UnityEngine.Random.Range(0, 5));            
            if (curReel == 2)
            {
                Debug.Log("Slot machine reels stopped");
                if (reelsStoppedEvent != null)
                {
                    // TO-DO: This is just hardcoded nonsense!!
                    // Remove this and check the reels to fill the parameters
                    SlotMachineResult r;
                    r.numAttacks = 3;
                    r.critical = false;
                    r.attackType = 1;
                    reelsStoppedEvent(r);
                }
            }
            curReel++;
        }

        if (Input.GetKeyDown(KeyCode.A))
            OnMouseDown();
	}

    void OnMouseDown()
    {

        Debug.Log("Spinning To Winning!");
        animation.Play();
        foreach (ReelController reel in reels)
        {
            reel.rotating = true;
        }
        curReel = 0;

    }
}
