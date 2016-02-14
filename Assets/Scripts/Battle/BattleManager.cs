using UnityEngine;
using System.Collections;
using System;

public interface IBattleAgent
{
    event Action turnEndedEvent;
    event Action<float> attackEvent;
    event Action deadEvent;
    void SetActive(bool active);


    void StartTurn();
    void BattleEnded();

    void ReceiveAttack(float damage);

    GameObject GetGameObject();
}

public class BattleManager : MonoBehaviour {
    public TerrainScroller terrainThingy;
    public IBattleAgent player;
    public GameObject[] enemyPool;
    public IBattleAgent currentEnemy;

    public event Action monsterDefeatedEvent;
    public event Action playerDefeatedEvent;
    public event Action battleEndedEvent;

    public EnemyHealthBar enemyHealthBar;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<IBattleAgent>();
        enemyPool = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemyPool)
        {
            go.SetActive(false);
        }
        //currentEnemy = GameObject.FindWithTag("Enemy").GetComponent<IBattleAgent>();
        currentEnemy = enemyPool[UnityEngine.Random.Range(0, enemyPool.Length)].GetComponent<IBattleAgent>();
        currentEnemy.SetActive(true);

        terrainThingy = GameObject.FindObjectOfType<TerrainScroller>();

        player.turnEndedEvent += OnPlayerTurnEnded;
        player.attackEvent += OnPlayerAttack;
        player.deadEvent += OnPlayerDead;

        terrainThingy.enabled = false;
        enemyHealthBar = FindObjectOfType<EnemyHealthBar>();
        //currentEnemy.turnEndedEvent += OnEnemyTurnEnded;
       // currentEnemy.attackEvent += OnEnemyAttack;
       // currentEnemy.deadEvent += OnEnemyDead;

        // Player starts attacking, can be changed tho
        //player.StartTurn();
	}

    public void StartBattle()
    {


        currentEnemy.turnEndedEvent += OnEnemyTurnEnded;
        currentEnemy.attackEvent += OnEnemyAttack;
        currentEnemy.deadEvent += OnEnemyDead;

        currentEnemy.SetActive(true);

        // Maybe play some animations?
        terrainThingy.enabled = false;
        player.StartTurn();

        enemyHealthBar.SetTarget(currentEnemy.GetGameObject().GetComponent<EnemyBattleAgent>());    
        enemyHealthBar.gameObject.SetActive(true);

    }



    void OnPlayerTurnEnded()
    {
       // Debug.Log("Player's turn ended");
        currentEnemy.StartTurn();
    }

    void OnEnemyTurnEnded()
    {
        player.StartTurn();
    }

    void OnPlayerAttack(float damage)
    {
        // TO - DO: do attack damage math and stuff
        currentEnemy.ReceiveAttack(damage);
    }

    void OnEnemyAttack(float damage)
    {
        // TO - DO: do attack damage math and stuff

        player.ReceiveAttack(damage);
    }

    void OnPlayerDead()
    {
     //   Debug.Log("YOU DIEDED...");
        if (playerDefeatedEvent != null)
            playerDefeatedEvent();

        player.BattleEnded();
        currentEnemy.BattleEnded();
    }

    void OnEnemyDead()
    {
        
     //   Debug.Log("ENEMY Killed GG");
        if (monsterDefeatedEvent != null)
            monsterDefeatedEvent();

        player.BattleEnded();
        currentEnemy.BattleEnded();

        if (battleEndedEvent != null)
        terrainThingy.enabled = true;
        currentEnemy.SetActive(false);
        battleEndedEvent();

        currentEnemy.turnEndedEvent -= OnEnemyTurnEnded;
        currentEnemy.attackEvent -= OnEnemyAttack;
        currentEnemy.deadEvent -= OnEnemyDead;

        currentEnemy = enemyPool[UnityEngine.Random.Range(0, enemyPool.Length)].GetComponent<IBattleAgent>();

        enemyHealthBar.gameObject.SetActive(false);
    }
}
