using UnityEngine;
using System.Collections;

public class EnemyBattleAgent : MonoBehaviour, IBattleAgent {    

    public event System.Action turnEndedEvent;
    public event System.Action deadEvent;
    public event System.Action<float> attackEvent;

    public float hp = 100.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void StartTurn()
    {
        Debug.Log("Enemy turn started");

        //throw new System.NotImplementedException();
        StartCoroutine(DoAttack());
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
        hp = Mathf.Max(0.0f, hp - damage);
        if (hp <= 0.0f && deadEvent != null)
            deadEvent();
    }


}
