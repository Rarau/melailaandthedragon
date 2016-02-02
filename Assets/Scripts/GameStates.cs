using UnityEngine;
using System.Collections;
public class GameStates : MonoBehaviour {
    public static GameStates currentState = null;
    public static GameStates lastState;
    enum GameState {
        MainMenu,
        Idle,
        Spinning,
        Moving,
        PlayerAction,
        EnemyAction,
        Spawning,
        YouWin,
        YouLose,
        Reset
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
