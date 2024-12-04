namespace Routine.Tests;

public class EmailServiceDouble : IEmailService
{
    public void ReadNewEmails() => ReadNewEmailsWasCalled = true;
    public bool ReadNewEmailsWasCalled { get; set; }
}