using UnityEngine;

public class PuntoDesembarque : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private NaveAutomatica nave;
    [SerializeField] private TransicionEscena transicion;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activado || nave == null || transicion == null)
            return;

        if (other.transform != nave.transform)
            return;

        activado = true;

        nave.DetenerNave();
        transicion.IniciarTransicion();
    }
}