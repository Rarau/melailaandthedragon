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
    public GameObject[] buttons;
    public event Action<SlotMachineResult> reelsStoppedEvent;
    public Camera camera; //create camera object to affect the intended camera for raycast, else Camera.Main finds first main camera.
    Ray ray;
    RaycastHit rayHit;
    public ReelController[] reels;

    Animation animation;
    public int curReel = 0;
    private bool reelsDirty = true;
	// Use this for initialization

    void OnEnable()
    {
        reelsDirty = true;
    }

	void Start () {
        reels = GetComponentsInChildren<ReelController>();
        animation = GetComponentInChildren<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (reelsDirty)
            return;
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit, 100.0f) && Input.GetMouseButtonDown(0) /*&& ReelScript.rotating == true*/)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (rayHit.collider.gameObject == buttons[i].gameObject)
                {
                    reels[i].Rotate(UnityEngine.Random.Range(0, 5));
                    Debug.Log(i + "Clicked");
                    if(checkReelsStopped() == 3)
                    {
                        if (reelsStoppedEvent != null)
                        {
                            SlotMachineResult r;
                            r.numAttacks = int.Parse(reels[1].getIconString());
                            r.critical = reels[2].getIconString() == "Crit";
                            if(r.critical)
                            {
                                Debug.Log("CRIT");
                            }
                            r.attackType = reels[0].getIconString() == "Heal" ? 0 : 1;
                            enabled = false;
                            reelsStoppedEvent(r);
                            //return;
                        }
                    }
                }
            }
        }
        /*

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
                    r.numAttacks = int.Parse(reels[1].getIconString());
                    r.critical = reels[2].getIconString() == "Crit";
                    r.attackType = reels[0].getIconString() == "Heal" ? 0:1;
                    reelsStoppedEvent(r);
                }
            }
            curReel++;
        }
         */

      //  if (Input.GetKeyDown(KeyCode.A))
      //      OnMouseDown();
	}

    int checkReelsStopped()
    {
        int r = 0;
        for (int i=0;i<reels.Length;i++)
        {
            if (!reels[i].rotating)
                r++;                
        }
        return r;
    }

    public void OnMouseDown()
    {
        // Cehck that the machine is enabled and all reels are stopped
        if (!enabled || checkReelsStopped() != reels.Length || !reelsDirty)
            return;

        reelsDirty = false;
        Debug.Log("Spinning To Winning!");
        animation.Play();
        foreach (ReelController reel in reels)
        {
            reel.rotating = true;
        }
        curReel = 0;

    }
    
}
