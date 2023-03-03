using System.Transactions;

namespace tetris;
//
// This is the game event logic that you can customize and cannibalize
// as needed. You should try to write your game in a modular way, avoid
// making one huge Game class.
//

class Game
{
    ScheduleTimer? _timer;

    private Block _currentBlock;

    public Block CurrentBlock
    {
        get => _currentBlock;
        private set
        {
            _currentBlock = value;
            _currentBlock.Reset();
        }
    }
    public Matrix Matrix { get; }
    public BlockQueue BlockQueue { get; }

    public Game()
    {
        Matrix = new Matrix(20, 10);
        BlockQueue = new BlockQueue();
        CurrentBlock = BlockQueue.GetAndUpdate();
    }
    public bool Paused { get; private set; }
    public bool GameOver { get; private set; }

    private bool BlockFits()
    {
        foreach (var p in CurrentBlock.TilePositions())
        {
            if (!Matrix.IsEmpty(p.Row, p.Column))
            {
                return false;
            }
        }
        return true;
    }

    public void RotateRight()
    {
        CurrentBlock.RotateRight();
        if (!BlockFits())
        {
            CurrentBlock.RotateLeft();;
        }
    }

    public void RotateLeft()
    {
        CurrentBlock.RotateLeft();
        if (!BlockFits())
        {
            CurrentBlock.RotateRight();
        }
    }

    public void MoveBlockRight()
    {
        CurrentBlock.Move(0,1);
        if (!BlockFits())
        {
            CurrentBlock.Move(0,-1);
        }
    }

    public void MoveBlockLeft()
    {
        CurrentBlock.Move(0,-1);
        if (!BlockFits())
        {
            CurrentBlock.Move(0,-1);
        }
    }

    private bool IsGameOver()
    {
        return !(Matrix.RowIsEmpty(0) && Matrix.RowIsEmpty(1));
    }

    private void PlaceBlock()
    {
        foreach (var p in CurrentBlock.TilePositions())
        {
            Matrix[p.Row, p.Column] = CurrentBlock.Id;
        }

        if (IsGameOver())
        {
            GameOver = true;
        }
        else
        {
            CurrentBlock = BlockQueue.GetAndUpdate();
        }
    }

    public void MoveBlockDown()
    {
        CurrentBlock.Move(1,0);
        if (!BlockFits())
        {
            CurrentBlock.Move(-1,0);
            PlaceBlock();
        }
    }
    public void Start()
    {
        Console.WriteLine("Start");
        ScheduleNextTick();
    }

    public void Pause()
    {
        Console.WriteLine("Pause");
        Paused = true;
        _timer!.Pause();
    }

    public void Resume()
    {
        Console.WriteLine("Resume");
        Paused = false;
        _timer!.Resume();
    }

    public void Stop()
    {
        Console.WriteLine("Stop");
        GameOver = true;
    }

    public void Input(ConsoleKey key)
    {
        Console.WriteLine($"Player pressed key: {key}");
        switch (key)
        {
            case ConsoleKey.RightArrow:
                MoveBlockRight();
                break;
            case ConsoleKey.LeftArrow:
                MoveBlockLeft();
                break;
            case ConsoleKey.X:
                RotateRight();
                break;
            case ConsoleKey.Z:
                RotateLeft();
                break;
            case ConsoleKey.DownArrow:
                MoveBlockDown();
                break;
        }
    }

    void Tick()
    {
        MoveBlockDown();
        ScheduleNextTick();
    }

    void ScheduleNextTick()
    {
        // the game will automatically update itself every half a second, adjust as needed
        _timer = new ScheduleTimer(500, Tick);
    }
}
