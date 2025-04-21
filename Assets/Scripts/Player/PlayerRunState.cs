using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player) 
    {
        Debug.Log("Enter Run state");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        var input = player.input;

        if (!input.RunHeld)
        {
            player.TransitionToState(player.walkState);
        }
        else if (input.MoveInput.magnitude < 0.1f)
        {
            player.TransitionToState(player.idleState);
        }
        else if (input.JumpPressed && player.IsGrounded() && !input.isJumpLocked)
        {
            player.TransitionToState(player.jumpState);
        }
    }

    public override void FixedUpdateState(PlayerStateMachine player)
    {
        Vector3 dir = new Vector3(player.input.MoveInput.x, 0, player.input.MoveInput.y);
        Vector3 move = player.transform.TransformDirection(dir.normalized) * player.input.runSpeed * Time.fixedDeltaTime;
        player.rb.MovePosition(player.rb.position + move);
    }

    public override void ExitState(PlayerStateMachine player) 
    {
        Debug.Log($"Exit {player.currentStateType} State");
    }
}

