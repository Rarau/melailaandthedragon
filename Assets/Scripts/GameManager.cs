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

    public GameObject[] buttons;
    public GameObject slime;
    public GameObject[] enemyType;
    List<GameObject> enemies;
    public bool enemyCleared = false;
    public float timer;
    private GameObject enemy;

    public BattleManager battleManager;

    public Camera camera; //create camera object to affect the intended camera for raycast, else Camera.Main finds first main camera.
    Ray ray;
    RaycastHit rayHit;
	// Use this for initialization
	void Start () {
        battleManager = FindObjectOfType<BattleManager>();

        fsm = new StateMachine<GameManager>(this);
        fsm.setState(new IntroState());



        buttons = GameObject.FindGameObjectsWithTag("buttons").OrderBy(go => go.name).ToArray(); // Sorts array of gameobjects in order by name. requires System.Linq
        timer = 3.0f;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void SpawnEnemy()
    {

    }

    void Update ()
    {
        if (Input.GetKeyDown("x"))
        {
            enemy.SetActive(false);
            enemyCleared = true;
            enemy.transform.Translate(new Vector3(0, 0, -18.41f));
        }
        if (enemyCleared == true)
        {
            timer -= Time.deltaTime;
        }
        else if (enemyCleared == false && enemy.transform.position.z >= -0.54f)
        {
          enemy.transform.Translate(new Vector3(0, 0, 6.0f * Time.deltaTime), Space.Self);
        }
        //27.72 , 3.67 , -0.54
        //18.41
        if (timer <= 0)
        {
            enemy.SetActive(true);
            enemyCleared = false;
            timer = 3.0f;
        }
       
        //if (GameObject.FindGameObjectsWithTag("Slime") == null)
        //{
        //    Instantiate()
        //}
        HandleController HandleScript = FindObjectOfType<HandleController>();
        ReelController ReelScript = FindObjectOfType<ReelController>();
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit,100.0f) && Input.GetMouseButtonDown(0) /*&& ReelScript.rotating == true*/)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (rayHit.collider.gameObject == buttons[i].gameObject)
                {
                    HandleScript.reels[i].Rotate(UnityEngine.Random.Range(0, 5));
                    Debug.Log(i + "Clicked");
                }
            }
        }
	}
}
