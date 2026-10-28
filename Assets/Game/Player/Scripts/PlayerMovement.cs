using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    public void Move(Vector2 direction)
    {
        Vector3 moveDirection =
            transform.forward * direction.y +
            transform.right * direction.x;

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        body.linearVelocity = new Vector3(
            moveDirection.x * speed,
            body.linearVelocity.y,
            moveDirection.z * speed
        );
    }

    public void Jump()
    {
        if (!IsGrounded())
            return;

        body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );
    }
}