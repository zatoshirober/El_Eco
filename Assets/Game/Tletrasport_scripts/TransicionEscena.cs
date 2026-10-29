using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour
{
    [Header("Pantalla negra")]
    [SerializeField] private CanvasGroup pantallaNegra;
    [SerializeField] private float duracionFundido = 1f;
    [SerializeField] private float tiempoPantallaNegra = 3f;

    [Header("Escena de destino")]
    [SerializeField] private string nombreEscena = "Main";

    private bool transicionIniciada = false;

    private void Awake()
    {
        if (pantallaNegra != null)
        {
            pantallaNegra.alpha = 0f;
            pantallaNegra.blocksRaycasts = false;
            pantallaNegra.interactable = false;
        }
    }

    public void IniciarTransicion()
    {
        if (transicionIniciada)
            return;

        transicionIniciada = true;
        StartCoroutine(ProcesarTransicion());
    }

    private IEnumerator ProcesarTransicion()
    {
        if (pantallaNegra == null)
        {
            Debug.LogError("Falta asignar el CanvasGroup de la pantalla negra.");
            yield break;
        }

        pantallaNegra.blocksRaycasts = true;

        float tiempo = 0f;

        while (tiempo < duracionFundido)
        {
            tiempo += Time.deltaTime;

            pantallaNegra.alpha = Mathf.Clamp01(
                tiempo / duracionFundido
            );

            yield return null;
        }

        pantallaNegra.alpha = 1f;

        yield return new WaitForSeconds(tiempoPantallaNegra);

        SceneManager.LoadScene(nombreEscena);
    }
}