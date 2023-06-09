using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class G1_Board : MonoBehaviour
{
    //public List<G1_Block> blockList;
    public G1_Block blockPrefab;
    public static G1_Board instance;
    public int row;
    public int column;
    public G1_Block[,] grid;
    public List<Sprite> spriteList = new List<Sprite>();
    public List<int> idBlocks = new List<int>();

    public bool[,] visited = new bool[10, 10];
    public int[] dx = { -1, 0, 0, 1 };
    public int[] dy = { 0, -1, 1, 0 };

    private void Awake()
    {
        instance = this;
        InitBoard();
    }
    private void Start()
    {
    }

    public void InitBoard()
    {
        grid = new G1_Block[row, column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                //int index = Random.Range(0, blockList.Count); //Random block: red, green, blue, violet, yellow
                int index = Random.Range(0, spriteList.Count - 1);
                Vector2 pos = new Vector2(i, j); // Vị trí ô trên transform
                G1_Block block = Instantiate(blockPrefab, pos, Quaternion.identity); //Spawn ô
                grid[i, j] = block; //gán ô vừa sinh ra vào mảng với chỉ số i,j
                block.transform.SetParent(transform, false); //Set tất cả các block vừa sinh ra làm con của GameObject Board
                block.name = "(" + i + "_" + j + ")"; // Set tên của các block theo vị trí i, j
                SpriteRenderer renderer = block.GetComponent<SpriteRenderer>();
                renderer.sprite = spriteList[index];
                block.id = idBlocks[index];
                //Gán chỉ số row, col cho block vừa sinh ra
                block.row = i;
                block.col = j;
                
            }
        }
    }
    // Mảng này để lưu các ô trong quá trình loang
    public List<G1_Block> listBlock = new List<G1_Block>();
    // thuật toán DFS tìm kiếm theo chiều sâu
    public void DFS(int i, int j)
    {
        visited[i, j] = true; // Đánh dấu ô vừa click đã được thăm
        //Duyệt 2 mảng dx, dy để lấy ra cặp giá trị i,j tương ứng với ô chung cạnh liền kề(trái, dưới, trên, phải)
        for (int k = 0; k < 4; k++)
        {
            int i1 = i + dx[k]; 
            int j1 = j + dy[k]; 
            if (i1 >= 0 && i1 < row && j1 >= 0 && j1 < column && grid[i1, j1].id == grid[i, j].id && !visited[i1, j1])
            { 
                listBlock.Add(grid[i1, j1]);
                DFS(i1, j1);
            }
        }
    }
    //
    public void AddBlockInList(int i, int j)
    {
        listBlock.Add(grid[i, j]);
    }
    public void GetIndexBlockInList()
    {
        for(int i = 0; i < listBlock.Count; i++)
        {
            Debug.Log(listBlock[i]);
        }
    }
}
