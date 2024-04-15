using System;
using System.Linq;

public partial class Developments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // Получение студии из БД
            var studioQuery =
                from studio in vsContext.GAMESTUDIOs
                select studio;
            GAMESTUDIO gameStudio = studioQuery.FirstOrDefault();
            if (gameStudio.CurrentDevelopment_ID == null) // Студия в данный момент ничего не разрабатывает
            {
                StartDevelopment.Visible = true;
            }
            else // Студия в данный момент ведёт какую-то разработку
            {
                StartDevelopment.Visible = false;
            }
        }
    }
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        TitleQueryTextBox.Text = "";
    }
}