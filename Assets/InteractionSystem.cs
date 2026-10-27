using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] List<Item> inventory = new();
    [SerializeField] Transform localPositionObject;
    public Item currentObj;
    

    public bool TryDragObject()
    {
        if (currentObj == null)
        {
            return false;
        }
        else
        {
            inventory.Add(currentObj);
            currentObj.transform.SetParent(localPositionObject);

            currentObj.transform.localPosition = Vector3.zero;
            return true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            currentObj = item;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            currentObj = null;
        }
    }
}
