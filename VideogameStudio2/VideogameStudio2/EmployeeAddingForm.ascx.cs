using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeAddingForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void EmployeeCancelButton_Click(object sender, EventArgs e)
    {
        // Redirect to home page
        Response.Redirect("~/Default.aspx");
    }

    protected void EmployeeWorkExperienceCorrectValueValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (int.Parse(EmployeeAgeTextBox.Text) - int.Parse(EmployeeWorkExperienceTextBox.Text) < 18)
        {
            args.IsValid = false;
        }
    }

    protected void EmployeeInsertButton_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // Получение студии из БД
            var studioQuery =
                from studio in vsContext.GAMESTUDIOs
                select studio;
            GAMESTUDIO gameStudio = studioQuery.FirstOrDefault();

            // Получение всех текущих зарплат сотрудников из БД
            var salariesQuery =
                from emp in vsContext.EMPLOYEEs
                select emp.Salary;
            decimal currentExpenses = 0; // Суммарные ежемесячные расходы студии на данный момент
            foreach (decimal empSalary in salariesQuery)
            {
                currentExpenses += empSalary;
            }

            int workExperience = int.Parse(EmployeeWorkExperienceTextBox.Text);
            int productivity, salary, job = int.Parse(EmployeeJobDropDownList.SelectedValue);
            if (job == 1)
            {
                productivity = 10 + (workExperience * 10);
                salary = 50000 + (workExperience * 20000);
            }
            else // if (job == 2)
            {
                productivity = 20 + (workExperience * 20);
                salary = 30000 + (workExperience * 20000);
            }

            currentExpenses += salary;
            if (gameStudio.Budget < currentExpenses)
            {
                Page.ModelState.AddModelError(string.Empty, "Бюджета студии недостаточно! Вы не можете нанять данного сотрудника. " +
                    "Не хватает " + (currentExpenses - gameStudio.Budget).ToString()  + " руб.");
                return;
            }

            EMPLOYEE employee = new EMPLOYEE
            {
                FirstName = EmployeeFirstNameTextBox.Text,
                LastName = EmployeeLastNameTextBox.Text,
                Age = int.Parse(EmployeeAgeTextBox.Text),
                WorkExperience = workExperience,
                WorkedMonths = 0,
                Job_ID = job,
                Productivity = productivity,
                Salary = salary,
                HappinessLevel_ID = 1
            };

            vsContext.EMPLOYEEs.InsertOnSubmit(employee);

            vsContext.SubmitChanges();

            // Перенаправление на страницу сотрудников
            Response.Redirect("~/Employees.aspx");
        }
    }
}