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
    public Animation healAnimation;

    public float hp = 100.0f;


	// Use this for initialization
	void Start () {
        slotMachine.reelsStoppedEvent += OnSlotMachineReelsStopped;
        //slotMachine.enabled = false;
	}
	
    void Medic(float amount)
    {
        hp = Mathf.Min(hp + amount,100);
    }
	// Update is called once per frame
	void Update () {
	
	}


    public void StartTurn()
    {
   //     Debug.Log("Player turn started");

        slotMachine.enabled = true;
    }

    public void OnSlotMachineReelsStopped(SlotMachineResult reelsResult)
    {
        // Debug.Log("Reels stopped player");
        if (reelsResult.attackType == 0)
        {
            StartCoroutine(DoHeals(reelsResult.numAttacks, reelsResult.critical));
        }
        else
        {
            StartCoroutine(DoAttacks(reelsResult.numAttacks, reelsResult.critical));
        }
    }

    IEnumerator DoHeals(int numHeals, bool crit)
    {
        //Debug.Log("Player attacked once");
        
        for (int i = 0; i < numHeals; ++i)
        {
            float healAmount = 10.0f;

            healAnimation.Rewind();
            healAnimation.Stop();
            yield return StartCoroutine(healAnimation.WhilePlaying(() =>
            {
                if (crit == true)
                {
                    healAmount *= 2;
                }
                Medic(healAmount);
            }));
        }
        yield return null;
        if (turnEndedEvent != null)
        {
            slotMachine.enabled = false;
            turnEndedEvent();
        }
    }
    IEnumerator DoAttacks(int numAttacks,bool crit)
    {
        //Debug.Log("Player attacked once");

        for (int i = 0; i < numAttacks; ++i)
        {
            float damage = 10.0f;

            // TO-DO: ACtually parse reelsResult data and attack or heal as stablished
            attackAnimation.Rewind();
            attackAnimation.Stop();
            yield return StartCoroutine(attackAnimation.WhilePlaying(() => 
            {
                if (attackEvent != null)
                {
                    
                    if (crit == true)
                        damage*=2;
                    
                    attackEvent(damage);
                }
        //        Debug.Log("Player attacked once");
                Debug.Log("Damage Dealt: " + damage);
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
      //  Debug.Log("Player battle ended");
        StopAllCoroutines();
    }


    public void SetActive(bool wat)
    {
        gameObject.SetActive(wat);
    }
}
