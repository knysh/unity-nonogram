using NoNoGramBackend;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Square squarePrefab;
    [SerializeField] private NSquare nSquarePrefab;
    [SerializeField] private Transform cam;

    private int width;
    private int height;
    private SquareInfos squareInfos;
    private Dictionary<Vector2, Square> squares;

    void Start()
    {
        var req = GetRequest("https://localhost:7010/random_game?squareSize=3");
        while (req.MoveNext());

        width = squareInfos.squares.First().row.Count;
        height = squareInfos.squares.Count;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        var nSquare = Instantiate(nSquarePrefab, new Vector3(-1, -1), Quaternion.identity);
        nSquare.SetCount(111);
        squares = new Dictionary<Vector2, Square>();
        foreach (var squareInfoRow in squareInfos.squares) {

            foreach(var squareInfoCol in squareInfoRow.row)
            {
                var spawnedTile = Instantiate(squarePrefab, new Vector3(squareInfoCol.x, squareInfoCol.y), Quaternion.identity);
                spawnedTile.name = $"Square {squareInfoCol.x} {squareInfoCol.y}";
                Color currentColor;
                switch (squareInfoCol.color)
                {
                    case "1":
                        currentColor = Color.white;
                        break;
                    case "0":
                        currentColor = Color.black;
                        break;
                    default: 
                        currentColor = Color.yellow;
                        break;
                }

                spawnedTile.GetComponent<Renderer>().material.color = currentColor;
                squares[new Vector2(squareInfoCol.x, squareInfoCol.y)] = spawnedTile;
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    public Square GetTileAtPosition(Vector2 pos)
    {
        if (squares.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            while (!webRequest.isDone)
                yield return true;

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }

            var response = webRequest.downloadHandler.text;
            squareInfos = JsonUtility.FromJson<SquareInfos>(response);
        }
    }
}
