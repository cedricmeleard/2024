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
            => VerifyPlayerWin(player1, player2, Winner.Player1) 
               ?? VerifyPlayerWin(player2, player1, Winner.Player2);

        private static Result? VerifyPlayerWin(Choice shouldWin, Choice mightLoose, Winner winner)
            => shouldWin == mightLoose 
                ? new Result(Winner.Draw, "same choice") 
                : DetermineWinningResult(shouldWin, mightLoose, winner);

        private static Result? DetermineWinningResult(Choice shouldWin, Choice mightLoose, Winner winner)
            => shouldWin switch
            {
                Choice.Rock when mightLoose == Choice.Scissors => new Result(winner, "rock crushes scissors"),
                Choice.Rock when mightLoose == Choice.Lizard => new Result(winner, "rock crushes lizard"),
                Choice.Paper when mightLoose == Choice.Rock => new Result(winner, "paper covers rock"),
                Choice.Paper when mightLoose == Choice.Spock => new Result(winner, "paper disproves spock"),
                Choice.Scissors when mightLoose == Choice.Paper => new Result(winner, "scissors cuts paper"),
                Choice.Scissors when mightLoose == Choice.Lizard => new Result(winner, "scissors decapitates lizard"),
                Choice.Lizard when mightLoose == Choice.Spock => new Result(winner, "lizard poisons spock"),
                Choice.Lizard when mightLoose == Choice.Paper => new Result(winner, "lizard eats paper"),
                Choice.Spock when mightLoose == Choice.Rock => new Result(winner, "spock vaporizes rock"),
                Choice.Spock when mightLoose == Choice.Scissors => new Result(winner, "spock smashes scissors"),
                _ => null
            };
    }
}