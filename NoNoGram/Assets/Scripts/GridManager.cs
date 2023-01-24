using NoNoGramBackend;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Square squarePrefab;
    [SerializeField] private Transform cam;

    private int width;
    private int height;
    private List<List<SquareInfo>> squareInfos;
    private Dictionary<Vector2, Square> squares;

    void Start()
    {
        var imageProcessor = new ImageProcessor();
        squareInfos = imageProcessor.GetSquareInfos(8, "dragonfly");
        width = squareInfos.First().Count;
        height = squareInfos.Count;
        GenerateGrid();
    }

    void GenerateGrid()
    {

        squares = new Dictionary<Vector2, Square>();
        foreach (var squareInfoRow in squareInfos) {

            foreach(var squareInfoCol in squareInfoRow)
            {
                var spawnedTile = Instantiate(squarePrefab, new Vector3(squareInfoCol.X, squareInfoCol.Y), Quaternion.identity);
                spawnedTile.name = $"Square {squareInfoCol.X} {squareInfoCol.Y}";
                UnityEngine.Color currentColor;
                switch (squareInfoCol.Color)
                {
                    case NoNoGramBackend.Color.WHITE:
                        currentColor = UnityEngine.Color.white;
                        break;
                    case NoNoGramBackend.Color.BLACK:
                        currentColor = UnityEngine.Color.black;
                        break;
                    default: 
                        currentColor = UnityEngine.Color.yellow;
                        break;
                }

                //spawnedTile.GetComponent<Renderer>().material.color = currentColor;
                //var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(currentColor);


                squares[new Vector2(squareInfoCol.X, squareInfoCol.Y)] = spawnedTile;
            }
        }


        //squares = new Dictionary<Vector2, Square>();        
        //for (int x = 0; x < width; x++)
        //{
        //    for (int y = 0; y < height; y++)
        //    {
        //        var spawnedTile = Instantiate(squarePrefab, new Vector3(x, y), Quaternion.identity);
        //        spawnedTile.name = $"Square {x} {y}";
        //        spawnedTile.GetComponent<Renderer>().material.color = UnityEngine.Color.black;

        //        var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
        //        spawnedTile.Init(isOffset);


        //        squares[new Vector2(x, y)] = spawnedTile;
        //    }
        //}

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    public Square GetTileAtPosition(Vector2 pos)
    {
        if (squares.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

}
