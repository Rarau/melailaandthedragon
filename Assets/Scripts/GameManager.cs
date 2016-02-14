using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    /**
     * GAME STATES:
     * GAME_INTRO
     * ENEMY_SPAWN
     * PLAYER_TURN
     * ENEMY_TURN
     * BATTLE_END
     * 
     */
    StateMachine<GameManager> fsm;

    public event Action enemyDeadEvent;
    public int battleCount;

    public GameObject slime;
    public GameObject[] enemyType;
    List<GameObject> enemies;
    public bool enemyCleared = false;
    public float timer;
    private GameObject enemy;

    public BattleManager battleManager;
    public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
        battleManager = FindObjectOfType<BattleManager>();

        fsm = new StateMachine<GameManager>(this);
        fsm.setState(new IntroState());
        battleManager.battleEndedEvent += OnBattleEnded;
        battleManager.playerDefeatedEvent += OnPlayerDefeated;

        timer = 3.0f;
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        battleManager.StartBattle();

    }

    void OnPlayerDefeated()
    {
        gameOverScreen.SetActive(true);
    }

    void OnBattleEnded()
    {
        battleCount += 1;
        
        if (battleCount <= 5)
        {
            StartCoroutine(pause());
        }

    }
    IEnumerator pause() { yield return new WaitForSeconds(4.1f); battleManager.StartBattle(); }
    void SpawnEnemy()
    {

    }

    void Update ()
    {
        HandleController HandleScript = FindObjectOfType<HandleController>();
        ReelController ReelScript = FindObjectOfType<ReelController>();
	}

    public void PlayAgainOnClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
