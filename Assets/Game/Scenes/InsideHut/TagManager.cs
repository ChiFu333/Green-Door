using UnityEngine;

public class TagManager : MonoBehaviour
{
    public int[] state = new int[9] { 9, 1, 3, 4, 5, 2, 1, 3, 0 }; //L - 1, след поворот против часовой 9 - клетка без провода 0 - нет клетки 5 - горизонт.
    private const int SIZE = 170; //массив идёт сверзу вниз слева направо
    /// <summary>
    /// 0 1 2
    /// 3 4 5
    /// 6 7 8
    /// </summary>
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
        }
    }

    private bool CheckPos(int p) {
        if (IsZeroAtIndex(-3, p)) return true;
        if (IsZeroAtIndex(+3, p)) return true;
        if (IsZeroAtIndex(+1, p)) return true;
        if (IsZeroAtIndex(-1, p)) return true;
        return false;
    }
    private bool IsZeroAtIndex(int offset, int p) {
        int index = p + offset;
        if (index < 0 || index > 8) return false;
        if (Mathf.Abs(offset) == 1 && p / 3 != index / 3) return false;
        return state[index] == 0;
    }
}
