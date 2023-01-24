using NoNoGramBackend;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Square squarePrefab;
    [SerializeField] private Transform cam;
    private Dictionary<Vector2, Square> squares;

    void Start()
    {
        var imageProcessor = new ImageProcessor();
        var infos = imageProcessor.GetSquareInfos(8, "dragonfly");
        width = infos.First().Count;
        height = infos.Count;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        squares = new Dictionary<Vector2, Square>();        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(squarePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Square {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);


                squares[new Vector2(x, y)] = spawnedTile;
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    public Square GetTileAtPosition(Vector2 pos)
    {
        if (squares.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

}
