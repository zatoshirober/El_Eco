using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput inputs;
    [SerializeField] PlayerMovement movement;
    [SerializeField] InteractionSystem interaction;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] GameObject lightObj;
    [SerializeField] Transform positionOrigenRaycast;
    [SerializeField] float maxDistance;
    RaycastHit rayo;


    private void OnEnable()
    {
        inputs._Light.started += ActivateLight;
        inputs.interar.started += InteractuarObject;

    }
    private void OnDisable()
    {
        inputs._Light.started -= ActivateLight;
        inputs.interar.started -= InteractuarObject;

    }
    public void ActivateLight(CallbackContext context)
    {
        if (lightObj.activeSelf == true)
        {
            lightObj.SetActive(false);
        }
        else
        {
            lightObj.SetActive(true);
        }
    }
    public void InteractuarObject(CallbackContext context)
    {
        interaction.TryInteract();
    }

    private void Update()
    {
        movement.Move(inputs.Direction);

        mouseLook.Look(inputs.LookDirection);

        if (inputs.JumpPressed)
        {

            movement.Jump();
        }
    }

    //public void FixedUpdate()
    //{
    //    if (Physics.Raycast(positionOrigenRaycast.position, transform.forward, out RaycastHit hit, maxDistance))
    //    {
    //        if (hit.collider.gameObject.TryGetComponent(out ))
    //        {

    //        }
    //    }

    //}
}