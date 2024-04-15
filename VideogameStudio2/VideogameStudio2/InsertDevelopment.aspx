<%@ Page Title="Новая разработка" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InsertDevelopment.aspx.cs" Inherits="InsertDevelopment" %>

<%@ Register Src="~/DevelopmentAddingForm.ascx" TagPrefix="uc1" TagName="DevelopmentAddingForm" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:DevelopmentAddingForm runat="server" ID="DevelopmentAddingForm" />
</asp:Content>

