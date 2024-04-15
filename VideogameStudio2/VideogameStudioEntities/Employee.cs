using System;

namespace VideogameStudio
{
    public abstract class Employee : IWorker
    {
        public int ID { get; private set; }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            private set
            {
                if (!char.IsUpper(value[0]))
                {
                    throw new ArgumentException("Имя сотрудника должно начинаться с большой буквы.");
                }
                firstName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            private set
            {
                if (!char.IsUpper(value[0]))
                {
                    throw new ArgumentException("Фамилия сотрудника должна начинаться с большой буквы.");
                }
                lastName = value;
            }
        }

        private int age;
        public int Age
        {
            get => age;
            private set
            {
                if (value < 20 || value > 80)
                {
                    throw new ArgumentException("Принимаем в студию только сотрудников в возрасте от 20 до 80 лет.");
                }
                age = value;
            }
        }

        private int workExpierence;
        public int WorkExperience
        {
            get => workExpierence;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Опыт работы нанимаемого сотрудника не может быть отрицательным.");
                }
                workExpierence = value;
            }
        }

        private int workedMonths = 0;
        public int WorkedMonths
        {
            get => workedMonths;
            private set
            {
                workedMonths = value;
                if (workedMonths > 0 && workedMonths % 12 == 0)
                {
                    ImproveSkills();
                }
            }
        }

        public int Productivity { get; protected set; }

        public decimal Salary { get; protected set; }

        public abstract Specilization Speciality { get; }

        public HappinessLevel Happiness { get; private set; } = HappinessLevel.Happy;

        public Employee(string firstName, string lastName, int age, int workExperience)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            WorkExperience = workExperience;
            if (age - workExpierence < 18)
            {
                throw new ArgumentException("Опыт работы нанимаемого сотрудника не соответствует возрасту (считается только опыт работы с 18 лет).");
            }
        }

        public Employee(int id, string firstName, string lastName, int age, int workExperience,
            int workedMonths, int productivity, decimal salary, string happinessLevel)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            WorkExperience = workExperience;
            WorkedMonths = workedMonths;
            Productivity = productivity;
            Salary = salary;
            if (happinessLevel == "Счастлив")
            {
                Happiness = HappinessLevel.Happy;
            }
            else if (happinessLevel == "Несчастлив")
            {
                Happiness = HappinessLevel.Unhappy;
            }
        }

        public virtual void ImproveSkills()
        {
            WorkExperience++;
            Productivity += 20;
            Salary += 10000;
        }

        public void SimulateMonthWorking(decimal salary)
        {
            WorkedMonths++;
            if (Salary > salary)
            {
                Happiness = HappinessLevel.Unhappy;
            }
            if (Happiness == HappinessLevel.Unhappy && Salary <= salary)
            {
                Happiness = HappinessLevel.Happy;
            }
        }
    }
}
