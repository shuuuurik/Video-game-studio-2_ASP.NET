using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StartDevelopment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void StartSelectedDevelopmentButton_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        string developmentTitle = TitleDevelopmentToStartTextBox.Text;
        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // Получение студии из БД
            var studioQuery =
                from studio in vsContext.GAMESTUDIOs
                select studio;
            GAMESTUDIO gameStudio = studioQuery.FirstOrDefault();

            // Получение разработки с указанным названием из БД
            var developmentQuery =
                from dev in vsContext.DEVELOPMENTs
                where dev.Title == developmentTitle
                select dev;
            DEVELOPMENT development = developmentQuery.FirstOrDefault();

            // Сохраняем в БД, что указанная разработка начата студией
            development.GameStudio_ID = gameStudio.ID;
            gameStudio.CurrentDevelopment_ID = development.ID;
            gameStudio.CurrentWorkForDevelopers = development.DevelopmentComplexity - development.DevelopmentProgress;
            gameStudio.CurrentWorkForTesters = development.DevelopmentComplexity - development.TestingProgress;

            try // Сохраняем в БД все изменения
            {
                vsContext.SubmitChanges();
            }
            catch (Exception)
            {
                // Обработать исключение
            }
            // Перенаправление на страницу студии
            Response.Redirect("~/StudioPage.aspx");
        }
    }

    protected void DevelopmentToStartValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // В базе нет разработки с таким названием
            args.IsValid = ((from development in vsContext.DEVELOPMENTs
                             where development.Title == args.Value
                             select development)
                                    .Any());
        }
    }
}