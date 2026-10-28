
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Inventario")]
    [SerializeField] private List<Item> inventory = new();
    [SerializeField] private Transform localPositionObject;

    public Item currentObj;

    private BaseInteractable currentInteractable;

    public bool TryInteract()
    {
        // Si estamos frente a un panel, intentamos interactuar con él.
        if (currentInteractable != null)
        {
            currentInteractable.Interactable();
            return true;
        }

        // Si no hay un panel cerca, intentamos recoger la celda.
        return TryDragObject();
    }

    public bool TryDragObject()
    {
        if (currentObj == null)
            return false;

        if (inventory.Contains(currentObj))
            return false;

        inventory.Add(currentObj);

        Rigidbody itemBody = currentObj.GetComponent<Rigidbody>();

        if (itemBody != null)
        {
            itemBody.linearVelocity = Vector3.zero;
            itemBody.angularVelocity = Vector3.zero;
            itemBody.isKinematic = true;
        }

        // Desactivar la colisión de la celda mientras se transporta.
        Collider itemCollider = currentObj.GetComponent<Collider>();

        if (itemCollider != null)
            itemCollider.enabled = false;

        currentObj.transform.SetParent(localPositionObject);
        currentObj.transform.localPosition = Vector3.zero;
        currentObj.transform.localRotation = Quaternion.identity;

        Debug.Log("Celda de energía recogida.");

        return true;
    }

    public bool HasPowerCell()
    {
        return inventory.Exists(
            item => item != null && item.IsPowerCell
        );
    }

    public Item TakePowerCell()
    {
        Item cell = inventory.Find(
            item => item != null && item.IsPowerCell
        );

        if (cell == null)
            return null;

        inventory.Remove(cell);

        if (currentObj == cell)
            currentObj = null;

        return cell;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
            currentObj = item;

        if (other.TryGetComponent(out BaseInteractable interactable))
            currentInteractable = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (currentObj == item)
                currentObj = null;
        }

        if (other.TryGetComponent(out BaseInteractable interactable))
        {
            if (currentInteractable == interactable)
                currentInteractable = null;
        }
    }
}
