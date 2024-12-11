namespace Delivery
{
    public static class Building
    {
        public static int WhichFloor(string instructions)
        {
            (int left, int right) = instructions.Contains("🧝") ? (-2, 3) : (1, -1);

            return instructions.Replace("🧝", "")
                .GroupBy(x => x)
                .Sum(x => x.Count() * (x.Key == '(' ? left : right));
        }
    }
}