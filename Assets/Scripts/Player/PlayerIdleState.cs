using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Enter Idle state");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        var input = player.input;

        if (!player.IsGrounded()) return;

        if (input.MoveInput.magnitude > 0.1f)
        {
            if (input.StealthHeld)
                player.TransitionToState(player.stealthState);
            else if (input.RunHeld)
                player.TransitionToState(player.runState);
            else
                player.TransitionToState(player.walkState);
        }
        else if (input.CrouchHeld)
        {
            player.TransitionToState(player.crouchState);
        }
        else if (input.JumpPressed && player.IsGrounded() && !input.isJumpLocked)
        {
            player.TransitionToState(player.jumpState);
        }
    }

    public override void FixedUpdateState(PlayerStateMachine player) { }

    public override void ExitState(PlayerStateMachine player)
    {
        Debug.Log($"Exit {player.currentStateType} State");
    }
}
