using NoNoGramBackend;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        width = squareInfos.squares.First().column.Count;
        height = squareInfos.squares.Count;
        GenerateImageGrid();
        GenerateRowCountersGrid();
        GenerateColumnCountersGrid();
    }

    void GenerateImageGrid()
    {
        squares = new Dictionary<Vector2, Square>();
        foreach (var squareInfoRow in squareInfos.squares) {

            foreach(var squareInfoCol in squareInfoRow.column)
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

    void GenerateRowCountersGrid()
    {

        for(var y = 0; y < squareInfos.rowCounters.lineCounters.Count; y++)
        {
            var startPointX = 0 - squareInfos.rowCounters.lineCounters[y].counters.Count;
            foreach (var counter in squareInfos.rowCounters.lineCounters[y].counters)
            {
                var nSquare = Instantiate(nSquarePrefab, new Vector3(startPointX, 0 - y), Quaternion.identity);
                nSquare.SetCount(counter);
                nSquare.name = $"NSquare: {startPointX} {0 - y}";
                startPointX++;
            }
        }
    }

    void GenerateColumnCountersGrid()
    {
        for (var x = 0; x < squareInfos.columnCounters.lineCounters.Count; x++)
        {
            var startPointY = squareInfos.columnCounters.lineCounters[x].counters.Count;
            foreach (var counter in squareInfos.columnCounters.lineCounters[x].counters)
            {
                var nSquare = Instantiate(nSquarePrefab, new Vector3(x, startPointY), Quaternion.identity);
                nSquare.SetCount(counter);
                nSquare.name = $"NSquare: {x} {startPointY}";
                startPointY--;
            }
        }
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
