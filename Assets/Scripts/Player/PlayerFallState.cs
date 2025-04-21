using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Enter Fall State");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.IsGrounded() && player.rb.linearVelocity.y <= 0.1f)
        {
            var input = player.input;

            input.isJumpLocked = false;

            if (input.MoveInput.magnitude < 0.1f)
                player.TransitionToState(player.idleState);
            else if (input.RunHeld)
                player.TransitionToState(player.runState);
            else
                player.TransitionToState(player.walkState);
        }
    }

    public override void FixedUpdateState(PlayerStateMachine player) 
    {
        ApplyAirMovement(player, player.input.airControlSpeed);
    }

    private void ApplyAirMovement(PlayerStateMachine player, float speed)
    {
        var input = player.input;

        Vector3 dir = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
        if (dir.magnitude > 0.1f)
        {
            Vector3 move = player.transform.TransformDirection(dir.normalized) * speed * Time.fixedDeltaTime;
            player.rb.MovePosition(player.rb.position + move);
        }
    }

    public override void ExitState(PlayerStateMachine player)
    {
        Debug.Log($"Exit {player.currentStateType} State");
    }
}
