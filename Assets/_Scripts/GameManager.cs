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

    public void Start()
    {
        var cam = Helper.Camera;
        
        cam.transform.position = new Vector3((boardWidth/2)-0.5f, (boardHeight/2)-0.5f,-10f);
        cam.orthographicSize = (boardHeight / 2)+0.5f;
        grid = new Grid();
        grid.Init(boardWidth, boardHeight, nodePrefab, transform);
    }
}