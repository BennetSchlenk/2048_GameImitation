using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int width;
    private int height;

    private List<Node> Nodes { get; } = new List<Node>();
    private List<Block> Blocks = new List<Block>();

    public void Init(int gridWidth, int gridHeight, Node gridNode, Transform parent)
    {
        width = gridWidth;
        height = gridHeight;

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