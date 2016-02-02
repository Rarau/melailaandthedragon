using UnityEngine;
using System.Collections;

public class EnemyBattleAgent : MonoBehaviour, IBattleAgent {    

    public event System.Action turnEndedEvent;
    public event System.Action deadEvent;
    public event System.Action<float> attackEvent;

    public float hp = 100.0f;
    bool dead;
    public Animation animation;


	// Use this for initialization
	void Start () {
        dead = false;
        animation = GetComponent<Animation>();
    }


    public void StartTurn()
    {
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

        hp = Mathf.Max(0.0f, hp - damage);
        if (hp <= 0.0f && deadEvent != null)
        {
            animation.clip = animation.GetClip("Dead");
            StartCoroutine(animation.WhilePlaying(() =>
            {
                deadEvent();
                dead = true;
            }));
        }
    }

    public void BattleEnded()
    {
        StopAllCoroutines();
    }
}
