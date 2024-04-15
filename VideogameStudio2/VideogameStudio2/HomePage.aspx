<%@ Page Title="Atahaio Studio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron text-center">
        <h1>Atahaio Studio</h1>
        <p class="lead">Разрабатываем и издаем игры по всему миру</p>
        <p><a href="StudioPage" class="btn btn-primary btn-lg">Перейти на страницу студии &raquo;</a></p>
    </div>
    <div class="grid-x grid-padding-x">
        <img src="images/war_thunder.jpg" class="cell shrink" style="height:200px" alt="Изображение игры 1" />
        <img src="images/crossout.jpg" class="cell shrink" style="height:200px" alt="Изображение игры 2" />
        <img src="images/game_image.jpg" class="cell shrink" style="height:200px" alt="Изображение игры 3" />
    </div>

</asp:Content>
