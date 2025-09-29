using UnityEngine;
using YG;

namespace _Source.Scripts.GamePlay.StaticClass.BankResources
{
    public static class LocalBank
    {
        private static int s_currentScoreBoss = 7;

        public static int Score { get; private set; }

        public static void AddScore(int countScore)
        {
            Score += countScore;

            if (s_currentScoreBoss != 0)
                s_currentScoreBoss--;
            else
                s_currentScoreBoss = 10;
        }

        public static void TryChangeScore()
        {
            if (Score > YG2.saves.Score)
            {
                YG2.saves.Score = Score;
                YG2.SaveProgress();
                YG2.SetLeaderboard("LeaderBoard", YG2.saves.Score);
            }

            ResetScore();
        }

        public static int TakeScoreBoss() => s_currentScoreBoss;

        public static int ResetScore() => Score = 0;
    }
}