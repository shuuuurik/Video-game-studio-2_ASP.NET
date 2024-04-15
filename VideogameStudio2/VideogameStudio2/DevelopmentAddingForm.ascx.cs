using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
public partial class DevelopmentAddingForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DevelopmentCancelButton_Click(object sender, EventArgs e)
    {
        // Redirect to home page
        Response.Redirect("~/Default.aspx");
    }

    protected void DevelopmentInsertButton_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            int developmentComplexity = int.Parse(DevelopmentComplexityTextBox.Text);
            Decimal profit = Decimal.Parse(DevelopmentProfitTextBox.Text);
            DEVELOPMENT development = new DEVELOPMENT
            {
                Title = DevelopmentTitleTextBox.Text,
                DevelopmentComplexity = developmentComplexity,
                DevelopmentProgress = 0,
                TestingProgress = 0,
                Profit = profit,
                Priority = Math.Round((profit / developmentComplexity), 2),
            };

            vsContext.DEVELOPMENTs.InsertOnSubmit(development);

            vsContext.SubmitChanges();

            // Перенаправление на страницу разработок
            Response.Redirect("~/Developments.aspx");
        }
    }

    protected void DevelopmentTitleValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        using (VideogameStudioDataContext vsContext = new VideogameStudioDataContext())
        {
            // В базе уже есть разработка с таким названием
            args.IsValid = !((from development in vsContext.DEVELOPMENTs
                              where development.Title == args.Value
                              select development)
                                    .Any());
        }
    }
}