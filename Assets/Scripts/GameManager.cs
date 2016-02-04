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


	// Use this for initialization
	void Start () {
        battleManager = FindObjectOfType<BattleManager>();

        fsm = new StateMachine<GameManager>(this);
        fsm.setState(new IntroState());
        battleManager.battleEndedEvent += battleEnded;

        timer = 3.0f;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void battleEnded()
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
        //if (Input.GetKeyDown("x"))
        //{
        //    enemy.SetActive(false);
        //    enemyCleared = true;
        //    enemy.transform.Translate(new Vector3(0, 0, -18.41f));
        //}
        //if (enemyCleared == true)
        //{
        //    timer -= Time.deltaTime;
        //}
        //else if (enemyCleared == false && enemy.transform.position.z >= -0.54f)
        //{
        //  enemy.transform.Translate(new Vector3(0, 0, 6.0f * Time.deltaTime), Space.Self);
        //}
        ////27.72 , 3.67 , -0.54
        ////18.41
        //if (timer <= 0)
        //{
        //    enemy.SetActive(true);
        //    enemyCleared = false;
        //    timer = 3.0f;
        //}
       
        //if (GameObject.FindGameObjectsWithTag("Slime") == null)
        //{
        //    Instantiate()
        //}
        HandleController HandleScript = FindObjectOfType<HandleController>();
        ReelController ReelScript = FindObjectOfType<ReelController>();
     
	}
}
