using UnityEngine;
using System.Collections;

public class InCombatState : GameState {


    public void enter(GameManager agent)
    {
    }

    public void exit(GameManager agent)
    {
    }

    public void execute(GameManager agent, StateMachine<GameManager> fsm)
    {

    }

    protected override void OnEnemyDied()
    {
        Debug.Log("Enemy DIED");
    }
}
