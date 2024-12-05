namespace EID.ValueObjects;

public class SerialNumber(string value)
{
    public string Value => value;
    public static SerialNumber? Parse(string value)
    {
        return new SerialNumber(value);
    }
}