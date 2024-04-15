<%@ Page Title="Сотрудники" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="text-center">Список сотрудников</h2>
    <h4>Разработчики</h4>
    <asp:LinqDataSource ID="DevelopersLinqDataSource" runat="server" ContextTypeName="VideogameStudioDataContext"
        EntityTypeName=""
        TableName="EMPLOYEEs" EnableDelete="True" EnableInsert="True" EnableUpdate="True" Where="Job_ID == @Job_ID">
        <WhereParameters>
            <asp:Parameter DefaultValue="1" Name="Job_ID" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:ListView ID="DevelopersListView" runat="server" DataSourceID="DevelopersLinqDataSource" DataKeyNames="ID">
        <AlternatingItemTemplate>
            <tr style="background-color: #FAFAD2; color: #284775;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
                <td>
                    <asp:Label ID="AgeLabel" runat="server" Text='<%# Eval("Age") %>' />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("JOB.Title") %>' />
                </td>
                <td>
                    <asp:Label ID="WorkExperienceLabel" runat="server" Text='<%# Eval("WorkExperience") %>' />
                </td>
                <td>
                    <asp:Label ID="WorkedMonthsLabel" runat="server" Text='<%# Eval("WorkedMonths") %>' />
                </td>
                <td>
                    <asp:Label ID="ProductivityLabel" runat="server" Text='<%# Eval("Productivity") %>' />
                </td>
                <td>
                    <asp:Label ID="SalaryLabel" runat="server" Text='<%# Eval("Salary") %>' />
                </td>
                <td>
                    <asp:Label ID="HAPPINESSLEVELLabel" runat="server" Text='<%# Eval("HAPPINESSLEVEL.Title") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color: #FFCC66; color: #000080;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="AgeTextBox" runat="server" Text='<%# Bind("Age") %>' />
                </td>
                <td>
                    <asp:Label ID="JOBLabel" runat="server" Text='<%# Eval("JOB.Title") %>' />
                </td>
                <td>
                    <asp:TextBox ID="WorkExperienceTextBox" runat="server" Text='<%# Bind("WorkExperience") %>' />
                </td>
                <td>
                    <asp:TextBox ID="WorkedMonthsTextBox" runat="server" Text='<%# Bind("WorkedMonths") %>' />
                </td>
                <td>
                    <asp:TextBox ID="ProductivityTextBox" runat="server" Text='<%# Bind("Productivity") %>' />
                </td>
                <td>
                    <asp:TextBox ID="SalaryTextBox" runat="server" Text='<%# Bind("Salary") %>' />
                </td>
                <td>
                    <asp:TextBox ID="HAPPINESSLEVELTextBox" runat="server" Text='<%# Bind("HAPPINESSLEVEL") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="AgeTextBox" runat="server" Text='<%# Bind("Age") %>' />
                </td>
                <td>
                    <asp:Label ID="JOBLabel" runat="server" Text='<%# Eval("JOB.Title") %>' />
                </td>
                <td>
                    <asp:TextBox ID="WorkExperienceTextBox" runat="server" Text='<%# Bind("WorkExperience") %>' />
                </td>
                <td>
                    <asp:TextBox ID="WorkedMonthsTextBox" runat="server" Text='<%# Bind("WorkedMonths") %>' />
                </td>
                <td>
                    <asp:TextBox ID="ProductivityTextBox" runat="server" Text='<%# Bind("Productivity") %>' />
                </td>
                <td>
                    <asp:TextBox ID="SalaryTextBox" runat="server" Text='<%# Bind("Salary") %>' />
                </td>
                <td>
                    <asp:TextBox ID="HAPPINESSLEVELTextBox" runat="server" Text='<%# Bind("HAPPINESSLEVEL") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #FFFBD6; color: #333333;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
                <td>
                    <asp:Label ID="AgeLabel" runat="server" Text='<%# Eval("Age") %>' />
                </td>
                <td>
                    <asp:Label ID="JOBLabel" runat="server" Text='<%# Eval("JOB.Title") %>' />
                </td>
                <td>
                    <asp:Label ID="WorkExperienceLabel" runat="server" CssClass="text-center" Text='<%# Eval("WorkExperience") %>' />
                </td>
                <td>
                    <asp:Label ID="WorkedMonthsLabel" runat="server" Text='<%# Eval("WorkedMonths") %>' />
                </td>
                <td>
                    <asp:Label ID="ProductivityLabel" runat="server" Text='<%# Eval("Productivity") %>' />
                </td>
                <td>
                    <asp:Label ID="SalaryLabel" runat="server" Text='<%# Eval("Salary") %>' />
                </td>
                <td>
                    <asp:Label ID="HAPPINESSLEVELLabel" runat="server" Text='<%# Eval("HAPPINESSLEVEL.Title") %>' />
                </td>

            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #FFFBD6; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">Имя</th>
                                <th runat="server">Фамилия</th>
                                <th runat="server">Возраст</th>
                                <th runat="server">Специальность</th>
                                <th runat="server">Стаж работы</th>
                                <th runat="server">Проработано месяцев</th>
                                <th runat="server">Продуктивность</th>
                                <th runat="server">Зарплата, руб</th>
                                <th runat="server">Уровень счастья</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #FFCC66; font-family: Verdana, Arial, Helvetica, sans-serif; color: #333333;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #FFCC66; font-weight: bold; color: #000080;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                </td>
                <td>
                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
                </td>
                <td>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
                </td>
                <td>
                    <asp:Label ID="AgeLabel" runat="server" Text='<%# Eval("Age") %>' />
                </td>
                <td>
                    <asp:Label ID="JOBLabel" runat="server" Text='<%# Eval("JOB.Title") %>' />
                </td>
                <td>
                    <asp:Label ID="WorkExperienceLabel" runat="server" Text='<%# Eval("WorkExperience") %>' />
                </td>
                <td>
                    <asp:Label ID="WorkedMonthsLabel" runat="server" Text='<%# Eval("WorkedMonths") %>' />
                </td>
                <td>
                    <asp:Label ID="ProductivityLabel" runat="server" Text='<%# Eval("Productivity") %>' />
                </td>
                <td>
                    <asp:Label ID="SalaryLabel" runat="server" Text='<%# Eval("Salary") %>' />
                </td>
                <td>
                    <asp:Label ID="HAPPINESSLEVELLabel" runat="server" Text='<%# Eval("HAPPINESSLEVEL") %>' />
                </td>

            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <br />
    <h4>Тестировщики</h4>
    <asp:LinqDataSource ID="TestersLinqDataSource" runat="server" ContextTypeName="VideogameStudioDataContext" EntityTypeName="" TableName="EMPLOYEEs" Where="Job_ID == @Job_ID" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
        <WhereParameters>
            <asp:Parameter DefaultValue="2" Name="Job_ID" Type="Int32" />
        </WhereParameters>
    </asp:LinqDataSource>
    <asp:GridView ID="TestersGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="TestersLinqDataSource" Width="1085px" AllowSorting="True" ForeColor="Black" HorizontalAlign="Center" PageSize="5">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Blue">
                <ControlStyle ForeColor="Blue"></ControlStyle>
            </asp:CommandField>
            <asp:BoundField DataField="FirstName" HeaderText="Имя" SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Фамилия" SortExpression="LastName" />
            <asp:BoundField DataField="Age" HeaderText="Возраст" SortExpression="Age" />
            <asp:BoundField DataField="JOB.Title" HeaderText="Специальность" SortExpression="JOB.Title" />
            <asp:BoundField DataField="WorkExperience" HeaderText="Стаж работы" SortExpression="WorkExperience" />
            <asp:BoundField DataField="WorkedMonths" HeaderText="Проработано месяцев" SortExpression="WorkedMonths" />
            <asp:BoundField DataField="Productivity" HeaderText="Продуктивность" SortExpression="Productivity" />
            <asp:BoundField DataField="Salary" HeaderText="Зарплата, руб" SortExpression="Salary" />
            <asp:BoundField DataField="HAPPINESSLEVEL.Title" HeaderText="Уровень счастья" SortExpression="HAPPINESSLEVEL.Title" />
        </Columns>
    </asp:GridView>
</asp:Content>

