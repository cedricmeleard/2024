namespace TaskAssignmentSystem
{
    public class TaskAssignment(IEnumerable<Elf> elves)
    {
        public bool ReportTaskCompletion(int elfId)
        {
            var elf = elves.FirstOrDefault(e => e.Id == elfId);
            if (elf != null)
            {
                TotalTasksCompleted++;
                return true;
            }

            return false;
        }

        public int TotalTasksCompleted { get; private set; }

        public Elf ElfWithHighestSkill()
            => elves.Aggregate((prev, current) => prev.SkillLevel > current.SkillLevel ? prev : current);

        private readonly List<int> _unassignedTasks = new();

        public Elf AssignTask(int taskSkillRequired)
        {
            var elf = elves.Where(e => e.SkillLevel >= taskSkillRequired)
                .OrderBy(e => e.SkillLevel)
                .FirstOrDefault();
            if (elf == null)
            {
                _unassignedTasks.Add(taskSkillRequired);
                return null; // No suitable elf found
            }

            return elf;
        }

        public void IncreaseSkillLevel(int elfId, int increment)
        {
            var elf = elves.FirstOrDefault(e => e.Id == elfId);
            if (elf != null)
            {
                elf.SkillLevel += increment;
            }
        }

        public void DecreaseSkillLevel(int elfId, int decrement)
        {
            var elf = elves.FirstOrDefault(e => e.Id == elfId);
            if (elf != null)
            {
                elf.SkillLevel = Math.Max(elf.SkillLevel - decrement, 1); // Enforce minimum skill level
            }
        }

        public bool ReassignTask(int fromElfId, int toElfId)
        {
            var fromElf = elves.FirstOrDefault(e => e.Id == fromElfId);
            var toElf = elves.FirstOrDefault(e => e.Id == toElfId);

            if (fromElf != null && toElf != null)
            {
                // Logic for reassigning tasks can be added here
                return true;
            }

            return false;
        }

        public List<Elf> ElvesBySkillDescending() => elves.OrderByDescending(e => e.SkillLevel).ToList();

        public void ResetAllSkillsToBaseline(int baseline)
        {
            foreach (var elf in elves)
            {
                elf.SkillLevel = baseline;
            }
        }
    }
}