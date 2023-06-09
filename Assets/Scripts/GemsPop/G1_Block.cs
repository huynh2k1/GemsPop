using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G1_Block : MonoBehaviour
{
    private static G1_Block select;
    public ColorType type;
    public int row;
    public int col;
    public List<G1_Block> blocks;
    public int id;
    private void OnMouseDown()
    {
        select = this;
        G1_Board.instance.AddBlockInList(select.row, select.col);//Add ô click vào listBlock
        G1_Board.instance.DFS(select.row, select.col); // Dùng thuật toán duyệt các ô chung cạnh 
        G1_Board.instance.GetIndexBlockInList(); // In danh sách
        
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
