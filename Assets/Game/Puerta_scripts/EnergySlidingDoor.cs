using UnityEngine;



public class EnergySlidingDoor : SlidingDoorBase
{
    [Header("Sistema de energía")]
    [SerializeField] private InteractionSystem interaction;
    [SerializeField] private Transform cellSlot;

    private bool desbloqueada;

    protected override bool PuedeAbrirse()
    {
        if (desbloqueada)
            return true;

        if (interaction == null || cellSlot == null)
            return false;

        if (!interaction.HasPowerCell())
            return false;

        Item cell = interaction.TakePowerCell();

        if (cell == null)
            return false;

        // Instalar la celda en su soporte.
        cell.transform.SetParent(cellSlot);
        cell.transform.localPosition = Vector3.zero;
        cell.transform.localRotation = Quaternion.identity;

        foreach (Collider col in
                 cell.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        Rigidbody body = cell.GetComponent<Rigidbody>();

        if (body != null)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.isKinematic = true;
        }

        desbloqueada = true;

        Debug.Log("Celda instalada. Puerta desbloqueada.");

        return true;
    }
}