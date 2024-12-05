using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;

namespace EID;

public class EID
{
    const int EIDLength = 8;
    public static Either<Error, string> Parse(string? eid)
    {
        if (string.IsNullOrWhiteSpace(eid)) {
            return Error.New("it is null or whitespace");
        }
        if (eid.Length != EIDLength) {
            return Error.New("it doesn't match the required size");
        }
        
        if (!eid.All(char.IsDigit)) {
            return Error.New("it is not composed of digits only");
        }
        
        if (!Regex.IsMatch(eid, "^(1|2|3)\\d+")) {
            return Error.New("it doesn't start with 1-3");
        }
        
        if (!Regex.IsMatch(eid, "^\\d{3}\\d{2}[1-9]\\d{2}$")) {
            return Error.New("SN can't be 000");
        }
        
        return eid;
    }
}