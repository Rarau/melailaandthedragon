using UnityEngine;
using System;
using System.Collections;

public class PlayerBattleAgent : MonoBehaviour, IBattleAgent 
{
    public SlotMachineController slotMachine;
    public event Action turnEndedEvent;
    public event Action deadEvent;
    public event Action<float> attackEvent;

    public float hp = 100.0f;


	// Use this for initialization
	void Start () {
        slotMachine.reelsStoppedEvent += OnSlotMachineReelsStopped;
        slotMachine.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartTurn()
    {
        slotMachine.enabled = true;
    }

    public void OnSlotMachineReelsStopped(SlotMachineResult reelsResult)
    {
        Debug.Log("Reels stopped player");
        for (int i = 0; i < reelsResult.numAttacks; ++i)
        {            
            // TO-DO: ACtually parse reel data and attack or heal as stablished
            if (attackEvent != null)
                attackEvent(10.0f);
        }
        if (turnEndedEvent != null)
        {
            turnEndedEvent();
        }
    }



    public void ReceiveAttack(float damage)
    {
        hp = Mathf.Max(0.0f, hp - damage);
        if (hp <= 0.0f && deadEvent != null)
            deadEvent();
    }


}
