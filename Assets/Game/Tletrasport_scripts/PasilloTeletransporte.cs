using UnityEngine;
using System.Collections;

public class PasilloTeletransporte : MonoBehaviour
{
    [Header("Destino")]
    [SerializeField] private Transform puntoInicio;

    [Header("Jugador")]
    [SerializeField] private string etiquetaJugador = "Player";

    [Header("Opciones")]
    [SerializeField] private float esperaAntesDeTeletransportar = 0f;

    private bool teletransportando;

    private void OnTriggerEnter(Collider other)
    {
        if (teletransportando)
            return;

        if (!other.CompareTag(etiquetaJugador))
            return;

        StartCoroutine(Teletransportar(other.transform));
    }

    private IEnumerator Teletransportar(Transform jugador)
    {
        if (puntoInicio == null)
        {
            Debug.LogError(
                "PasilloTeletransporte: falta asignar Punto Inicio.",
                this
            );

            yield break;
        }

        teletransportando = true;

        if (esperaAntesDeTeletransportar > 0f)
            yield return new WaitForSeconds(
                esperaAntesDeTeletransportar
            );

        CharacterController controlador =
            jugador.GetComponent<CharacterController>();

        Rigidbody cuerpo =
            jugador.GetComponent<Rigidbody>();

        // Desactivar temporalmente el CharacterController
        // para evitar problemas al cambiar la posición.
        if (controlador != null)
            controlador.enabled = false;

        jugador.SetPositionAndRotation(
            puntoInicio.position,
            puntoInicio.rotation
        );

        if (cuerpo != null)
        {
            cuerpo.linearVelocity = Vector3.zero;
            cuerpo.angularVelocity = Vector3.zero;
        }

        if (controlador != null)
            controlador.enabled = true;

        yield return new WaitForSeconds(0.5f);

        teletransportando = false;
    }
}