using UnityEngine;

public class PlayerInput
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RunHeld { get; private set; }
    public bool CrouchHeld { get; private set; }
    public bool StealthHeld { get; private set; }

    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float stealthSpeed = 1.5f;
    public float jumpForce = 6f;
    public bool isJumpLocked = false;
    public float airControlSpeed = 3f;
    public float crouchSpeed = 2f;
    public float standHeight = 1f;
    public float crouchHeight = 0.5f;
    public float crouchTransitionSpeed = 10f;

    public void Tick()
    {
        MoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        JumpPressed = Input.GetButtonDown("Jump");
        RunHeld = Input.GetKey(KeyCode.LeftShift);
        CrouchHeld = Input.GetKey(KeyCode.LeftControl);
        StealthHeld = Input.GetKey(KeyCode.C);
    }
}

