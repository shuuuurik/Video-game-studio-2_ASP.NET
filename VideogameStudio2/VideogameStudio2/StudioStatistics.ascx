<%@  Control Language="C#" AutoEventWireup="true" CodeFile="StudioStatistics.ascx.cs" Inherits="StudioStatistics" ClassName="StudioStatistics" %>

<div class="primary callout">
    <h3 class="text-center"><strong>Статистика студии</strong></h3>
    <hr />
    <div class="grid-x grid-padding-x align-center">
        <asp:Label ID="budgetLabel" Text="Бюджет" runat="server" CssClass="cell medium-3" Font-Size="16" />
        <asp:Label ID="currentExpensesLabel" Text="Расход на з/п в месяц" runat="server" CssClass="cell medium-5" Font-Size="16" />
    </div>
    <hr />
    <div class="grid-x align-center">
        <asp:Label ID="numberOfWorkersLabel" Text="Сотрудники" runat="server" CssClass="cell shrink" Font-Size="18" />
    </div>
    <br />
    <div class="grid-x grid-padding-x align-center">
        <asp:Label ID="numberOfDevelopersLabel" Text="Разработчики" runat="server" CssClass="cell medium-3" Font-Size="14" />
        <asp:Label ID="developmentProductivityLabel" Text="Продуктивность разработки" runat="server" CssClass="cell medium-4" Font-Size="14" />
        <asp:Label ID="currentDevelopmentExpensesLabel" Text="Затраты на разработку" runat="server" CssClass="cell medium-4" Font-Size="14" />
    </div>
    <br />
    <div class="grid-x grid-padding-x align-center">
        <asp:Label ID="numberOfSoftwareTestersLabel" Text="Тестировщики" runat="server" CssClass="cell medium-3" Font-Size="14" />
        <asp:Label ID="testingProductivityLabel" Text="Продуктивность тестирования" runat="server" CssClass="cell medium-4" Font-Size="14" />
        <asp:Label ID="currentTestingExpensesLabel" Text="Затраты на тестирование " runat="server" CssClass="cell medium-4" Font-Size="14" />
    </div>
    <hr />
    <div class="grid-x grid-padding-x align-center">
        <asp:Label ID="currentTodoLabel" Text="Текущая разработка" runat="server" CssClass="cell shrink" Font-Size="18" />
        <asp:Button ID="StopCurrentDevelopmentButton" Text="Отложить" runat="server" CssClass="button cell shrink"
            CausesValidation="false" OnClick="StopCurrentDevelopmentButton_Click" />
    </div>
    <br />
    <div class="grid-x grid-padding-x align-center">
        <asp:Label ID="currentTodoComplexityLabel" Text="Сложность разработки" runat="server" CssClass="cell medium-4" Font-Size="14" />
        <asp:Label ID="currentTodoProfitLabel" Text="Вознаграждение" runat="server" CssClass="cell medium-4" Font-Size="14" />
    </div>
    <br />
    <div class="grid-x grid-padding-x align-center">
        <asp:Label ID="developmentProgressLabel" Text="Прогресс разработки" runat="server" CssClass="cell medium-4" Font-Size="14" />
        <asp:Label ID="testingProgressLabel" Text="Прогресс тестирования" runat="server" CssClass="cell medium-4" Font-Size="14" />
    </div>
</div>
<div class="grid-x align-center">
    <asp:ValidationSummary ID="StudioValidationSummary" runat="server" Font-Size="16" />
</div>
<div class="grid-x align-center">
    <asp:Button ID="skipMonthButton" Text="Работать 1 месяц" runat="server" CssClass="large button" CausesValidation="false" OnClick="skipMonthButton_Click" />
</div>

