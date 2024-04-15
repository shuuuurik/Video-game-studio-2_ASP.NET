using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideogameStudio;
using System.Collections.ObjectModel;

public partial class StudioStatistics : System.Web.UI.UserControl
{
    public GameStudio studio = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // Получение сотрудников из БД
            IQueryable<Employee> employeeQuery =
                from emp in vsContext.EMPLOYEEs
                join job in vsContext.JOBs on emp.Job_ID equals job.ID
                join status in vsContext.HAPPINESSLEVELs on emp.HappinessLevel_ID equals status.ID
                orderby emp.ID
                select СreateEmpliyeeInheritor(job.Title, emp.ID, emp.FirstName, emp.LastName, emp.Age, emp.WorkExperience,
                emp.WorkedMonths, emp.Productivity, emp.Salary, status.Title);
            ObservableCollection<Employee> employeeList = new ObservableCollection<Employee>(employeeQuery.ToList());

            // Получение разработок из БД
            IQueryable<Todo> developmentQuery =
                from dev in vsContext.DEVELOPMENTs
                where dev.GameStudio_ID == null
                select new Todo(dev.ID, dev.Title, dev.DevelopmentComplexity, dev.DevelopmentProgress,
                dev.TestingProgress, dev.Profit, dev.Priority);
            TodoList developmentList = new TodoList(new ObservableCollection<Todo>(developmentQuery.ToList()));

            // Получение студии из БД
            var studioQuery =
                from studio in vsContext.GAMESTUDIOs
                select studio;
            GAMESTUDIO gameStudio = studioQuery.FirstOrDefault();

            // Создание объекта студии и заполнение данными
            studio = new GameStudio(gameStudio.ID, gameStudio.Budget, employeeList, developmentList,
                gameStudio.CurrentWorkForDevelopers, gameStudio.CurrentWorkForTesters);
            // Добавление в студию текущей разработки, если такая имеется
            int? currentDevelopmentId = gameStudio.CurrentDevelopment_ID;
            if (currentDevelopmentId != null)
            {
                IQueryable<Todo> currentDevelopmentQuery =
                    from dev in vsContext.DEVELOPMENTs
                    where dev.ID == currentDevelopmentId
                    select new Todo(dev.ID, dev.Title, dev.DevelopmentComplexity, dev.DevelopmentProgress,
                    dev.TestingProgress, dev.Profit, dev.Priority);
                Todo currentDevelopment = currentDevelopmentQuery.FirstOrDefault();
                studio.CurrentTodo = currentDevelopment;
            }
        }
        ShowStatistics();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        // Destroy Studio object
        studio.Dispose();
    }

    // Создаем объект класса Developer или SoftwareTester в зависимости от значения строковой переменной speciality
    private Employee СreateEmpliyeeInheritor(string speciality, int id, string firstName, string lastName, int age, int experience,
            int months, int productivity, decimal salary, string happiness)
    {
        if (speciality == "Разработчик")
        {
            return new Developer(id, firstName, lastName, age, experience, months, productivity, salary, happiness);
        }
        else // speciality == "Тестировщик"
        {
            return new SoftwareTester(id, firstName, lastName, age, experience, months, productivity, salary, happiness);
        }
    }

    private void ShowStatistics() // Выводим статистическую информацию студии на веб-контролах (Label)
    {
        budgetLabel.Text = string.Format("Бюджет: {0} руб", (int)studio.Budget);
        currentExpensesLabel.Text = string.Format("Расход на з/п в месяц: {0} руб", studio.CurrentExpenses);
        numberOfWorkersLabel.Text = string.Format("Сотрудники: {0}", studio.Workers.Count);
        numberOfDevelopersLabel.Text = string.Format("Разработчики: {0}", studio.NumberOfDevelopers);
        numberOfSoftwareTestersLabel.Text = string.Format("Тестировщики: {0}", studio.NumberOfSoftwareTesters);
        developmentProductivityLabel.Text = string.Format("Продуктивность разработки: {0}", studio.DevelopmentProductivity);
        testingProductivityLabel.Text = string.Format("Продуктивность тестирования: {0}", studio.TestingProductivity);
        currentDevelopmentExpensesLabel.Text = string.Format("Затраты на разработку: {0} р", studio.CurrentDevelopmentExpenses);
        currentTestingExpensesLabel.Text = string.Format("Затраты на тестирование: {0} р", studio.CurrentTestingExpenses);
        if (studio.CurrentTodo != null && studio.CurrentWorkForSowtwareTesters != 0)
        // Если в данный момент в студии ведётся разработка, отображаем это на соответствующих Label
        {
            StopCurrentDevelopmentButton.Visible = true;
            currentTodoLabel.Text = string.Format("Текущая разработка: {0}", studio.CurrentTodo.Title);
            currentTodoComplexityLabel.Visible = true;
            currentTodoComplexityLabel.Text = string.Format("Сложность разработки: {0}", studio.CurrentTodo.DevelopmentComplexity);
            developmentProgressLabel.Visible = true;
            developmentProgressLabel.Text = string.Format("Прогресс разработки: {0}%", (int)studio.GetDevelopmentProgress());
            testingProgressLabel.Visible = true;
            testingProgressLabel.Text = string.Format("Прогресс тестирования: {0}%", (int)studio.GetTestingProgress());
            currentTodoProfitLabel.Visible = true;
            currentTodoProfitLabel.Text = string.Format("Вознаграждение: {0}", studio.CurrentTodo.Profit);
        }
        else // Иначе скрываем соответствующие Label
        {
            if (!currentTodoLabel.Text.StartsWith("Разработка"))
            {
                currentTodoLabel.Text = string.Format("Разработок в данный момент не ведётся, программисты отдыхают");
            }
            StopCurrentDevelopmentButton.Visible = false;
            //currentTodoComplexityLabel.Text = string.Format("Сложность разработки:");
            //developmentProgressLabel.Text = string.Format("Прогресс разработки:");
            //testingProgressLabel.Text = string.Format("Прогресс тестирования:");
            //currentTodoProfitLabel.Text = string.Format("Вознаграждение:");
            currentTodoComplexityLabel.Visible = false;
            developmentProgressLabel.Visible = false;
            testingProgressLabel.Visible = false;
            currentTodoProfitLabel.Visible = false;
        }
    }

    protected void skipMonthButton_Click(object sender, EventArgs e)
    {
        try
        {
            studio.SimulateMonthSkipping(); // здесь нужно перехватить возможные исключения

            // если все хорошо, то сохраняем в БД изменения, которые произошли в студии
            using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
            {
                // Сохраняем изменения сотрудников в БД (некоторые могли уволиться, остальные повысили кол-во проработанных месяцев)
                var modifyEmployeeQuery =
                    from emp in vsContext.EMPLOYEEs
                    orderby emp.ID
                    select emp;
                foreach (EMPLOYEE emp in modifyEmployeeQuery)
                {
                    Employee employeeInStudio = null;
                    // Проверяем остался ли в студии сотрудник, который записан в базе
                    foreach (Employee worker in studio.Workers)
                    {
                        if (worker.ID == emp.ID)
                        {
                            employeeInStudio = worker;
                        }
                    }
                    // Если все еще работает - сохраняем изменения в БД
                    if (employeeInStudio != null)
                    {
                        emp.WorkExperience = employeeInStudio.WorkExperience;
                        emp.WorkedMonths = employeeInStudio.WorkedMonths;
                        emp.Productivity = employeeInStudio.Productivity;
                        emp.Salary = employeeInStudio.Salary;
                        if (employeeInStudio.Happiness == HappinessLevel.Happy)
                        {
                            emp.HappinessLevel_ID = 1;
                        }
                        else if (employeeInStudio.Happiness == HappinessLevel.Unhappy)
                        {
                            emp.HappinessLevel_ID = 2;
                        }
                    }
                    // Иначе удаляем сотрудника из БД
                    else
                    {
                        vsContext.EMPLOYEEs.DeleteOnSubmit(emp);
                    }
                }

                // Теперь сохраняем изменения самой студии в БД
                var modifyStudioQuery =
                    from studio in vsContext.GAMESTUDIOs
                    select studio;
                GAMESTUDIO gameStudio = modifyStudioQuery.FirstOrDefault();
                gameStudio.Budget = studio.Budget; // Изменили хранящуюся в БД информацию о бюджете
                if (studio.CurrentWorkForSowtwareTesters != 0)
                // Вносим в БД возможные изменения, связанные с текущей разработкой
                {
                    var modifyDevelopmentQuery =
                        from dev in vsContext.DEVELOPMENTs
                        where dev.ID == studio.CurrentTodo.ID
                        select dev;
                    DEVELOPMENT development = modifyDevelopmentQuery.FirstOrDefault();
                    // Помечаем в БД, что данная разработка начата студией, и сохраняем изменения текущей разработки в БД
                    gameStudio.CurrentDevelopment_ID = studio.CurrentTodo.ID;
                    gameStudio.CurrentWorkForDevelopers = studio.CurrentWorkForDevelopers;
                    gameStudio.CurrentWorkForTesters = studio.CurrentWorkForSowtwareTesters;
                    development.GameStudio_ID = studio.ID;
                }
                else // Текущей разработки у студии нет, так как она только что завершила предыдущую
                {
                    gameStudio.CurrentDevelopment_ID = null;
                    gameStudio.CurrentWorkForDevelopers = 0;
                    gameStudio.CurrentWorkForTesters = 0;
                    // Ищем завершённую разработку
                    var deleteDevelopmentQuery =
                        from dev in vsContext.DEVELOPMENTs
                        where dev.ID == studio.CurrentTodo.ID
                        select dev;
                    DEVELOPMENT developmentToDelete = deleteDevelopmentQuery.FirstOrDefault();
                    currentTodoLabel.Text = "Разработка " + developmentToDelete.Title + " завершена!";
                    vsContext.DEVELOPMENTs.DeleteOnSubmit(developmentToDelete);
                }
                try // Сохраняем в БД все изменения
                {
                    vsContext.SubmitChanges();
                }
                catch (Exception)
                {
                    // Обработать исключение
                }
            }
            ShowStatistics();
        }
        catch (NotSupportedException ex)
        {
            if (ex.Message == "Add Todos before calling TakeMostPriorityItem") // Если при нажатии на кнопку нет разработок
            {
                Page.ModelState.AddModelError(string.Empty, "Программисты бездельничают! Для продолжения добавьте новую задачу.");
            }
            // Если при нажатии на кнопку недостаточно сотрудников
            else if (ex.Message == "В студии должен работать хотя бы 1 разработчик и 1 тестировщик.")
            {
                Page.ModelState.AddModelError(string.Empty, "Недостаточно работников! " + ex.Message);
            }
        }
    }

    protected void StopCurrentDevelopmentButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Откладываем текущую разработку в студии и записываем ее в переменную stoppedDevelopmentInStudio, чтобы зафиксировать изменения в БД
            Todo stoppedDevelopmentInStudio = studio.StopCurrentDevelopment();
            using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
            {
                // Сохраняем изменение студии в БД (убираем текущую разработку, обнуляем текущую работу для сотрудников)
                var modifyStudioQuery =
                    from studio in vsContext.GAMESTUDIOs
                    select studio;
                GAMESTUDIO gameStudio = modifyStudioQuery.FirstOrDefault();
                gameStudio.CurrentDevelopment_ID = null;
                gameStudio.CurrentWorkForDevelopers = 0;
                gameStudio.CurrentWorkForTesters = 0;

                // Сохраняем изменение отложенной разработки в БД
                // (убираем привязку к студии, записываем текущий прогресс разработки и новый приоритет)
                var modifyDevelopmentQuery =
                        from dev in vsContext.DEVELOPMENTs
                        where dev.ID == stoppedDevelopmentInStudio.ID
                        select dev;
                DEVELOPMENT stopedDevelopmentInDB = modifyDevelopmentQuery.FirstOrDefault();
                stopedDevelopmentInDB.GameStudio_ID = null;
                stopedDevelopmentInDB.DevelopmentProgress = stoppedDevelopmentInStudio.DevelopmentProgress;
                stopedDevelopmentInDB.TestingProgress = stoppedDevelopmentInStudio.TestingProgress;
                stopedDevelopmentInDB.Priority = stoppedDevelopmentInStudio.Priority;

                try // Сохраняем в БД все изменения
                {
                    vsContext.SubmitChanges();
                }
                catch (Exception)
                {
                    // Обработать исключение
                }
            }
            ShowStatistics();
        }
        catch (NotSupportedException ex)
        {
            Page.ModelState.AddModelError(string.Empty, ex.Message);
        }
    }
}