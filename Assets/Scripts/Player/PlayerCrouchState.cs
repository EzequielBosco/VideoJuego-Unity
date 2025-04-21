using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player) 
    {
        Debug.Log("Enter Crouch State");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        var input = player.input;

        if (!input.CrouchHeld)
        {
            player.TransitionToState(player.idleState);
        }

        float currentY = player.transform.localScale.y;
        float targetY = input.crouchHeight;
        float newY = Mathf.Lerp(currentY, targetY, Time.deltaTime * input.crouchTransitionSpeed);
        player.transform.localScale = new Vector3(1f, newY, 1f);
    }

    public override void FixedUpdateState(PlayerStateMachine player)
    {
        Vector3 dir = new Vector3(player.input.MoveInput.x, 0f, player.input.MoveInput.y);
        Vector3 move = player.transform.TransformDirection(dir.normalized) * player.input.crouchSpeed * Time.fixedDeltaTime;
        player.rb.MovePosition(player.rb.position + move);
    }

    public override void ExitState(PlayerStateMachine player) 
    { 
        Debug.Log($"Exit {player.currentStateType} State");
        player.StartCoroutine(RestoreHeight(player));
    }

    private System.Collections.IEnumerator RestoreHeight(PlayerStateMachine player)
    {
        var input = player.input;

        while (Mathf.Abs(player.transform.localScale.y - input.standHeight) > 0.01f)
        {
            float newY = Mathf.Lerp(player.transform.localScale.y, input.standHeight, Time.deltaTime * input.crouchTransitionSpeed);
            player.transform.localScale = new Vector3(1f, newY, 1f);
            yield return null;
        }

        player.transform.localScale = new Vector3(1f, input.standHeight, 1f);
    }
}

