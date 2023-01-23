using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoNoGramBackend;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Square square;

    // Start is called before the first frame update
    void Start()
    {
        ImageProcessor.Process("dragonfly");
        GenerateNonogram();
    }

    void GenerateNonogram()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var squarePrefab = Instantiate(square, new Vector3(x, y), Quaternion.identity);
                squarePrefab.name = $"Square {x}:{y}";
            }
        }
    }

}
