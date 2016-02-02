using UnityEngine;
using System.Collections;

public class IdleState : GameState {


    public void enter(GameManager agent)
    {
    }

    public void exit(GameManager agent)
    {
    }

    public void execute(GameManager agent, StateMachine<GameManager> fsm)
    {

    }

    protected override void OnHandleDown()
    {
        Debug.Log("Handle down idle");
    }
}
