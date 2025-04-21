using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerInput input;
    public PlayerStateMachine stateMachineRef;
    private PlayerBaseState _currentState;
    public PlayerStateType currentStateType { get; private set; }

    // Ground Check
    public Transform groundCheck;
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    // States
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerFallState fallState = new PlayerFallState();
    public PlayerRunState runState = new PlayerRunState();
    public PlayerCrouchState crouchState = new PlayerCrouchState();
    public PlayerStealthState stealthState = new PlayerStealthState();

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayer);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput(); // Wrapper inputs
        stateMachineRef = this;
    }

    void Start()
    {
        TransitionToState(idleState);
    }

    void Update()
    {
        input.Tick();
        _currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    public void TransitionToState(PlayerBaseState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState?.EnterState(this);

        currentStateType = GetStateEnumFromInstance(newState);
    }

    private PlayerStateType GetStateEnumFromInstance(PlayerBaseState state)
    {
        switch (state)
        {
            case PlayerIdleState:
                return PlayerStateType.Idle;
            case PlayerWalkState:
                return PlayerStateType.Walk;
            case PlayerRunState:
                return PlayerStateType.Run;
            case PlayerJumpState:
                return PlayerStateType.Jump;
            case PlayerFallState:
                return PlayerStateType.Fall;
            case PlayerCrouchState:
                return PlayerStateType.Crouch;
            case PlayerStealthState:
                return PlayerStateType.Stealth;
        }

        return PlayerStateType.Idle;
    }
}
