namespace VideogameStudio
{
    public sealed class SoftwareTester: Employee
    {
        public SoftwareTester(string firstName,  string lastName, int age, int workExperience)
            : base(firstName, lastName, age, workExperience)
        {
            Productivity = 20 + (workExperience * 20);
            Salary = 30000 + (workExperience * 20000);
        }

        public SoftwareTester(int id, string firstName, string lastName, int age, int workExperience,
            int workedMonths, int productivity, decimal salary, string happinessLevel)
                : base(id, firstName, lastName, age, workExperience, workedMonths, productivity, salary, happinessLevel)
        { }

        public override Specilization Speciality => Specilization.SoftwareTester;
    }
}
