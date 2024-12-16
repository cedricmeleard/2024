namespace TaskAssignmentSystem
{
    public class TaskAssignmentService
    {
        private readonly List<Elf> _elves;
        public int TotalTasksCompleted { get; private set; }

        // Constructor
        public TaskAssignmentService(IEnumerable<Elf> elves)
        {
            if (elves == null || !elves.Any()) throw new ArgumentException("A list of elves is required.");
            _elves = elves.ToList();
        }

        // Report Task Completion
        public bool ReportTaskCompletion(int elfId)
        {
            var elf = FindElfById(elfId);
            if (elf == null) return false;

            TotalTasksCompleted++;
            return true;
        }

        // Get the Elf with the Highest Skill Level
        public Elf GetElfWithHighestSkill()
        {
            return _elves.OrderByDescending(elf => elf.SkillLevel).First();
        }

        // Assign Task Based on Skills
        public Elf AssignTask(int taskSkillRequired)
        {
            return _elves.FirstOrDefault(elf => elf.SkillLevel >= taskSkillRequired + 1);
        }

        // Reassign Tasks
        // Task reassignment works only if the "from" elf has a lower or equal skill level compared to the "to" elf.
        public bool TryReassignTask(int fromElfId, int toElfId)
        {
            var fromElf = FindElfById(fromElfId);
            var toElf = FindElfById(toElfId);

            if (fromElf == null || toElf == null) return false;
            if (fromElf.SkillLevel > toElf.SkillLevel) return false;

            return true;
        }

        // Get Elves by Skill in Descending Order
        public List<Elf> GetElvesBySkillDescending()
        {
            return _elves.OrderByDescending(elf => elf.SkillLevel).ToList();
        }

        // Reset All Skills to a Baseline Level
        public void ResetAllSkillsToBaseline(int baseline)
        {
            if (baseline <= 0) throw new ArgumentException("Baseline must be positive.");

            foreach (var elf in _elves)
            {
                elf.SetSkillLevel(baseline);
            }
        }

        // Private Helper Methods
        private Elf FindElfById(int elfId)
        {
            return _elves.FirstOrDefault(e => e.Id == elfId);
        }
    }
}