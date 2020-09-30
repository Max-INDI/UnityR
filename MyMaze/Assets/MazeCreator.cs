using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour {
    public int row = 30;
    public int column = 30;
    static public int[,] logmap;
    public int[,] lastmap;
    private GameObject[,] map;
    private Stack<int> road = new Stack<int>();
    public GameObject PreWall;
    public float offset;
    public Vector3 originPoint;

    // Use this for initialization
    void Awake () {
        InitTerrain();
        logmap[row + 1, column - 1] = 0;
        logmap[row, column - 1] = 0;
        DrawMaze();
    }

    private void Start()
    {
        //road.Push(0);
       // FindDirection(1, 1);
    }

    void FindDirection(int i, int j)
    {
        Debug.Log(logmap[i,j]);
        List<int> dir = new List<int>
        {
            0
        };
        IsAccessible2(i, j, ref dir);
        if (IsOut(i, j))
        {
            
            if (dir[0] > 0)
            {
                int num = Random.Range(1, dir.Count);
                road.Push(dir[num]);
                switch (dir[num])
                {
                    case 1:
                        i = i - 1;
                        FindDirection(i, j);
                        break;
                    case 2:
                        i = i + 1;
                        FindDirection(i, j);
                        break;
                    case 3:
                        j = j - 1;
                        FindDirection(i, j);
                        break;
                    case 4:
                        j = j + 1;
                        FindDirection(i, j);
                        break;
                }
            }
            /**/else
            {
                if (road.Peek() != 0)
                {
                    switch (road.Peek())
                    {
                        case 1:
                            i += 1;
                            break;
                        case 2:
                            i -= 1;
                            break;
                        case 3:
                            j += 1;
                            break;
                        case 4:
                            j -= 1;
                            break;
                    }
                    road.Pop();
                    FindDirection(i, j);
                }
            }
        }
    }
    void IsAccessible2(int i, int j, ref List<int> dir)
    {
        //Direction:Up & Down
        if (i >= 1 && i <= row && j >= 1 && j <= column)
        {
            if (i - 1 >= 1)
                if (logmap[i - 1, j] == 0)
                {
                    dir[0]++;
                    dir.Add(1);
                }
            if (i + 1 <= row)
                if (logmap[i + 1, j] == 0)
                {
                    dir[0]++;
                    dir.Add(2);
                }
            if (j - 1 >= 1)
                if (logmap[i, j - 1] == 0)
                {
                    dir[0]++;
                    dir.Add(3);
                }
            if (j + 1 <= column)
                if (logmap[i, j + 1] == 0)
                {
                    dir[0]++;
                    dir.Add(4);
                }
        }
    }

    bool IsOut(int i, int j)
    {
        if (i == row && j == column - 1)
            return true;
        else
            return false;
    }

    void CopyMap()
    {
        
        for (int i = 1; i <= row; i++)
        {
            for (int j = 1; j <= column; j++)
            {
                
            }
        }
    }

















    void InitTerrain()
    {
        road.Push(0);
        logmap = new int[row + 2, column + 2];
        map = new GameObject[row + 2, column + 2];
        //生成逻辑地图
        int flag = 1;
        for (int i = 0; i < row + 2; i++)
        {
            flag *= -1;
            for (int j = 0; j < column + 2; j++)
            {

                if (i == 0 || j == 0 || i == row + 1 || j == column + 1 || i % 2 == 0 )
                    //外墙标记为-1
                    logmap[i, j] = -1;
                else
                {
                    //初始化逻辑通路：1为通路，-1为障碍物
                    logmap[i, j] = flag; 
                    flag *= -1;
                    
                }
            }
        }
        logmap[1, 1] = 0;
        QueryRoad(1, 1);
    }

    void QueryRoad(int i, int j)
    {
        List<int> dir = new List<int>
        {
            0
        };
        if (!IsVisttAll())
        {
            IsAccessible(i, j, ref dir);
            if (dir[0] > 0)
            {
                int num = Random.Range(1, dir.Count);
                road.Push(dir[num]);
                switch (dir[num])
                {
                    case 1:
                        i = i - 2;
                        logmap[i, j] = 0;
                        logmap[i + 1, j] = 0;
                        QueryRoad(i, j);
                        break;
                    case 2:
                        i = i + 2;
                        logmap[i, j] = 0;
                        logmap[i - 1, j] = 0;
                        QueryRoad(i, j);
                        break;
                    case 3:
                        j = j - 2;
                        logmap[i, j] = 0;
                        logmap[i, j + 1] = 0;
                        QueryRoad(i, j);
                        break;
                    case 4:
                        j = j + 2;
                        logmap[i, j] = 0;
                        logmap[i, j - 1] = 0;
                        QueryRoad(i, j);
                        break;
                }
            }
            else
            {
                if (road.Peek() != 0)
                {
                    switch (road.Peek())
                    {
                        case 1:
                            i += 2;
                            break;
                        case 2:
                            i -= 2;
                            break;
                       case 3:
                          j += 2;
                          break;
                        case 4:
                            j -= 2;
                           break;
                    }
                    road.Pop();
                    QueryRoad(i, j);
                }
                
            }
        }
    }

    //检查方向的可行性
    void IsAccessible(int i, int j, ref List<int> dir)
    {
        //Direction:Up & Down
        if (i  >= 1 && i <= row && j >= 1 && j <= column)
        {
            if(i - 2 >= 1)
                if (logmap[i - 2, j] == 1)
                {
                    dir[0]++;
                    dir.Add(1);
                }       
            if (i + 2 <= row)
                if (logmap[i + 2, j] == 1)
                 {
                    dir[0]++;
                    dir.Add(2);
                }        
            if (j - 2 >= 1)
                if (logmap[i, j - 2] == 1)
                {
                    dir[0]++;
                    dir.Add(3);
                }
            if (j + 2 <= column)
                if (logmap[i, j + 2] == 1)
                {
                    dir[0]++;
                    dir.Add(4);
                }
        }
    }

    //检查是否已经访问完所有标记为1的节点
    bool IsVisttAll()
    {
        int flag = 0;
        for(int i =1; i <= row; i++)
        {
            for (int j = 1; j <= column; j++)
            {
                if (logmap[i, j] == 1)
                {
                    flag = 1;
                }
            }
        }
        if (flag == 1)
            return false;
        else
            return true;
        
    }

    void DrawMaze()
    {
        for (int i = 0; i < row + 2; i++)
        {
            for(int j = 0; j < column + 2; j++)
            {
                if (logmap[i, j] == -1)
                {
                    map[i, j] = Instantiate(PreWall, originPoint + new Vector3(j * offset, 0, i * offset), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
