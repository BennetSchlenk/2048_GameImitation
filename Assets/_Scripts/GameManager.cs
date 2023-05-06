using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int boardWidth;

    [SerializeField]
    private int boardHeight;

    [SerializeField]
    private Node nodePrefab;

    [SerializeField]
    private Block blockPrefab;

    [SerializeField]
    private SpriteRenderer boardPrefab;

    [SerializeField]
    private List<BlockType> blockTypes;

    private Board board;
    private BlockType GetBlockTypeByValue(uint value) => blockTypes.FirstOrDefault(v => v.Value == value);


    public void Start()
    {
        //Create Parent to spawn Objects under
        var parent = new GameObject("GameBoard");

        //Set Camera to capture full board
        var cam = Helper.Camera;
        cam.transform.position = new Vector3((boardWidth / 2f) - 0.5f, (boardHeight / 2f) - 0.5f, -10f);
        cam.orthographicSize = (boardHeight / 2f) + 0.5f;

        this.board = new Board();
        this.board.Init(boardWidth, boardHeight, nodePrefab, boardPrefab, parent.transform);

        SpawnBlocks(2);
    }

    public void SpawnBlocks(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        var oneFreeNode = board.Nodes.Where(n => n.occupyingBlock == null).OrderBy(o => UnityEngine.Random.value)
            .Take(1).ToList();
        if (oneFreeNode.FirstOrDefault() == null)
        {
            //Game is Lost
        }
        else
        {
            var block = Instantiate(blockPrefab, oneFreeNode[0].Position, quaternion.identity);
            block.Init(GetBlockTypeByValue(2));
            board.Blocks.Add(block);
        }

    }
}