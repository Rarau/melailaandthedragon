using UnityEngine;
using System.Collections;
using System;

public interface IBattleAgent
{
    event Action turnEndedEvent;
    event Action<float> attackEvent;
    event Action deadEvent;

    void StartTurn();
    void ReceiveAttack(float damage);
}

public class BattleManager : MonoBehaviour {

    public IBattleAgent player;
    public IBattleAgent enemy;

    public event Action monsterDefeatedEvent;
    public event Action playerDefeatedEvent;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<IBattleAgent>();
        enemy = GameObject.Find("Enemy").GetComponent<IBattleAgent>();

        player.turnEndedEvent += OnPlayerTurnEnded;
        player.attackEvent += OnPlayerAttack;
        player.deadEvent += OnPlayerDead;

        enemy.turnEndedEvent += OnEnemyTurnEnded;
        enemy.attackEvent += OnEnemyAttack;
        enemy.deadEvent += OnEnemyDead;

        // Player starts attacking, can be changed tho
        player.StartTurn();
	}

    void OnPlayerTurnEnded()
    {
        Debug.Log("Player's turn ended");
        enemy.StartTurn();
    }

    void OnEnemyTurnEnded()
    {
        player.StartTurn();
    }

    void OnPlayerAttack(float damage)
    {
        // TO - DO: do attack damage math and stuff
        enemy.ReceiveAttack(damage);
    }

    void OnEnemyAttack(float damage)
    {
        // TO - DO: do attack damage math and stuff
        player.ReceiveAttack(damage);
    }

    void OnPlayerDead()
    {
        Debug.Log("YOU DIEDED...");
        if (playerDefeatedEvent != null)
            playerDefeatedEvent();
    }

    void OnEnemyDead()
    {
        Debug.Log("ENEMY Killed GG");
        if (monsterDefeatedEvent != null)
            monsterDefeatedEvent();
    }
}
