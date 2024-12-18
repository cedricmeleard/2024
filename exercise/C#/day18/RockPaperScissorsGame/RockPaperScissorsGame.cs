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
            if (player1 == player2)
                return new Result(Winner.Draw, "same choice");
            else if (player1 == Choice.Rock && player2 == Choice.Scissors)
                return new Result(Winner.Player1, "rock crushes scissors");
            else if (player1 == Choice.Rock && player2 == Choice.Lizard)
                return new Result(Winner.Player1, "rock crushes lizard");
            else if (player1 == Choice.Paper && player2 == Choice.Rock)
                return new Result(Winner.Player1, "paper covers rock");
            else if (player1 == Choice.Paper && player2 == Choice.Spock)
                return new Result(Winner.Player1, "paper disproves spock");
            else if (player1 == Choice.Scissors && player2 == Choice.Paper)
                return new Result(Winner.Player1, "scissors cuts paper");
            else if (player1 == Choice.Scissors && player2 == Choice.Lizard)
                return new Result(Winner.Player1, "scissors decapitates lizard");
            else if (player1 == Choice.Lizard && player2 == Choice.Spock)
                return new Result(Winner.Player1, "lizard poisons spock");
            else if (player1 == Choice.Lizard && player2 == Choice.Paper)
                return new Result(Winner.Player1, "lizard eats paper");
            else if (player1 == Choice.Spock && player2 == Choice.Rock)
                return new Result(Winner.Player1, "spock vaporizes rock");
            else if (player1 == Choice.Spock && player2 == Choice.Scissors)
                return new Result(Winner.Player1, "spock smashes scissors");
            
            else if (player2 == Choice.Rock && player1 == Choice.Scissors)
                return new Result(Winner.Player2, "rock crushes scissors");
            else if (player2 == Choice.Rock && player1 == Choice.Lizard)
                return new Result(Winner.Player2, "rock crushes lizard");
            else if (player2 == Choice.Paper && player1 == Choice.Rock)
                return new Result(Winner.Player2, "paper covers rock");
            else if (player2 == Choice.Paper && player1 == Choice.Spock)
                return new Result(Winner.Player2, "paper disproves spock");
            else if(player2 == Choice.Scissors && player1 == Choice.Paper)
                return new Result(Winner.Player2, "scissors cuts paper");
            else if (player2 == Choice.Scissors && player1 == Choice.Lizard)
                return new Result(Winner.Player2, "scissors decapitates lizard");
            else if (player2 == Choice.Lizard && player1 == Choice.Spock)
                return new Result(Winner.Player2, "lizard poisons spock");
            else if (player2 == Choice.Lizard && player1 == Choice.Paper)
                return new Result(Winner.Player2, "lizard eats paper");
            else if (player2 == Choice.Spock && player1 == Choice.Rock)
                return new Result(Winner.Player2, "spock vaporizes rock");
            else if (player2 == Choice.Spock && player1 == Choice.Scissors)
                return new Result(Winner.Player2, "spock smashes scissors");
            
            else
                return null;
                
        }
    }
}