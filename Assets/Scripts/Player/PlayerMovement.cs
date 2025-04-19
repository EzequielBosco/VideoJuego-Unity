using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movementInput;
    public float moveSpeed = 5f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(horizontal, 0f, vertical);
        movementInput = transform.right * input.x + transform.forward * input.z;
        movementInput = Vector3.ClampMagnitude(movementInput, 1f);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
}