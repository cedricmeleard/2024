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
                    _ => throw new IncertainResultException(),
                    win => new Result(win.Winner, win.Reason)
                );

        private static Either<IEndGame, AnIncertainResultYet> VerifyPlayerWin(Choice shouldWin, Choice mightLoose, Winner winner)
        {
            if (shouldWin == mightLoose) return new ADraw();
            
            return shouldWin switch
            {
                Choice.Rock when mightLoose == Choice.Scissors => new AWin(winner, "rock crushes scissors"),
                Choice.Rock when mightLoose == Choice.Lizard => new AWin(winner, "rock crushes lizard"),
                Choice.Paper when mightLoose == Choice.Rock => new AWin(winner, "paper covers rock"),
                Choice.Paper when mightLoose == Choice.Spock => new AWin(winner, "paper disproves spock"),
                Choice.Scissors when mightLoose == Choice.Paper => new AWin(winner, "scissors cuts paper"),
                Choice.Scissors when mightLoose == Choice.Lizard => new AWin(winner, "scissors decapitates lizard"),
                Choice.Lizard when mightLoose == Choice.Spock => new AWin(winner, "lizard poisons spock"),
                Choice.Lizard when mightLoose == Choice.Paper => new AWin(winner, "lizard eats paper"),
                Choice.Spock when mightLoose == Choice.Rock => new AWin(winner, "spock vaporizes rock"),
                Choice.Spock when mightLoose == Choice.Scissors => new AWin(winner, "spock smashes scissors"),
                _ => new AnIncertainResultYet()
            };
        }
    }
    
    public record AnIncertainResultYet;

    public interface IEndGame
    {
        public Winner Winner { get; }
        public string Reason { get; }
    }
    public record AWin(Winner Winner, string Reason) : IEndGame;

    public record ADraw : IEndGame
    {
        public Winner Winner => Winner.Draw;
        public string Reason => "same choice";
    };
    
    public class IncertainResultException() : Exception("Can't determine a Result");
}