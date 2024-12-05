using System.Text.RegularExpressions;
using EID.ValueObjects;
using LanguageExt;
using LanguageExt.Common;

namespace EID;

public class Eid
{
    private readonly string _value;
    private const int EidLength = 8;
    private const int MeaningfulEidLength = 6;
    private const int BirthYearLength = 2;
    private const int ComplementNumber = 97;
    private const int SerialNumberIndex = 3;
    private const int SerialNumberLength = 3;
    
    public Sex Sex { get; }
    public SerialNumber SerialNumber { get; }
    public BirthYear BirthYear { get; }
    public override string ToString() => _value;
    private Eid(string eid, Sex sex, SerialNumber serialNumber, BirthYear birthYear)
    {
        _value = eid;
        Sex = sex;
        BirthYear = birthYear;
        SerialNumber = serialNumber;
    }
    
    public static Either<Eid, Error> Parse(string? eid)
    {
        if (string.IsNullOrWhiteSpace(eid)) return Error.New("it is null or whitespace");
        if (IsNotAValidLength(eid)) return Error.New("it doesn't match the required size");
        if (!IsADigit(eid)) return Error.New("it is not composed of digits only");
        if (!ValidateEidSex(eid, out var sex)) return Error.New("it doesn't start with 1-3");
        if (!ValidateSerialNumberFormat(eid, out var serialNumber)) return Error.New("SN can't be 000");
        if(IsEidValid(eid)) return Error.New($"Validation isn't correct");
        
        return new Eid(eid, sex, serialNumber!, BirthYear.Parse(eid.Substring(1, 2)));
    }
    private static bool IsNotAValidLength(string eid) => eid.Length != EidLength;
    private static bool IsADigit(string eid) => eid.All(char.IsDigit);
    private static bool ValidateEidSex(string eid, out Sex sex)
    {
        bool matchASexValue = Regex.IsMatch(eid, @"^(1|2|3)\d+");
        sex = matchASexValue ? (Sex)int.Parse(eid[0].ToString()) : 0;
        return matchASexValue;
    }

    private static bool ValidateSerialNumberFormat(string eid, out SerialNumber? serialNumber)
    {
        bool matchSerialNumber = Regex.IsMatch(eid, @"^\d{3}\d{2}[1-9]\d{2}$");
        serialNumber = matchSerialNumber ? SerialNumber.Parse(eid.Substring(SerialNumberIndex, SerialNumberLength)) : null;
        return matchSerialNumber;
    }
    
    private static bool IsEidValid(string eid) => ComputeValidationKey(eid) != GetValidationNumber(eid);
    private static int GetValidationNumber(string eid) => int.Parse(eid.Substring(MeaningfulEidLength, BirthYearLength));
    private static int GetBaseNumber(string eid) => int.Parse(eid[..MeaningfulEidLength]);
    private static int ComputeValidationKey(string eid) => ComplementNumber - GetBaseNumber(eid) % ComplementNumber;
}