<%@  Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeAddingForm.ascx.cs" Inherits="EmployeeAddingForm" ClassName="EmployeeAddingForm" %>

<h2 class="text-center">Добавление нового сотрудника</h2>
<div class="grid-x align-center">
    <div class="cell medium-5 medium-offset-1">
        <strong>
            <asp:Label ID="EmployeeFirstNameLabel" runat="server" Text="Имя:"></asp:Label>
        </strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="EmployeeFirstNameTextBox" runat="server" MaxLength="30"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="EmployeeFirstNameRequiredFieldValidator" runat="server"
                ControlToValidate="EmployeeFirstNameTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Имя сотрудника должно быть заполнено."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmployeeFirstNameRegularExpressionValidator"
                ControlToValidate="EmployeeFirstNameTextBox" runat="server" ErrorMessage="Имя сотрудника должно начинаться с большой буквы и состоять хотя бы из двух символов."
                Text="*" ValidationExpression="[A-ZА-ЯЁ][A-zА-яЁё']+">
            </asp:RegularExpressionValidator>
        </div>

        <strong>
            <asp:Label ID="EmployeeLastNameLabel" runat="server" Text="Фамилия:"></asp:Label>
        </strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="EmployeeLastNameTextBox" runat="server" MaxLength="30"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="EmployeeLastNameRequiredFieldValidator" runat="server"
                ControlToValidate="EmployeeLastNameTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Фамилия сотрудника должна быть заполнена."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmployeeLastNameRegularExpressionValidator"
                ControlToValidate="EmployeeLastNameTextBox" runat="server" ErrorMessage="Фамилия сотрудника должна начинаться с большой буквы и состоять хотя бы из двух символов."
                Text="*" ValidationExpression="[A-ZА-ЯЁ][A-zА-яЁё']+">
            </asp:RegularExpressionValidator>
        </div>

        <strong>
            <asp:Label ID="EmployeeAgeLabel" runat="server" Text="Возраст:"></asp:Label>
        </strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="EmployeeAgeTextBox" runat="server" MaxLength="2"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="EmployeeAgeRequiredFieldValidator" runat="server"
                ControlToValidate="EmployeeAgeTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Возраст сотрудника должен быть заполнен."></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="EmployeeAgeRangeValidator"
                ControlToValidate="EmployeeAgeTextBox" runat="server" MinimumValue="20"
                MaximumValue="80" ErrorMessage="Принимаем в студию только сотрудников в возрасте от 20 до 80 лет."
                Text="*" Type="Integer">
            </asp:RangeValidator>
        </div>

        <strong>
            <asp:Label ID="EmployeeJobLabel" runat="server" Text="Специальность:"></asp:Label>
        </strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:DropDownList ID="EmployeeJobDropDownList" runat="server" DataSourceID="JobLinqDataSource"
                DataValueField="ID" DataTextField="Title">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="EmployeeJobRequiredFieldValidator" ControlToValidate="EmployeeJobDropDownList"
                runat="server" ErrorMessage="Специальность должна быть выбрана." Text="*">
            </asp:RequiredFieldValidator>
        </div>

        <strong>
            <asp:Label ID="EmployeeWorkExperienceLabel" runat="server" Text="Стаж работы по специальности:"></asp:Label>
        </strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="EmployeeWorkExperienceTextBox" runat="server" MaxLength="2"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="EmployeeWorkExperienceRequiredFieldValidator" runat="server"
                ControlToValidate="EmployeeWorkExperienceTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Стаж работы сотрудника должен быть заполнен."></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="EmployeeWorkExperienceRangeValidator"
                ControlToValidate="EmployeeWorkExperienceTextBox" runat="server" MinimumValue="0"
                MaximumValue="80" ErrorMessage="Стаж работы сотрудника должен быть от 0 до 80 лет."
                Text="*" Type="Integer">
            </asp:RangeValidator>
            <asp:CustomValidator ID="EmployeeWorkExperienceCorrectValueValidator"
                ControlToValidate="EmployeeWorkExperienceTextBox" runat="server"
                ErrorMessage="Стаж работы нанимаемого сотрудника не соответствует возрасту (считается только опыт работы с 18 лет)." Text="*"
                OnServerValidate="EmployeeWorkExperienceCorrectValueValidator_ServerValidate">
            </asp:CustomValidator>
        </div>

        <asp:ValidationSummary ID="EmployeeValidationSummary" runat="server"></asp:ValidationSummary>
        <asp:Button ID="EmployeeInsertButton" runat="server" Text="Добавить" CssClass="button success" OnClick="EmployeeInsertButton_Click" />
        &nbsp;<asp:Button ID="EmployeeCancelButton" runat="server" Text="Cancel" CssClass="button alert"
            CausesValidation="false" OnClick="EmployeeCancelButton_Click" />
        <asp:LinqDataSource ID="JobLinqDataSource" runat="server" ContextTypeName="VideogameStudioDataContext" EntityTypeName="" OrderBy="ID" Select="new (ID, Title)" TableName="JOBs">
        </asp:LinqDataSource>
    </div>
</div>



