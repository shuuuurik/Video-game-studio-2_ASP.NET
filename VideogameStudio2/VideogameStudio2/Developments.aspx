<%@ Page Title="Разработки" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Developments.aspx.cs" Inherits="Developments" %>

<%@ Register Src="~/StartDevelopment.ascx" TagPrefix="uc1" TagName="StartDevelopment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="text-center">Список разработок</h2>
    <div class="grid-container">
        <div class="grid-x grid-margin-x align-center align-bottom">
            <label class="cell shrink">
                <strong>Поиск по названию:</strong>
                <asp:TextBox ID="TitleQueryTextBox" CssClass="cell shrink" runat="server" MaxLength="30" />
            </label>
            <asp:Button ID="FetchButton" Text="Искать" runat="server" CssClass="button success cell medium-2" CausesValidation="false" />
            <asp:Button ID="ClearButton" Text="Сбросить" runat="server" CssClass="button alert cell medium-2" CausesValidation="false"
                OnClick="ClearButton_Click" />
        </div>
    </div>

    <asp:LinqDataSource ID="DevelopmentsLinqDataSource" runat="server" ContextTypeName="VideogameStudioDataContext" EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="DEVELOPMENTs"
        Where='(@searchTerm = "" OR Title.Contains(@searchTerm)) AND GameStudio_ID = null' OrderBy="Priority desc">
        <WhereParameters>
            <asp:ControlParameter Name="searchTerm" ControlID="TitleQueryTextBox" DefaultValue="" ConvertEmptyStringToNull="false" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:GridView ID="DevelopmentsGridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="DevelopmentsLinqDataSource" ForeColor="Black" HorizontalAlign="Center">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Blue">
                <ControlStyle ForeColor="Blue"></ControlStyle>
            </asp:CommandField>
            <asp:BoundField DataField="Title" HeaderText="Название" SortExpression="Title" />
            <asp:BoundField DataField="Priority" HeaderText="Приоритет" SortExpression="Priority" />
            <asp:BoundField DataField="DevelopmentComplexity" HeaderText="Сложность разработки" SortExpression="DevelopmentComplexity" />
            <asp:BoundField DataField="DevelopmentProgress" HeaderText="Прогресс разработки" SortExpression="DevelopmentProgress" />
            <asp:BoundField DataField="TestingProgress" HeaderText="Прогресс тестирования" SortExpression="TestingProgress" />
            <asp:BoundField DataField="Profit" HeaderText="Вознаграждение, руб" SortExpression="Profit" />
        </Columns>
    </asp:GridView>

    <uc1:StartDevelopment runat="server" id="StartDevelopment" />
</asp:Content>
