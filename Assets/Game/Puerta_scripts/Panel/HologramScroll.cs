using UnityEngine;

public class HologramScroll : MonoBehaviour
{
    private Material mat;
    public float scrollSpeed = 0.5f; 

    void Start()
    {
        // Obtiene el material del Renderer
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            mat = rend.material;
        }
    }

    void Update()
    {
        if (mat != null)
        {
            
            float offset = Time.time * scrollSpeed;
            mat.mainTextureOffset = new Vector2(0, -offset);
        }
    }
}