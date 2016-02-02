using UnityEngine;
using System.Collections;

public class GameState : State<GameManager> 
{


    public void enter(GameManager agent)
    {
        agent.handleDownEvent += OnHandleDown;
    }

    public void exit(GameManager agent)
    {
        agent.handleDownEvent -= OnHandleDown;
    }

    public void execute(GameManager agent, StateMachine<GameManager> fsm)
    {

    }

    protected virtual void OnHandleDown()
    {
    }

}
