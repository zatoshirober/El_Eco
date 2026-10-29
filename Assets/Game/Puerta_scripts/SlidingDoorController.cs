using UnityEngine;
using System.Collections;

public class SlidingDoorController : MonoBehaviour
{
    [Header("Hojas de la puerta")]
    [SerializeField] private Transform hojaIzquierda;
    [SerializeField] private Transform hojaDerecha;

    [Header("Configuracion")]
    [SerializeField] private float distanciaApertura = 1.5f;
    [SerializeField] private float velocidad = 2f;

    private Vector3 posicionInicialIzquierda;
    private Vector3 posicionInicialDerecha;

    private Coroutine movimientoPuerta;
    private bool abierta;

    private void Awake()
    {
        if (hojaIzquierda == null || hojaDerecha == null)
        {
            Debug.LogError(
                "SlidingDoorController: asigna las dos hojas en el Inspector.",
                this
            );

            enabled = false;
            return;
        }

        posicionInicialIzquierda = hojaIzquierda.localPosition;
        posicionInicialDerecha = hojaDerecha.localPosition;
    }

    public void AbrirPuerta()
    {
        EstablecerAbierta(true);
    }

    public void CerrarPuerta()
    {
        EstablecerAbierta(false);
    }

    public void EstablecerAbierta(bool abrir)
    {
        if (abierta == abrir)
            return;

        abierta = abrir;

        Vector3 destinoIzquierda = abrir
            ? posicionInicialIzquierda +
              Vector3.left * distanciaApertura
            : posicionInicialIzquierda;

        Vector3 destinoDerecha = abrir
            ? posicionInicialDerecha +
              Vector3.right * distanciaApertura
            : posicionInicialDerecha;

        MoverPuerta(destinoIzquierda, destinoDerecha);
    }

    private void MoverPuerta(
        Vector3 destinoIzquierda,
        Vector3 destinoDerecha)
    {
        if (movimientoPuerta != null)
            StopCoroutine(movimientoPuerta);

        movimientoPuerta = StartCoroutine(
            AnimarPuertas(destinoIzquierda, destinoDerecha)
        );
    }

    private IEnumerator AnimarPuertas(
        Vector3 destinoIzquierda,
        Vector3 destinoDerecha)
    {
        while (
            Vector3.Distance(
                hojaIzquierda.localPosition,
                destinoIzquierda) > 0.01f ||
            Vector3.Distance(
                hojaDerecha.localPosition,
                destinoDerecha) > 0.01f
        )
        {
            hojaIzquierda.localPosition = Vector3.MoveTowards(
                hojaIzquierda.localPosition,
                destinoIzquierda,
                velocidad * Time.deltaTime
            );

            hojaDerecha.localPosition = Vector3.MoveTowards(
                hojaDerecha.localPosition,
                destinoDerecha,
                velocidad * Time.deltaTime
            );

            yield return null;
        }

        hojaIzquierda.localPosition = destinoIzquierda;
        hojaDerecha.localPosition = destinoDerecha;

        movimientoPuerta = null;
    }
}
