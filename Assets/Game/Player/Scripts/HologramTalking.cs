using UnityEngine;

public class HologramTalking : MonoBehaviour
{
    private Material mat;

    [Header("Texturas de expresión (Hablando)")]
    public Texture2D[] talkTextures; // Arrastra aquí tus imágenes de caras
    public float frameRate = 0.15f;  // Velocidad de cambio entre cada cara (menor = más rápido)

    private float timer;
    private int currentIndex = 0;

    void Start()
    {
        // Obtenemos el material del Renderer del objeto
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            mat = rend.material;
        }
    }

    void Update()
    {
        // Verificamos que el material exista y tengamos texturas asignadas
        if (mat == null || talkTextures == null || talkTextures.Length == 0) return;

        // Temporizador para alternar las texturas
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % talkTextures.Length;

            // Asignamos la nueva textura a la propiedad principal del material
            mat.mainTexture = talkTextures[currentIndex];
        }
    }
}