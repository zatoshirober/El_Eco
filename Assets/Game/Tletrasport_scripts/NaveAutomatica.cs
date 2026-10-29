using UnityEngine;

public class NaveAutomatica : MonoBehaviour
{
    [Header("Movimiento de la nave")]
    [SerializeField] private Transform destino;
    [SerializeField] private float velocidad = 5f;

    [Header("Camara")]
    [SerializeField] private Transform camara;
    [SerializeField] private Vector3 desplazamientoCamara = new Vector3(0f, 4f, -8f);
    [SerializeField] private bool seguirCamara = true;

    private bool enMovimiento = true;

    private void Update()
    {
        if (!enMovimiento || destino == null)
            return;

        Vector3 direccion = (destino.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino.position,
            velocidad * Time.deltaTime
        );

        if (direccion.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direccion);
        }
    }

    private void LateUpdate()
    {
        if (!seguirCamara || camara == null)
            return;

        camara.position = transform.position + desplazamientoCamara;
        camara.LookAt(transform.position);
    }

    public void DetenerNave()
    {
        enMovimiento = false;
    }
}