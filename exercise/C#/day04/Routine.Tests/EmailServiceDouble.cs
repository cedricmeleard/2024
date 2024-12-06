namespace Routine.Tests;

public class EmailServiceDouble : IEmailService
{
    private bool _readNewEmailsWasCalled;
    public void ReadNewEmails() => _readNewEmailsWasCalled = true;
    public void ShouldEnsureNewEmailHasBeenRead() => _readNewEmailsWasCalled.Should().BeTrue();
}