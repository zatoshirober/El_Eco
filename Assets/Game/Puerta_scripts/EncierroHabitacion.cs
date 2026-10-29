
using System.Collections;
using UnityEngine;

public class EncierroHabitacion : MonoBehaviour
{
    [Header("Barrera de la habitación")]
    [SerializeField] private Collider barreraCollider;

    [Header("Mensaje")]
    [SerializeField] private AudioSource audioMensaje;

    private bool activado = false;

    private void Start()
    {
        // La barrera empieza sin bloquear el paso.
        if (barreraCollider != null)
        {
            barreraCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activado)
            return;

        if (!other.CompareTag("Player"))
            return;

        activado = true;

        StartCoroutine(EsperarMensajeYActivarBarrera());
    }

    private IEnumerator EsperarMensajeYActivarBarrera()
    {
        // Si el mensaje todavía no está sonando, lo iniciamos.
        if (audioMensaje != null && !audioMensaje.isPlaying)
        {
            audioMensaje.Play();
        }

        // Esperar hasta que termine completamente el audio.
        if (audioMensaje != null)
        {
            yield return new WaitWhile(() => audioMensaje.isPlaying);
        }

        // Activar el bloqueo físico permanentemente.
        if (barreraCollider != null)
        {
            barreraCollider.enabled = true;
        }

        Debug.Log("Mensaje terminado. Barrera activada permanentemente.");
    }
}
