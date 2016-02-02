using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour {

    StateMachine<GameManager> fsm;

    public event Action handleDownEvent;

	// Use this for initialization
	void Start ()
    {
        fsm = new StateMachine<GameManager>(this);
        fsm.setState(new IdleState());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (handleDownEvent != null)
                handleDownEvent();
	}
}
