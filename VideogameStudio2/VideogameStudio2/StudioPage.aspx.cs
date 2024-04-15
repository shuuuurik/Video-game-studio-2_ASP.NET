using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class StudioPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void IncreaseBudgetButton_Click(object sender, EventArgs e)
    {
        StudioStatistics.studio.Budget += int.Parse(BudgetIncreaseTextBox.Text);
        ((Label)StudioStatistics.FindControl("budgetLabel")).Text = string.Format("Бюджет: {0} руб", (int)StudioStatistics.studio.Budget);
        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // Сохраняем изменение бюджета студии в БД
            var modifyStudioQuery =
                from studio in vsContext.GAMESTUDIOs
                select studio;
            GAMESTUDIO gameStudio = modifyStudioQuery.FirstOrDefault();
            gameStudio.Budget = StudioStatistics.studio.Budget;
            try // Сохраняем в БД все изменения
            {
                vsContext.SubmitChanges();
            }
            catch (Exception)
            {
                // Обработать исключение
            }
        }  
    }
}