using UnityEngine;
using System;
using System.Collections;

public class PlayerBattleAgent : MonoBehaviour, IBattleAgent 
{
    public SlotMachineController slotMachine;
    public event Action turnEndedEvent;
    public event Action deadEvent;
    public event Action<float> attackEvent;

    public Animation attackAnimation;

    public float hp = 100.0f;


	// Use this for initialization
	void Start () {
        slotMachine.reelsStoppedEvent += OnSlotMachineReelsStopped;
        Debug.Log(" player");

        //slotMachine.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartTurn()
    {
        Debug.Log("Player turn started");

        slotMachine.enabled = true;
    }

    public void OnSlotMachineReelsStopped(SlotMachineResult reelsResult)
    {
        Debug.Log("Reels stopped player");
        StartCoroutine(DoAttacks(3));


    }

    IEnumerator DoAttacks(int numAttacks)
    {
        Debug.Log("Player attacked once");

        for (int i = 0; i < numAttacks; ++i)
        {
            // TO-DO: ACtually parse reelsResult data and attack or heal as stablished
            attackAnimation.Rewind();
            attackAnimation.Stop();
            yield return StartCoroutine(attackAnimation.WhilePlaying(() => 
            {
                if (attackEvent != null)
                    attackEvent(10.0f);
                Debug.Log("Player attacked once");
            }));
        }

        if (turnEndedEvent != null)
        {
            slotMachine.enabled = false;
            turnEndedEvent();
        }
    }

    public void ReceiveAttack(float damage)
    {
        hp = Mathf.Max(0.0f, hp - damage);
        if (hp <= 0.0f && deadEvent != null)
            deadEvent();
    }

    public void BattleEnded()
    {
        Debug.Log("Player battle ended");
        StopAllCoroutines();
    }

}
