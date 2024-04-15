<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StartDevelopment.ascx.cs" Inherits="StartDevelopment" ClassName="StartDevelopment" %>

<hr />
<div class="grid-x align-center">
    <asp:ValidationSummary ID="DevelopmentToStartValidationSummary" runat="server"></asp:ValidationSummary>
</div>
<div class="grid-x grid-margin-x align-center align-bottom">
    <div class="cell medium-3">
        <div class="grid-x grid-padding-x align-middle">
            <asp:Label ID="TitleDevelopmentToStartLabel" Text="Название разработки:" runat="server" Font-Bold="true" Font-Size="12" CssClass="cell shrink"></asp:Label>
            <asp:TextBox ID="TitleDevelopmentToStartTextBox" CssClass="cell shrink" runat="server" MaxLength="30" />
            &nbsp;<asp:RequiredFieldValidator ID="TitleDevelopmentToStartRequiredFieldValidator" runat="server"
                ControlToValidate="TitleDevelopmentToStartTextBox" Text="*" Display="Dynamic"
                ErrorMessage="Название разработки, которую хотим начать, должно быть указано."></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="DevelopmentToStartFetchValidator"
                ControlToValidate="TitleDevelopmentToStartTextBox" runat="server"
                ErrorMessage="Разработки с таким названием не существует." Text="*"
                OnServerValidate="DevelopmentToStartValidator_ServerValidate">
            </asp:CustomValidator>
        </div>
    </div>
    <asp:Button ID="StartSelectedDevelopmentButton" Text="Начать выбранную разработку" runat="server"
        CssClass="button cell shrink" OnClick="StartSelectedDevelopmentButton_Click" />
</div>

