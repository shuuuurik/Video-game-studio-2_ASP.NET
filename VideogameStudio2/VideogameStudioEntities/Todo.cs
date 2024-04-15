using System;

namespace VideogameStudio
{
    public class Todo : IComparable<Todo>
    {
        public int ID { get; private set; }

        public string Title { get; set; }

        private int developmentComplexity;
        public int DevelopmentComplexity
        {
            get => developmentComplexity;
            set
            {
                if (value <= 0 || value > 1000)
                {
                    throw new ArgumentException("Принимаем разработки, сложность которых от 1 до 1000");
                }
                developmentComplexity = value;
            }
        }

        public int DevelopmentProgress { get; set; } = 0;

        private int testingProgress;
        public int TestingProgress
        {
            get => testingProgress;
            set
            {
                testingProgress = value;
                Priority = Profit / (DevelopmentComplexity - testingProgress);
            }
        } 

        private decimal profit;
        public decimal Profit
        {
            get => profit;
            set
            {
                if (value < 100000)
                {
                    throw new ArgumentException("Принимаем разработки с вознаграждением от 100000 руб");
                }
                profit = value;
            }
        }

        public decimal Priority { get; set; }

        public Todo(int id, string title, int developmentComplexity, decimal profit)
        {
            ID = id;
            Title = title;
            Profit = profit;
            DevelopmentComplexity = developmentComplexity;
            TestingProgress = 0;
        }

        public Todo(int id, string title, int developmentComplexity, int developmentProgress, int testingProgress, decimal profit, decimal priority)
        {
            ID = id;
            Title = title;
            DevelopmentComplexity = developmentComplexity;
            DevelopmentProgress = developmentProgress;
            TestingProgress = testingProgress;
            Profit = profit;
            Priority = priority;
        }

        public int CompareTo(Todo other)
        {
            int result = 0;

            if (Priority < other.Priority)
            {
                result = -1;
            }

            if (Priority == other.Priority)
            {
                result = 0;
            }

            if (Priority > other.Priority)
            {
                result = 1;
            }
            return result;
        }

        public static bool operator >(Todo lhs, Todo rhs)
        {
            return lhs.CompareTo(rhs) == 1;
        }

        public static bool operator <(Todo lhs, Todo rhs)
        {
            return lhs.CompareTo(rhs) == -1;
        }
    }
}
