using System;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    public uint Value;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private TextMeshPro text;
    
    public Vector2 Position => transform.position;
    public Node Node;
    public Block BlockToMergeWith;
    public bool IsMerging;

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

    public void MergeThisBlock(Block blockToMergeWith)
    {
        BlockToMergeWith = blockToMergeWith;
        Node.occupyingBlock = null;
        blockToMergeWith.IsMerging = true;
    }

    public bool CanMerge(uint blockValue)
    {
        return !IsMerging && BlockToMergeWith == null && blockValue == Value;
    }
}

[Serializable]
public struct BlockType
{
    public uint Value;
    public Color Color;
}
