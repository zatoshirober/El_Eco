
using System.Collections;
using UnityEngine;

public class PanelVigilancia : BaseInteractable
{
    [Header("Referencias")]
    [SerializeField] private InteractionSystem interaction;
    [SerializeField] private Transform cellSlot;

    [Header("Holograma")]
    [SerializeField] private GameObject hologram;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mensaje;

    [Header("Configuracion del audio")]
    [SerializeField] private float duracionMensaje = 60f;

    private bool activated;
    private Coroutine audioCoroutine;

    private void Start()
    {
        if (hologram != null)
            hologram.SetActive(false);
    }

    public override void Interactable()
    {
        if (activated)
            return;

        if (interaction == null || cellSlot == null)
        {
            Debug.LogError(
                "Asigna Interaction y Cell Slot en el Inspector."
            );
            return;
        }

        if (!interaction.HasPowerCell())
        {
            Debug.Log("Necesitas una celda de energía.");
            return;
        }

        Item cell = interaction.TakePowerCell();

        if (cell == null)
            return;

        // Instalar la celda en el panel.
        cell.transform.SetParent(cellSlot);
        cell.transform.localPosition = Vector3.zero;
        cell.transform.localRotation = Quaternion.identity;

        Collider cellCollider = cell.GetComponent<Collider>();

        if (cellCollider != null)
            cellCollider.enabled = false;

        Rigidbody cellBody = cell.GetComponent<Rigidbody>();

        if (cellBody != null)
        {
            cellBody.linearVelocity = Vector3.zero;
            cellBody.angularVelocity = Vector3.zero;
            cellBody.isKinematic = true;
        }

        // Activar el panel.
        activated = true;

        if (hologram != null)
            hologram.SetActive(true);

        // Reproducir el mensaje durante un máximo de 60 segundos.
        if (audioSource != null && mensaje != null)
        {
            audioCoroutine = StartCoroutine(
                ReproducirMensaje()
            );
        }
        else
        {
            Debug.LogWarning(
                "Falta asignar el AudioSource o el mensaje."
            );
        }

        Debug.Log("Panel activado. Reproduciendo mensaje.");
    }

    private IEnumerator ReproducirMensaje()
    {
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = mensaje;
        audioSource.Play();

        yield return new WaitForSeconds(duracionMensaje);

        if (audioSource != null)
            audioSource.Stop();

        audioCoroutine = null;


        Debug.Log("El mensaje ha terminado.");
        hologram.SetActive(false);
    }

    private void OnDisable()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
            audioCoroutine = null;
        }

        if (audioSource != null)
            audioSource.Stop();
    }
}
