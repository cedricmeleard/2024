namespace EID.ValueObjects;

public class BirthYear(int value)
{
    public int Value { get; } = value;
    public static BirthYear Parse(string value)
    {
        if (int.TryParse(value, out var parsedValue)) {
            if (parsedValue < 0 || parsedValue > 99) {
                throw new ArgumentException("Birth year must be between 0 and 99");
            }
            return new BirthYear(parsedValue);    
        }
        
        throw new ArgumentException("Not a valid birth year");
    }
}