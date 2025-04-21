using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        var input = player.input;

        if (!input.isJumpLocked)
        {
            Debug.Log("Enter Jump State");
            player.rb.AddForce(Vector3.up * input.jumpForce, ForceMode.Impulse);
            input.isJumpLocked = true;
        }
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.rb.linearVelocity.y < 0f)
        {
            player.TransitionToState(player.fallState);
        }
    }

    public override void FixedUpdateState(PlayerStateMachine player) 
    {
        ApplyAirMovement(player, player.input.airControlSpeed);
    }

    private void ApplyAirMovement(PlayerStateMachine player, float airSpeed)
    {
        var input = player.input;

        Vector3 dir = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
        if (dir.magnitude > 0.1f)
        {
            Vector3 move = player.transform.TransformDirection(dir.normalized) * airSpeed * Time.fixedDeltaTime;
            player.rb.MovePosition(player.rb.position + move);
        }
    }

    public override void ExitState(PlayerStateMachine player)
    {
        Debug.Log($"Exit {player.currentStateType} State");
    }
}
