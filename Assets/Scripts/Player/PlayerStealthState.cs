using UnityEngine;

public class PlayerStealthState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player) 
    {
        Debug.Log("Enter Stealth State");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        var input = player.input;

        if (!input.StealthHeld)
        {
            player.TransitionToState(player.walkState);
        }
        else if (input.MoveInput.magnitude < 0.1f)
        {
            player.TransitionToState(player.idleState);
        }
    }

    public override void FixedUpdateState(PlayerStateMachine player)
    {
        Vector3 dir = new Vector3(player.input.MoveInput.x, 0f, player.input.MoveInput.y);
        Vector3 move = player.transform.TransformDirection(dir.normalized) * player.input.stealthSpeed * Time.fixedDeltaTime;
        player.rb.MovePosition(player.rb.position + move);
    }

    public override void ExitState(PlayerStateMachine player) 
    {
        Debug.Log($"Exit {player.currentStateType} State");
    }
}

