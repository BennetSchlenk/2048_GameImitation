using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Grid grid;

    [SerializeField]
    private int boardWidth;

    [SerializeField]
    private int boardHeight;

    [SerializeField]
    private Node nodePrefab;
    
    [SerializeField]
    private SpriteRenderer boardPrefab;

    public void Start()
    {
        var centerPos = new Vector3((boardWidth/2)-0.5f, (boardHeight/2)-0.5f,0f);

        var parent = new GameObject("GameBoard");
        
        var cam = Helper.Camera;
        cam.transform.position = new Vector3((boardWidth/2)-0.5f, (boardHeight/2)-0.5f,-10f);
        cam.orthographicSize = (boardHeight / 2)+0.5f;

        var board = Instantiate(boardPrefab, centerPos, quaternion.identity, parent.transform);
        board.size = new Vector2(boardWidth, boardHeight);
        
        grid = new Grid();
        grid.Init(boardWidth, boardHeight, nodePrefab, parent.transform);
    }
}