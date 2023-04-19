using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Board : MonoBehaviour
{
    private int width;
    private int height;

    public List<Node> Nodes { get; private set; } = new List<Node>();
    public List<Block> Blocks { get; private set; } = new List<Block>();

    public void Init(int gridWidth, int gridHeight, Node gridNode, SpriteRenderer boardPrefab,Transform parent)
    {
        width = gridWidth;
        height = gridHeight;
        
        var centerPos = new Vector3((width/2)-0.5f, (height/2)-0.5f,0f);
        
        var board = Instantiate(boardPrefab, centerPos, quaternion.identity, parent.transform);
        board.size = new Vector2(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var node = Instantiate(gridNode, new Vector3(x, y, 0f), quaternion.identity, parent);
                Nodes.Add(node);
            }
        }
    }
    
}