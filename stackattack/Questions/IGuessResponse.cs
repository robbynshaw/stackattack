using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stackattack.Questions
{
    public interface IGuessResponse
    {
        bool Success { get; }
        int Score { get; }
    }

    public class GuessResponse : IGuessResponse
    {
        public bool Success { get; private set; }
        public int Score { get; private set; }

        public GuessResponse(bool success, int score)
        {
            this.Success = success;
            this.Score = score;
        }
    }
}