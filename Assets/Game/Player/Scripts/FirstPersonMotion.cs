
using UnityEngine;

public class FirstPersonMotion : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerInput inputs;
    [SerializeField] private Rigidbody playerBody;

    [Header("Respiracion en reposo")]
    [SerializeField] private float breathingHeight = 0.006f;
    [SerializeField] private float breathingSpeed = 0.7f;

    [Header("Movimiento al caminar")]
    [SerializeField] private float walkHeight = 0.012f;
    [SerializeField] private float walkSide = 0.004f;
    [SerializeField] private float walkSpeed = 7f;

    [Header("Suavidad")]
    [SerializeField] private float transitionSpeed = 3f;

    private Vector3 initialLocalPosition;
    private float walkTimer;
    private float movementAmount;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        if (inputs == null || playerBody == null)
            return;

        Vector2 direction = inputs.Direction;

        Vector3 horizontalVelocity = new Vector3(
            playerBody.linearVelocity.x,
            0f,
            playerBody.linearVelocity.z
        );

        bool moving =
            direction.sqrMagnitude > 0.01f &&
            horizontalVelocity.magnitude > 0.1f;

        float targetMovement = moving ? 1f : 0f;

        movementAmount = Mathf.MoveTowards(
            movementAmount,
            targetMovement,
            Time.deltaTime * transitionSpeed
        );

        float verticalOffset;
        float horizontalOffset = 0f;

        if (movementAmount < 0.99f)
        {
            // Respiración lenta y sutil.
            float breathing = Mathf.Sin(
                Time.time * breathingSpeed * Mathf.PI * 2f
            );

            verticalOffset = breathing * breathingHeight;
        }
        else
        {
            verticalOffset = 0f;
        }

        if (moving || movementAmount > 0f)
        {
            walkTimer += Time.deltaTime * walkSpeed;

            float verticalWalk =
                Mathf.Cos(walkTimer * 2f) * walkHeight;

            float sideWalk =
                Mathf.Sin(walkTimer) * walkSide;

            verticalOffset = Mathf.Lerp(
                verticalOffset,
                verticalWalk,
                movementAmount
            );

            horizontalOffset = sideWalk * movementAmount;
        }

        Vector3 targetPosition = initialLocalPosition +
            new Vector3(
                horizontalOffset,
                verticalOffset,
                0f
            );

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * 5f
        );
    }
}
