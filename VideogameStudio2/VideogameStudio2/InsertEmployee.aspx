<%@ Page Title="Новый сотрудник" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InsertEmployee.aspx.cs" Inherits="InsertEmployee" %>

<%@ Register Src="~/EmployeeAddingForm.ascx" TagPrefix="uc1" TagName="EmployeeAddingForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:EmployeeAddingForm runat="server" ID="EmployeeAddingForm" />
</asp:Content>

