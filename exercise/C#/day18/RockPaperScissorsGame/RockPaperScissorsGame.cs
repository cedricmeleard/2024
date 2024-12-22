using LanguageExt;

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
        public static Result Play(Choice player1, Choice player2)
            => VerifyPlayerWin(player1, player2, Winner.Player1)
                .Bind(_ => VerifyPlayerWin(player2, player1, Winner.Player2))
                .Match(
                    draw => new Result(Winner.Draw, "same choice"),
                    win => new Result(win.Winner, win.Reason)
                );

        private static Either<AGameWin, AGameDraw> VerifyPlayerWin(Choice shouldWin, Choice mightLoose, Winner winner)
            => shouldWin switch
            {
                Choice.Rock when mightLoose == Choice.Scissors => new AGameWin(winner, "rock crushes scissors"),
                Choice.Rock when mightLoose == Choice.Lizard => new AGameWin(winner, "rock crushes lizard"),
                Choice.Paper when mightLoose == Choice.Rock => new AGameWin(winner, "paper covers rock"),
                Choice.Paper when mightLoose == Choice.Spock => new AGameWin(winner, "paper disproves spock"),
                Choice.Scissors when mightLoose == Choice.Paper => new AGameWin(winner, "scissors cuts paper"),
                Choice.Scissors when mightLoose == Choice.Lizard => new AGameWin(winner, "scissors decapitates lizard"),
                Choice.Lizard when mightLoose == Choice.Spock => new AGameWin(winner, "lizard poisons spock"),
                Choice.Lizard when mightLoose == Choice.Paper => new AGameWin(winner, "lizard eats paper"),
                Choice.Spock when mightLoose == Choice.Rock => new AGameWin(winner, "spock vaporizes rock"),
                Choice.Spock when mightLoose == Choice.Scissors => new AGameWin(winner, "spock smashes scissors"),
                _ => new AGameDraw()
            };
    }

    public record AGameDraw;
    public record AGameWin(Winner Winner, string Reason);
}