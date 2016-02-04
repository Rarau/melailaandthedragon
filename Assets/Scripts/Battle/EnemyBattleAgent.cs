using UnityEngine;
using System.Collections;
using System;

public class EnemyBattleAgent : MonoBehaviour, IBattleAgent {    

    public event System.Action turnEndedEvent;
    public event System.Action spawnEvent;
    public event System.Action deadEvent;
    public event System.Action<float> attackEvent;

    public float initalHP = 100f;
    public float hp = 100.0f;
    public bool dead;
    public Animation animation;

    public Transform spawnPos;
    public Transform combatPos;
    public Transform deathResetPos;

	// Use this for initialization
	void Awake () {
        animation = GetComponent<Animation>();
    }

    void OnEnable()
    {
        dead = false;
        hp = initalHP;
        animation.CrossFade("Idle", 0.0f);
        transform.position = spawnPos.position;
        StartCoroutine(gameObject.GoToPos(combatPos.position, 3.0f, () =>
        {
            if (spawnEvent != null)
                spawnEvent();
        }));
    }

    public void StartTurn()
    {
        if (dead)
            return;
        Debug.Log("Enemy turn started");

        //throw new System.NotImplementedException();
        animation.clip = animation.GetClip("Attack");
        StartCoroutine(animation.WhilePlaying(() =>
        {
            
            if (attackEvent != null)
            {
                // TO-DO: Randomize damage or something ???
                attackEvent(10.0f);
            }
            if (turnEndedEvent != null)
                turnEndedEvent();

            animation.CrossFade("Idle", 1.65f);

        }));

    }


    public void ReceiveAttack(float damage)
    {
        if (dead)
            return;
       // StopAllCoroutines();

        hp = Mathf.Max(0.0f, hp - damage);
        if (hp <= 0.0f && deadEvent != null)
        {
            dead = true;
            transform.position = new Vector3(transform.position.x, deathResetPos.position.y,transform.position.z);
            this.StartCoroutine(gameObject.GoToPos(deathResetPos.position, 1.5f,() => {
                Debug.Log("Monster Dead"); gameObject.SetActive(false); }));
            animation.clip = animation.GetClip("Dead");
            StartCoroutine(animation.WhilePlaying(() =>
            {
                deadEvent();

            }));
        }
    }

    public void BattleEnded()
    {
        Debug.Log("Monster Battle ended");
       // StopAllCoroutines();
    }

    public void SetActive(bool wat)
    {
        gameObject.SetActive(wat);
    }
    //public void monsterDefeated()
    //{

    //}
}
