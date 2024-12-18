namespace RockPaperScissorsGame
{
    public enum Choice
    {
        Rock,
        Paper,
        Scissors,
        Spock,
        Lizard
    }

    public enum Winner
    {
        Player1,
        Player2,
        Draw
    }

    public record Result(Winner Winner, string Reason);

    public static class RockPaperScissors
    {
        public static Result? Play(Choice player1, Choice player2)
        {
            return VerifyPlayerWin(player1, player2, Winner.Player1) 
                   ?? VerifyPlayerWin(player2, player1, Winner.Player2);
        }

        private static Result? VerifyPlayerWin(Choice shouldWin, Choice mightLoose, Winner winner)
        {
            if (shouldWin == mightLoose)
                return new Result(Winner.Draw, "same choice");
            else if (shouldWin == Choice.Rock && mightLoose == Choice.Scissors)
                return new Result(winner, "rock crushes scissors");
            else if (shouldWin == Choice.Rock && mightLoose == Choice.Lizard)
                return new Result(winner, "rock crushes lizard");
            else if (shouldWin == Choice.Paper && mightLoose == Choice.Rock)
                return new Result(winner, "paper covers rock");
            else if (shouldWin == Choice.Paper && mightLoose == Choice.Spock)
                return new Result(winner, "paper disproves spock");
            else if (shouldWin == Choice.Scissors && mightLoose == Choice.Paper)
                return new Result(winner, "scissors cuts paper");
            else if (shouldWin == Choice.Scissors && mightLoose == Choice.Lizard)
                return new Result(winner, "scissors decapitates lizard");
            else if (shouldWin == Choice.Lizard && mightLoose == Choice.Spock)
                return new Result(winner, "lizard poisons spock");
            else if (shouldWin == Choice.Lizard && mightLoose == Choice.Paper)
                return new Result(winner, "lizard eats paper");
            else if (shouldWin == Choice.Spock && mightLoose == Choice.Rock)
                return new Result(winner, "spock vaporizes rock");
            else if (shouldWin == Choice.Spock && mightLoose == Choice.Scissors)
                return new Result(winner, "spock smashes scissors");

            return null;
        }
    }
}