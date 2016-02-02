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
	
	// Update is called once per frame
	void Update () {
	
	}



    public void StartTurn()
    {
        Debug.Log("Enemy turn started");

        //throw new System.NotImplementedException();
        animation.clip = animation.GetClip("Slime_Attack");
        StartCoroutine(animation.WhilePlaying(() =>
        {
            if (attackEvent != null)
                attackEvent(10.0f);
            if (turnEndedEvent != null)
                turnEndedEvent();

            animation.CrossFade("Slime_Idle", 1.65f);

        }));

        //StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(4.0f);

        if (attackEvent != null)
            attackEvent(10.0f);

        if (turnEndedEvent != null)
            turnEndedEvent();
    }

    public void ReceiveAttack(float damage)
    {
        if (dead)
            return;

        hp = Mathf.Max(0.0f, hp - damage);
        if (hp <= 0.0f && deadEvent != null)
        {
            animation.clip = animation.GetClip("Slime_Dead");
            StartCoroutine(animation.WhilePlaying(() =>
            {
                deadEvent();
                dead = true;
            }));
        }
    }




    public void BattleEnded()
    {
        //throw new System.NotImplementedException();
    }
}
