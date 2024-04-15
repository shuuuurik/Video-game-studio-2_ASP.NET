<%@ Page Title="Студия" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudioPage.aspx.cs" Inherits="StudioPage" %>

<%@ Register Src="~/StudioStatistics.ascx" TagPrefix="uc1" TagName="StudioStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="text-center">Страница студии</h2>
    <hr />
    <div class="grid-container">
        <div class="grid-x grid-margin-x align-center align-bottom">
            <div class="cell medium-3">
                <div class="grid-x grid-padding-x align-middle">
                    <asp:Label Text="Увеличить бюджет:" runat="server" Font-Bold="true" Font-Size="12" CssClass="cell shrink"></asp:Label>
                    <asp:TextBox ID="BudgetIncreaseTextBox" CssClass="cell shrink" runat="server" MaxLength="7" />
                    &nbsp;<asp:RequiredFieldValidator ID="BudgetIncreaseRequiredFieldValidator" runat="server"
                        ControlToValidate="BudgetIncreaseTextBox" Text="*" Display="Dynamic"
                        ErrorMessage="Сумма для увеличения бюджета должна быть указана."></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="BudgetIncreaseRangeValidator"
                        ControlToValidate="BudgetIncreaseTextBox" runat="server" MinimumValue="1"
                        MaximumValue="9999999" ErrorMessage="Сумма для увеличения бюджета должна быть положительной."
                        Text="*" Type="Integer"></asp:RangeValidator>
                </div>
            </div>
            <asp:Button ID="IncreaseBudgetButton" Text="Инвестировать" runat="server" CssClass="button cell medium-2" OnClick="IncreaseBudgetButton_Click" />
        </div>

        <uc1:StudioStatistics runat="server" ID="StudioStatistics" />
    </div>
</asp:Content>

