using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    GenerateGameBoard,
    SpawnBlock,
    GetMoveInput,
    MovingBlocks,
    GameWon,
    GameLost
}

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

    private GameState gameState;
    private int roundsPlayed;
    private uint scoreValue;
    
    [SerializeField]
    private TextMeshProUGUI roundsText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void Start()
    {
        int gridSize = PlayerPrefs.GetInt("GridSize");
        boardWidth = gridSize;
        boardHeight = gridSize;
        roundsPlayed = 0;
        scoreValue = 0;
        ChangeGameState(GameState.GenerateGameBoard);
    }

    public void GenerateGameBoard()
    {
        //Create Parent to spawn Objects under
        var parent = new GameObject("GameBoard");

        //Set Camera to capture full board
        var cam = Helper.Camera;
        cam.transform.position = new Vector3((boardWidth / 2f) - 0.5f, (boardHeight / 2f) - 0.5f, -10f);
        cam.orthographicSize = (boardHeight / 2f) + 0.5f;

        this.board = new Board();
        this.board.Init(boardWidth, boardHeight, nodePrefab, boardPrefab, parent.transform);

        ChangeGameState(GameState.SpawnBlock);
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
        var openNodes = board.Nodes.Where(n => n.occupyingBlock == null).OrderBy(o => UnityEngine.Random.value)
            .ToList();
        if (openNodes.Count == 1)
        {
            ChangeGameState(GameState.GameLost);
            SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
            Debug.Log("Game Lost");
        }
        else
        {
            Node openNode = openNodes.Take(1).FirstOrDefault();
            var block = Instantiate(blockPrefab, openNode.Position, quaternion.identity);
            block.Init(GetBlockTypeByValue(2));
            block.SetBlock(openNodes[0]);
            board.Blocks.Add(block);
        }

        ChangeGameState(GameState.GetMoveInput);
    }

    private void SpawnMergedBlock(Node node, uint value)
    {
        var block = Instantiate(blockPrefab, node.Position, quaternion.identity);
        block.Init(GetBlockTypeByValue(value));
        block.SetBlock(node);
        board.Blocks.Add(block);
    }

    private void Update()
    {
        roundsText.text = "Round: " + roundsPlayed;
        scoreText.text = "Score: " + scoreValue;

        if (gameState != GameState.GetMoveInput) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveBlocks(Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) MoveBlocks(Vector2.up);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) MoveBlocks(Vector2.right);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) MoveBlocks(Vector2.down);
    }

    private void MoveBlocks(Vector2 dir)
    {
        ChangeGameState(GameState.MovingBlocks);
        var orderedBlocks = board.Blocks.OrderBy(x => x.Position.x).ThenBy(y => y.Position.y).ToList();
        if (dir == Vector2.right || dir == Vector2.up) orderedBlocks.Reverse();

        foreach (var block in orderedBlocks)
        {
            var next = block.Node;
            do
            {
                block.SetBlock(next);
                var possibleNode = GetNodeAtPosition(next.Position + dir);
                if (possibleNode != null)
                {
                    if (possibleNode.occupyingBlock != null && possibleNode.occupyingBlock.CanMerge(block.Value))
                    {
                        block.MergeThisBlock(possibleNode.occupyingBlock);
                    }
                    else if (possibleNode.occupyingBlock == null) next = possibleNode;
                }
            } while (next != block.Node);
        }

        foreach (var block in orderedBlocks)
        {
            block.transform.position = block.BlockToMergeWith != null
                ? block.BlockToMergeWith.Node.Position
                : block.Node.Position;
        }

        foreach (var block in orderedBlocks.Where(b => b.BlockToMergeWith != null))
        {
            MergeBlocks(block.BlockToMergeWith, block);
        }

        ChangeGameState(GameState.SpawnBlock);
    }

    private void MergeBlocks(Block mergeToBlock, Block mergingBlock)
    {
        var mergeValue = mergeToBlock.Value * 2;

        scoreValue += mergeValue;
        SpawnMergedBlock(mergeToBlock.Node, mergeValue);

        DestroyBlock(mergingBlock);
        DestroyBlock(mergeToBlock);

        if (mergeValue == 2048)
        {
            Invoke(nameof(GameWon),5f);
        }
    }

    private void DestroyBlock(Block block)
    {
        board.Blocks.Remove(block);
        Destroy(block.gameObject);
    }

    private Node GetNodeAtPosition(Vector2 pos)
    {
        return board.Nodes.FirstOrDefault(n => n.Position == pos);
    }
    
    private void GameWon()
    {
        ChangeGameState(GameState.GameWon);
        SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.GenerateGameBoard:
                GenerateGameBoard();
                break;
            case GameState.SpawnBlock:
                SpawnBlocks(roundsPlayed == 0 ? 2 : 1);
                break;
            case GameState.GetMoveInput:
                break;
            case GameState.MovingBlocks:
                roundsPlayed++;
                break;
            case GameState.GameWon:
                break;
            case GameState.GameLost:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}