using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
                int index = Random.Range(0, spriteList.Count - 1);
                Vector2 pos = new Vector2(i, j); // Vị trí ô
                G1_Block block = Instantiate(blockPrefab, pos, Quaternion.identity); //Spawn ô
                grid[i, j] = block; //gán ô vừa sinh ra vào mảng với chỉ số i,j
                block.transform.SetParent(transform, false); //Set tất cả các block vừa sinh ra làm con của GameObject Board
                block.name = "(" + i + "_" + j + ")"; // Set tên của các block theo vị trí i, j

                //Gán ảnh 
                block.UpdateSprite(spriteList[index]);
                block.UpdateState(BlockState.full);

                //Gán chỉ số row, col cho block vừa sinh ra
                block.row = i;
                block.col = j;
                
            }
        }
    }

    // thuật toán DFS tìm kiếm theo chiều sâu
    public void DFS(int i, int j)
    {
        visited[i, j] = true; // Đánh dấu ô vừa click đã được thăm
        
        for (int k = 0; k < 4; k++)
        {
            int i1 = i + dx[k];
            int j1 = j + dy[k];
            if (i1 >= 0 && i1 < row && j1 >= 0 && j1 < column && grid[i1, j1].sprite == grid[i, j].sprite && !visited[i1, j1])
            {
                grid[i, j].UpdateState(BlockState.empty);
                grid[i1, j1].UpdateState(BlockState.empty);
                DFS(i1, j1);
            }
        }
    }
    //Reset các ô đã loang trước đó
    public void ResetVisited()
    {
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < column; j++)
            {
                visited[i, j] = false;
            }
        }
    }
    //Tìm ô không null theo hàng, và gán sprite ô đó cho ô null tìm được 
    public IEnumerator TranslateRow()
    {
        for(int j = 0; j < column; j++)
        {
            for(int i = 0; i < row; i++)
            {
                if (grid[i, j].state == BlockState.empty)
                {
                    yield return new WaitForSeconds(0.01f);
                    SwapSpriteRow(i, j);
                }
            }
        }
    }
    public IEnumerator TranslateColumn()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (grid[i, j].state == BlockState.empty)
                {
                    yield return new WaitForSeconds(0.1f);
                    SwapSpriteColumn();
                }
            }
        }
    }
    // Dịch các ô theo hàng
    public void SwapSpriteRow(int i, int j)
    {
        for (int k = j; k < column; k++) 
        {
            if (grid[i, k].state != BlockState.empty) // duyệt theo cột, nếu có ô null thì
            {
                Sprite temp = grid[i, j].sprite; 
                grid[i, j].sprite = grid[i, k].sprite;
                grid[i, j].UpdateState(BlockState.full);
                grid[i, k].sprite = temp;
                grid[i, k].UpdateState(BlockState.empty);
                return;
            }
        }
    }
    //Dịch các ô theo cột
    public void SwapSpriteColumn()
    {
        for(int k = 0; k < row; k++) //duyệt hàng 0
        {
            if (grid[k, 0].state == BlockState.empty) //Nếu có ô null
            {
                for(int j = 0; j < column; j++)
                {
                    Sprite temp = grid[k + 1, j].sprite; 
                    grid[k + 1, j].sprite= grid[k, j].sprite;
                    grid[k + 1, j].UpdateState(BlockState.full);
                    grid[k, j].sprite = temp;
                    grid[k, j].UpdateState(BlockState.empty);
                }    
                return;
                
            }
        }
    }
    public IEnumerator ShifeTilesDown(int x, int y)
    {
        List<G1_Block> renders = new List<G1_Block>();
        int nullCount = 0;

        for(int j = y; j < column; j++)
        {
            G1_Block block = grid[x, j];
            if(block.state == BlockState.empty)
            {
                nullCount++;
            }
            renders.Add(block);
        }

        for(int i = 0; i < nullCount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            for(int k = 0; k < renders.Count - 1; k++)
            {
                renders[k].sprite = renders[k + 1].sprite;
                renders[k].UpdateState(BlockState.full);

                renders[k + 1].sprite = null;
                renders[k + 1].UpdateState(BlockState.empty);
            }
        }
    }
}

