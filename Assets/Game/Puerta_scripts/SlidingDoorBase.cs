using System.Collections.Generic;
using UnityEngine;

public abstract class SlidingDoorBase : MonoBehaviour
{
    [Header("Movimiento de la puerta")]
    [SerializeField] private SlidingDoorController doorController;

    [Header("Sonidos")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoApertura;
    [SerializeField] private AudioClip sonidoCierre;

    [Header("Gas desinfectante")]
    [SerializeField] private ParticleSystem gasDesinfectante;

    private readonly HashSet<Collider> jugadoresDentro = new();

    private bool estaAbierta;

    protected bool EstaAbierta => estaAbierta;

    protected virtual void Awake()
    {
        if (doorController == null)
        {
            Debug.LogError(
                "SlidingDoorBase: asigna SlidingDoorController en el Inspector.",
                this
            );
        }

        if (gasDesinfectante != null)
        {
            gasDesinfectante.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
        }
    }

    protected virtual void Update()
    {
        if (doorController == null)
            return;

        bool debeAbrirse =
            jugadoresDentro.Count > 0 && PuedeAbrirse();

        if (debeAbrirse != estaAbierta)
            CambiarEstado(debeAbrirse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadoresDentro.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadoresDentro.Remove(other);
    }

    protected abstract bool PuedeAbrirse();

    private void CambiarEstado(bool abrir)
    {
        estaAbierta = abrir;

        doorController.EstablecerAbierta(abrir);

        if (abrir)
        {
            if (audioSource != null && sonidoApertura != null)
                audioSource.PlayOneShot(sonidoApertura);

            if (gasDesinfectante != null)
                gasDesinfectante.Play();
        }
        else
        {
            if (audioSource != null && sonidoCierre != null)
                audioSource.PlayOneShot(sonidoCierre);
        }
    }
}