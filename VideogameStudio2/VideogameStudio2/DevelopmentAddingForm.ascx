<%@  Control Language="C#" AutoEventWireup="true" CodeFile="DevelopmentAddingForm.ascx.cs" Inherits="DevelopmentAddingForm" ClassName="DevelopmentAddingForm" %>

<h2 class="text-center">Добавление новой разработки</h2>
<div class="grid-x align-center">
    <div class="cell medium-5 medium-offset-1">
        <strong>
            <asp:Label ID="DevelopmentTitleLabel" runat="server" Text="Название:"></asp:Label></strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="DevelopmentTitleTextBox" runat="server" MaxLength="50"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="DevelopmentTitleRequiredFieldValidator" runat="server"
                ControlToValidate="DevelopmentTitleTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Название разработки должно быть заполнено."></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="DevelopmentTitleUniqueValidator"
                ControlToValidate="DevelopmentTitleTextBox" runat="server"
                ErrorMessage="Разработка с таким названием уже существует." Text="*"
                OnServerValidate="DevelopmentTitleValidator_ServerValidate">
            </asp:CustomValidator>
        </div>

        <strong>
            <asp:Label ID="DevelopmentComplexityLabel" runat="server" Text="Сложность:"></asp:Label></strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="DevelopmentComplexityTextBox" runat="server" MaxLength="4"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="DevelopmentComplexityRequiredFieldValidator" runat="server"
                ControlToValidate="DevelopmentComplexityTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Сложность разработки должна быть заполнена."></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="DevelopmentComplexityRangeValidator"
                ControlToValidate="DevelopmentComplexityTextBox" runat="server" MinimumValue="1"
                MaximumValue="1000" ErrorMessage="Принимаем разработки, сложность которых от 1 до 1000."
                Text="*" Type="Integer"></asp:RangeValidator>
        </div>

        <strong>
            <asp:Label ID="DevelopmentProfitLabel" runat="server" Text="Вознаграждение, руб:"></asp:Label></strong>
        <div class="grid-x grid-padding-x align-middle">
            <asp:TextBox ID="DevelopmentProfitTextBox" runat="server" MaxLength="9"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="DevelopmentProfitRequiredFieldValidator" runat="server"
                ControlToValidate="DevelopmentProfitTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Вознаграждение разработки должно быть заполнено."></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="DevelopmentProfitRangeValidator" runat="server"
                ControlToValidate="DevelopmentProfitTextBox" MinimumValue="100000" MaximumValue="999999999" Text="*" Type="Integer"
                ErrorMessage="Принимаем разработки с вознаграждением от 100000 руб."></asp:RangeValidator>
        </div>

        <asp:ValidationSummary ID="DevelopmentValidationSummary" runat="server"></asp:ValidationSummary>
        <asp:Button ID="DevelopmentInsertButton" runat="server" Text="Добавить" CssClass="button success" OnClick="DevelopmentInsertButton_Click" />
        &nbsp;<asp:Button ID="DevelopmentCancelButton" runat="server" Text="Cancel" CssClass="button alert"
            CausesValidation="false" OnClick="DevelopmentCancelButton_Click" />
    </div>
</div>

