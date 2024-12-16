namespace TaskAssignmentSystem
{
    public class Elf
    {
        // Properties
        public int Id { get; }
        public int SkillLevel { get; private set; }

        // Constructor
        public Elf(int id, int skillLevel)
        {
            if (id <= 0) throw new ArgumentException("Id must be positive.");
            if (skillLevel <= 0) throw new ArgumentException("Skill level must be positive.");

            Id = id;
            SkillLevel = skillLevel;
        }

        // Behavior
        public void IncreaseSkill(int increment)
        {
            if (increment <= 0) throw new ArgumentException("Increment must be positive.");
            SkillLevel += increment;
        }

        public void DecreaseSkill(int decrement)
        {
            if (decrement <= 0) throw new ArgumentException("Decrement must be positive.");
            SkillLevel = Math.Max(1, SkillLevel - decrement);
        }

        public void SetSkillLevel(int baseline)
        {
            SkillLevel = baseline;
        }
    }
}