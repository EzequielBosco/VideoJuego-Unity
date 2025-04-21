using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Enter Walk state");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        var input = player.input;

        if (input.MoveInput.magnitude < 0.1f)
        {
            player.TransitionToState(player.idleState);
        }
        else if (input.RunHeld)
        {
            player.TransitionToState(player.runState);
        }
        else if (input.StealthHeld)
        {
            player.TransitionToState(player.stealthState);
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

    public override void FixedUpdateState(PlayerStateMachine player)
    {
        Move(player, player.input.walkSpeed);
    }

    private void Move(PlayerStateMachine player, float speed)
    {
        Vector3 dir = new Vector3(player.input.MoveInput.x, 0, player.input.MoveInput.y);
        Vector3 move = player.transform.TransformDirection(dir.normalized) * speed * Time.fixedDeltaTime;
        player.rb.MovePosition(player.rb.position + move);
    }

    public override void ExitState(PlayerStateMachine player)
    {
        Debug.Log($"Exit {player.currentStateType} State");
    }
}
