namespace VideogameStudio
{
    interface IWorker : IPerson
    {
        int ID { get; }

        int WorkExperience { get; }

        int WorkedMonths { get; }

        int Productivity { get; }

        decimal Salary { get; }

        Specilization Speciality { get; }

        HappinessLevel Happiness { get; }

        void ImproveSkills();

        void SimulateMonthWorking(decimal salary);
    }
}
