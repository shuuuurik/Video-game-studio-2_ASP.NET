namespace VideogameStudio
{
    public sealed class Developer : Employee
    {
        public Developer(string firstName, string lastName, int age, int workExperience)
            : base(firstName, lastName, age, workExperience)
        {
            Productivity = 10 + (workExperience * 10);
            Salary = 50000 + (workExperience * 20000);
        }

        public Developer(int id, string firstName, string lastName, int age, int workExperience,
            int workedMonths, int productivity, decimal salary, string happinessLevel) 
                : base(id, firstName,lastName, age, workExperience, workedMonths, productivity, salary, happinessLevel)
        { }

        public override Specilization Speciality => Specilization.Developer;

        public override void ImproveSkills()
        {
            WorkExperience++;
            Productivity += 10;
            Salary += 20000;
        }
    }
}
