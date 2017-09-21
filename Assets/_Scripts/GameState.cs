public class GameState
{
    public enum States
    {
        Running,
        Paused,
		GameOver
    }

    private static States _currentState;

    public static void ChangeState (States newGameState)
    {
        if (IsState (newGameState))
        {
            return;
        }
		else
		{
			_currentState = newGameState;
		}
    }

    public static bool IsState (States state)
    {
        if (_currentState == state)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsRunning
    {
        get
        {
            return IsState (States.Running);
        }
    }

    public static bool IsPaused
    {
        get
        {
            return IsState (States.Paused);
        }
    }

	public static bool IsGameOver
	{
		get
		{
			return IsState (States.GameOver);
		}
	}
}