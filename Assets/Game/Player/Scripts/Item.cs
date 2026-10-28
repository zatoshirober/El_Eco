
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Tipo de objeto")]
    [SerializeField] private bool isPowerCell = true;

    public bool IsPowerCell => isPowerCell;
}
