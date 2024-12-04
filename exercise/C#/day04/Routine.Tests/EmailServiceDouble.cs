namespace Routine.Tests;

public class EmailServiceDouble : IEmailService
{
    public void ReadNewEmails()
    {
        Console.WriteLine("IEmailService : Reading newEmails");
    }
}