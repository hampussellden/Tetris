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
        return true;
    }

    public bool IsEmpty(int r, int c)
    {
        return true;
    }

    public bool RowIsFull(int r, int c)
    {
        return true;
    }

    public bool RowIsEmpty(int r, int c)
    {
        return true;
        
    }

    public void ClearRow(int r)
    {
        
    }

    //after a row is cleared the above row needs to replace it
    public void MoveRowDown(int r, int count)
    {

    }

    //Checks for Full rows using above methods then giving a score depending on the amount of cleared rows
    public int ClearFullRows()
    {
        return 1;
    }
}