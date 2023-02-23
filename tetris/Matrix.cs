namespace tetris;

public class Matrix
{
    public int Columns { get; }
    public int Rows { get; }
    private readonly int[,] _grid;

    public int this[int r, int c]
    {
        get => _grid[r, c];
        set => _grid[r, c] = value;
    }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _grid = new int[rows, columns];
    }

    public bool IsInside(int r, int c)
    {
        return r >= 0 && r < Rows && c >= 0 && c < Columns;
    }

    public bool IsEmpty(int r, int c)
    {
        return IsInside(r, c) && _grid[r, c] == 0;
    }

    //Goes through the grid and checks for rows with no 0 value cells
    public bool RowIsFull(int r)
    {
        for (int c = 0; c < Columns; c++)
        {
            if (_grid[r, c] == 0)
            {
                return false; //returns false as soon as we find an empty cell
            }
        }

        return true;
    }
    
    //Does the opposite of the above method
    public bool RowIsEmpty(int r)
    {
        for (int c = 0; c < Columns; c++)
        {
            if (_grid[r, c] != 0)
            {
                return false; //returns false as soon as we find an empty cell
            }
        }

        return true;
    }

    public void ClearRow(int r)
    {
        for (int c = 0; c < Columns; c++)
        {
            _grid[r, c] = 0;
        }
    }

    //after a row is cleared the above row needs to replace it
    public void MoveRowDown(int r, int count)
    {
        for (int c = 0; c < Columns; c++)
        {
            _grid[r + count, c] = _grid[r, c];
            _grid[r, c] = 0;
        }
    }

    //Checks for Full rows using above methods then giving a score depending on the amount of cleared rows
    public int ClearFullRows()
    {
        int count = 0;
        for (int r = Rows - 1; r >= 0; r--)
        {
            if (RowIsFull(r))
            {
                ClearRow(r);
                count++;
            } else if (count > 0)
            {
                MoveRowDown(r,count);
            }
        }
        return count;
    }
}