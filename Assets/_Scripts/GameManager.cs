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
        grid = new Grid();
        grid.Init(boardWidth, boardHeight, nodePrefab, transform);
    }
}