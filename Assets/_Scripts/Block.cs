using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    
    public uint Value;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private TextMeshPro text;
    
    public Vector2 Position => transform.position;
    public Node Node;

    public void Init(BlockType type)
    {
        Value = type.Value;
        renderer.color = type.Color;
        text.text = type.Value.ToString();
    }

    public void SetBlock(Node newNode)
    {
        if (Node != null) Node.occupyingBlock = null;
        newNode.occupyingBlock = this;
        Node = newNode;
    }
}

[Serializable]
public struct BlockType
{
    public uint Value;
    public Color Color;
}
