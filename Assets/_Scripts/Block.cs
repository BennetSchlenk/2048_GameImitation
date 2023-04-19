using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public uint Value;

    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private TextMeshPro text;

    public void Init(BlockType type)
    {
        Value = type.Value;
        renderer.color = type.color;
        text.text = type.Value.ToString();
    }
}

[Serializable]
public struct BlockType
{
    public uint Value;
    public Color color;
}