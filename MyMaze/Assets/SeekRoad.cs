using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekRoad : MonoBehaviour {
    private int row = 8;
    private int column = 8;
    static public int[,] lastmap;
    private Stack<int> road = new Stack<int>();
    public GameObject PreStar;
    public Vector3 originPoint;
    public float offset;
    // Use this for initialization
    void Start () {
        //Debug.Log(MazeCreator.logmap[1,1]);
        road.Push(0);
        CopyMap();
        FindDirection(1, 1);
        InitRoad();
        for(int i = 0; i < row+1; i++)
        {
            for (int j = 0; j < column+1; j++)
            {
                Debug.Log(lastmap[i, j]);
            }
        }
	}

    void FindDirection(int i, int j)
    {
       // Debug.Log(lastmap[i, j]);
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
            /**/
            else
            {
                lastmap[i, j] = 1;
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
                if (lastmap[i - 1, j] == 0)
                {
                    dir[0]++;
                    dir.Add(1);
                }
            if (i + 1 <= row)
                if (lastmap[i + 1, j] == 0)
                {
                    dir[0]++;
                    dir.Add(2);
                }
            if (j - 1 >= 1)
                if (lastmap[i, j - 1] == 0)
                {
                    dir[0]++;
                    dir.Add(3);
                }
            if (j + 1 <= column)
                if (lastmap[i, j + 1] == 0)
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
        lastmap = new int[row + 2, column + 2];
        for (int i = 0; i <= row + 1; i++)
        {
            for (int j = 0; j <= column + 1; j++)
            {
                lastmap[i, j] = MazeCreator.logmap[i, j];
            }
        }
    }

    void InitRoad()
    {
        Debug.Log("!!!");
        Stack<int> trueroad = new Stack<int>();
        while (road.Peek() != 0)
        {
            trueroad.Push(road.Peek());
            road.Pop();
        }
        GameObject[,] starmap;
        starmap = new GameObject[row + 2, column + 2];
        while (trueroad.Count != 0)
        {
            int m = 1;
            int n = 1;
            starmap[m,n]= Instantiate(PreStar, originPoint + new Vector3(n * offset, 0, m * offset), Quaternion.identity);
            switch (trueroad.Peek())
            {
                case 1:
                    m = m - 2;
                    break;
                case 2:
                    m = m + 2;
                    break;
                case 3:
                    n = n - 2;
                    break;
                case 4:
                    n = n + 2;
                    break;
            }
            trueroad.Pop();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
