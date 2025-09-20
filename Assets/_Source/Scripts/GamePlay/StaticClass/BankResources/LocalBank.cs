using YG;

public static class LocalBank
{
    private static int CurrentScoreBoss = 7;

    public static int Score { get; private set; }

    public static void AddScore(int countScore)
    {
        Score += countScore;

        if (CurrentScoreBoss != 0)
            CurrentScoreBoss--;
        else
            CurrentScoreBoss = 10;

    }

    public static void TryChangeScore()
    {
        if (Score > YG2.saves.Score)
        {
            YG2.saves.Score = Score;
            YG2.SaveProgress();
            YG2.SetLeaderboard("LeaderBoard", YG2.saves.Score);
        }
    }

    public static int TakeScoreBoss() => CurrentScoreBoss;

    public static int ResetScore() => Score = 0;
}