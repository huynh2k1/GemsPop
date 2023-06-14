using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G1_Block : MonoBehaviour
{
    private static G1_Block select;
    public SpriteRenderer spriteRenderer;
    public ColorType type;
    public int row;
    public int col;
    public List<G1_Block> blocks;
    public BlockState state;
    public Sprite sprite;
    private void Start()
    {
        state = BlockState.full;
    }
    private void OnMouseDown()
    {
        select = this;
        G1_Board.instance.DFS(select.row, select.col);
        StartCoroutine(G1_Board.instance.TranslateRow());
        StartCoroutine(G1_Board.instance.TranslateColumn());
    }
    public void UpdateState(BlockState temp)
    {
        if (temp == state)
            return;
        state = temp;
        if(state == BlockState.full)
        {
            spriteRenderer.sprite = this.sprite;
        }
        else if(state == BlockState.empty)
        {
            spriteRenderer.sprite = null;
        }
    }
    public void UpdateSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }
    
}
public enum ColorType
{
    red,
    green,
    blue,
    yellow,
    violet
}

public enum BlockState
{
    empty,
    full
}
