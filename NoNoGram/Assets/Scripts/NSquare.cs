using TMPro;
using UnityEngine;

public class NSquare : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    public void SetCount(int count)
    {
        textMeshPro.text = count.ToString();
    }
}
