using System;
using System.IO;
using System.Collections.ObjectModel;

namespace VideogameStudio
{
    public class GameStudio : IDisposable, ICloneable
    {
        public int ID { get; private set; }
        public bool IsDisposed { get; private set; } = false;
        public TextWriter LoggingFileWriter { get; private set; }
        public string LoggingFileName { get; private set; } = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..\AppData\currentLists.txt");

        public ObservableCollection<Employee> Workers { get; private set; }

        public TodoList Todos { get; private set; }

        public decimal Budget { get; set; }

        public int NumberOfDevelopers { get; private set; } = 0;

        public int NumberOfSoftwareTesters { get; private set; } = 0;

        public decimal CurrentExpenses { get; private set; }

        public decimal CurrentDevelopmentExpenses { get; private set; }

        public decimal CurrentTestingExpenses { get; private set; }

        public Todo CurrentTodo { get; set; }

        public int CurrentWorkForDevelopers { get; set; }

        public int CurrentWorkForSowtwareTesters { get; set; }

        public int DevelopmentProductivity { get; private set; }

        public int TestingProductivity { get; private set; }

        public GameStudio(int id, decimal budget, ObservableCollection<Employee> workers, TodoList todos,
            int currentWorkForDevelopers, int currentWorkForSowtwareTesters)
        {
            ID = id;
            Budget = budget;
            Workers = workers;
            Todos = todos;
            CurrentWorkForDevelopers = currentWorkForDevelopers;
            CurrentWorkForSowtwareTesters = currentWorkForSowtwareTesters;
            foreach(Employee employee in workers)
            {
                if (employee.Speciality == Specilization.Developer)
                {
                    NumberOfDevelopers++;
                }
            }
            NumberOfSoftwareTesters = Workers.Count - NumberOfDevelopers;
            CalculateCurrentProductivity();
            CalculateCurrentExpenses();
        }

        public void SimulateMonthSkipping()
        {
            if (NumberOfDevelopers == 0 || NumberOfSoftwareTesters == 0)
            {
                throw new NotSupportedException("В студии должен работать хотя бы 1 разработчик и 1 тестировщик.");
            }

            if (CurrentTodo == null || CurrentWorkForSowtwareTesters == 0)
            {
                StartNewDevelopment();
            }

            CurrentWorkForDevelopers = Math.Max(0, CurrentWorkForDevelopers - DevelopmentProductivity);
            CurrentWorkForSowtwareTesters = TestingProductivity <= DevelopmentProductivity
                ? Math.Max(0, CurrentWorkForSowtwareTesters - TestingProductivity)
                : Math.Max(0, CurrentWorkForSowtwareTesters - DevelopmentProductivity); //чтобы тестирование не перегнало разработку

            if (CurrentWorkForSowtwareTesters == 0)
            {
                Budget += CurrentTodo.Profit;
            }

            if (Budget >= CurrentExpenses)
            {
                Budget -= CurrentExpenses;
                foreach (Employee employee in Workers)
                {
                    employee.SimulateMonthWorking(employee.Salary);
                }
            }
            else
            {
                decimal incompleteSalary = Budget / Workers.Count;
                Budget = 0;
                for (int i = Workers.Count - 1; i >= 0; --i)
                {
                    Employee employee = Workers[i];
                    if (employee.Happiness == HappinessLevel.Unhappy && employee.Salary > incompleteSalary)
                    {
                        Workers.Remove(employee);
                        if (employee.Speciality == Specilization.Developer)
                        {
                            NumberOfDevelopers--;
                        } 
                        else if (employee.Speciality == Specilization.SoftwareTester)
                        {
                            NumberOfSoftwareTesters--;
                        }
                    }
                    else
                    {
                        employee.SimulateMonthWorking(incompleteSalary);
                    }
                }
            }
            CalculateCurrentProductivity();
            CalculateCurrentExpenses();
        }

        public void StartNewDevelopment()
        {
            CurrentTodo = Todos.TakeMostPriorityItem();
            CurrentWorkForDevelopers = CurrentTodo.DevelopmentComplexity - CurrentTodo.DevelopmentProgress;
            CurrentWorkForSowtwareTesters = CurrentTodo.DevelopmentComplexity - CurrentTodo.TestingProgress;
        }
        public void StartSelectedDevelopment(Todo todo)
        {
            CurrentTodo = todo;
            Todos.Remove(todo);
            CurrentWorkForDevelopers = CurrentTodo.DevelopmentComplexity - CurrentTodo.DevelopmentProgress;
            CurrentWorkForSowtwareTesters = CurrentTodo.DevelopmentComplexity - CurrentTodo.TestingProgress;
        }
        public Todo StopCurrentDevelopment()
        {
            if (CurrentWorkForSowtwareTesters == 0)
            {
                throw new NotSupportedException("В данный момент ничего не разрабатывается.");
            }
            Todo savedDevelopment = new Todo(CurrentTodo.ID, CurrentTodo.Title, CurrentTodo.DevelopmentComplexity, CurrentTodo.Profit);
            savedDevelopment.DevelopmentProgress = CurrentTodo.DevelopmentComplexity - CurrentWorkForDevelopers;
            savedDevelopment.TestingProgress = CurrentTodo.DevelopmentComplexity - CurrentWorkForSowtwareTesters;
            Todos.Add(savedDevelopment);
            CurrentTodo = null;
            return savedDevelopment;
        }

        public void CalculateCurrentProductivity()
        {
            DevelopmentProductivity = TestingProductivity = 0;
            foreach (Employee employee in Workers)
            {
                if (employee.Speciality == Specilization.Developer)
                {
                    DevelopmentProductivity += employee.Productivity;
                }
                else
                {
                    TestingProductivity += employee.Productivity;
                }
            }
        }
        public void CalculateCurrentExpenses()
        {
            CurrentExpenses = CurrentDevelopmentExpenses = CurrentTestingExpenses = 0;
            foreach (Employee employee in Workers)
            {
                CurrentExpenses += employee.Salary;
                if (employee.Speciality == Specilization.Developer)
                {
                    CurrentDevelopmentExpenses += employee.Salary;
                }
                else
                {
                    CurrentTestingExpenses += employee.Salary;
                }
            }
        }

        public double GetTestingProgress()
        {
            return (CurrentTodo.DevelopmentComplexity - CurrentWorkForSowtwareTesters) /
                (double)CurrentTodo.DevelopmentComplexity * 100;
        }

        public double GetDevelopmentProgress()
        {
            return (CurrentTodo.DevelopmentComplexity - CurrentWorkForDevelopers) /
                (double)CurrentTodo.DevelopmentComplexity * 100;
        }

        public void AddEmployee(Employee employee)
        {
            Workers.Add(employee);
            if (employee.Speciality == Specilization.Developer)
            {
                NumberOfDevelopers++;
                DevelopmentProductivity += employee.Productivity;
                CurrentDevelopmentExpenses += employee.Salary;
                CurrentExpenses += employee.Salary;
            }
            else
            {
                NumberOfSoftwareTesters++;
                TestingProductivity += employee.Productivity;
                CurrentTestingExpenses += employee.Salary;
                CurrentExpenses += employee.Salary;
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            Workers.Remove(employee);
            if (employee.Speciality == Specilization.Developer)
            {
                NumberOfDevelopers--;
                DevelopmentProductivity -= employee.Productivity;
                CurrentDevelopmentExpenses -= employee.Salary;
                CurrentExpenses -= employee.Salary;
            }
            else
            {
                NumberOfSoftwareTesters--;
                TestingProductivity -= employee.Productivity;
                CurrentTestingExpenses -= employee.Salary;
                CurrentExpenses -= employee.Salary;
            }
        }

        public void AddTodo(Todo todo)
        {
            Todos.Add(todo);
        }
        public void DeleteTodo(string title)
        {
            Todos.Remove(Todos[title]);
        }

        /*public static GameStudio operator +(GameStudio studio, Employee employee)
        {
            AddEmployee(studio, employee);
            return studio;
        }
        public static GameStudio operator +(Employee employee, GameStudio studio)
        {
            return studio + employee;
        }*/

        /*public static GameStudio operator +(GameStudio studio, Todo todo)
        {
            //GameStudio newStudio = new GameStudio(studio);
            studio.Todos.Add(todo);
            return studio;
        }
        public static GameStudio operator +(Todo todo, GameStudio studio)
        {
            return studio + todo;
        }*/

        public void StartLogging()
        {
            if (LoggingFileWriter == null)
            {
                // Check if the logging file exists - if not create it.
                if (!File.Exists(LoggingFileName))
                {
                    LoggingFileWriter = File.CreateText(LoggingFileName);
                    //loggingFileWriter.WriteLine("Log file status checked - Created");
                }
                /*else
                {
                    loggingFileWriter = new StreamWriter(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Opened");
                }*/
            }
            /*else
            {
                loggingFileWriter.WriteLine("Log file status checked - Already open");
            }*/
            WriteDataToFile();
        }

        public void WriteDataToFile()
        {
            // Проверяем, не был ли объект уже уничтожен
            if (IsDisposed)
                throw new ObjectDisposedException("Студия уже уничтожена. Перезагрузите приложение.");

            if (LoggingFileWriter != null)
                LoggingFileWriter.Close();

            LoggingFileWriter = new StreamWriter(LoggingFileName);
            LoggingFileWriter.WriteLine("Текущий список сотрудников:");
            foreach (Employee worker in Workers)
            {
                LoggingFileWriter.WriteLine("Имя: {0},  Профессия: {1}, Опыт: {2}, Проработано: {3} мес, Продуктивность: {4}, З/п: {5} руб, Уровень счастья: {6}.\n",
                    worker.FirstName, worker.Speciality, worker.WorkExperience, worker.WorkedMonths, worker.Productivity, worker.Salary, worker.Happiness);
            }
            if (CurrentTodo != null)
            {
                LoggingFileWriter.WriteLine("\n---------------------------------------------------------------");
                LoggingFileWriter.WriteLine("Текущая разработка:");
                LoggingFileWriter.WriteLine("Название: {0},  Сложность: {1},  Вознаграждение: {2} руб, Приоритет: {3}\n ",
                       CurrentTodo.Title, CurrentTodo.DevelopmentComplexity, CurrentTodo.Profit, CurrentTodo.Priority, CurrentTodo.DevelopmentProgress, CurrentTodo.TestingProgress);
            }

            LoggingFileWriter.WriteLine("\n---------------------------------------------------------------");
            LoggingFileWriter.WriteLine("Текущая очередь разработок:");
            foreach (Todo todo in Todos)
            {
                LoggingFileWriter.WriteLine("Название: {0},  Сложность: {1},  Вознаграждение: {2} руб, Приоритет: {3}, Прогресс разработки: {4}, Прогресс тестирования: {5}\n ",
                    todo.Title, todo.DevelopmentComplexity, todo.Profit, todo.Priority, todo.DevelopmentProgress, todo.TestingProgress);
            }
            LoggingFileWriter.Flush();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~GameStudio()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (!IsDisposed)
            {
                if (isDisposing)
                {
                    // Освобождаем ресурсы только если Dispose
                    // был вызван приложением явным образом
                    Workers.Clear();
                    Todos.Clear();
                    LoggingFileName = null;
                    CurrentTodo = null;
                }
                // Всегда освобождаем неуправляемые ресурсы
                if (LoggingFileWriter != null)
                {
                    LoggingFileWriter.Close();
                }
                LoggingFileWriter = null;
            }
            // Указываем, что объект уже уничтожен,
            // а ресурсы освобождены
            IsDisposed = true;
        }

        public object Clone()
        {
            ObservableCollection<Employee> workers = new ObservableCollection<Employee>();
            foreach (Employee employee in Workers)
            {
                if (employee.Speciality == Specilization.Developer)
                {
                    workers.Add(new Developer(employee.FirstName, employee.LastName, employee.Age, employee.WorkExperience));
                }
                else
                {
                    workers.Add(new SoftwareTester(employee.FirstName, employee.LastName, employee.Age, employee.WorkExperience));
                }
            }

            TodoList todos = new TodoList();
            foreach (Todo todo in Todos)
            {
                todos.Add(new Todo(todo.ID, todo.Title, todo.DevelopmentComplexity, todo.Profit));
            }

            return new GameStudio(ID, Budget, workers, todos, CurrentWorkForDevelopers, CurrentWorkForSowtwareTesters);
        }
    }
}
