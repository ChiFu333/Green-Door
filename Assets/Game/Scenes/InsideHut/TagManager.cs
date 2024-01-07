using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class TagManager : MonoBehaviour
{
    public int[] state = new int[9] { 1, 2, 4, 1, 5, 9, 3, 3, 0 }; //L - 1, след поворот против часовой 9 - клетка без провода 0 - нет клетки 5 - горизонт.
    private const int SIZE = 170; //массив идёт сверзу вниз слева направо
    [SerializeField] private UnityEvent afterComplete = new UnityEvent();
    /// </summary>
    void Start()
    {
        
    }  
    public void MoveBlock(GameObject block)
    {
        Block bl = block.GetComponent<Block>();
        if (CheckPos(bl.pos))
        {
            int b = -1;
            for(int i = 0; i < state.Length; i++)
            {
                if (state[i] == 0)
                {
                    b = i;
                    break;
                }
            }
            block.GetComponent<RectTransform>().anchoredPosition = new Vector2(-SIZE, SIZE) + new Vector2(SIZE * (b%3), -SIZE * Mathf.Floor(b/3));
            state[b] = bl.number;
            state[bl.pos] = 0;
            bl.pos = b;
            if (IsComplete()) afterComplete.Invoke();
        }
    }
    private bool CheckPos(int p)
    {
        bool A = false;
        switch (p)
        {
            case 0:
                A = CheckNum(1,3,1,3);
                break;
            case 1:
                A = CheckNum(0, 2, 4, 0);
                break;
            case 2:
                A = CheckNum(1, 5, 1, 5);
                break;
            case 3:
                A = CheckNum(0, 6, 4, 0);
                break;
            case 4:
                A = CheckNum(1, 7, 3, 5);
                break;
            case 5:
                A = CheckNum(2, 2, 4, 8);
                break;
            case 6:
                A = CheckNum(3, 3, 7, 7);
                break;
            case 7:
                A = CheckNum(4, 4, 6, 8);
                break;
            case 8:
                A = CheckNum(7, 7, 5, 5);
                break;

        }
        return A;
    }
    private bool CheckNum(int a,int b,int c, int d)
    {
        return state[a] == 0 || state[b] == 0 || state[c] == 0 || state[d] == 0;
    }
    public bool IsComplete()
    {
        int[] win = new int[9] { 9, 1, 3, 4, 5, 2, 1, 3, 0 };
        for(int i = 0; i < 9; i++)
        {
            if (state[i] != win[i]) return false;
        }
        return true;
    }
}
