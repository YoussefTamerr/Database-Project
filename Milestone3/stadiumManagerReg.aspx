﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StadiumManagerReg.aspx.cs" Inherits="Milestone3.stadiumManagerReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link rel="stylesheet" type="text/css" href="style.css"/>
        <title>Register</title>
    </head>
    <body>
        <form id="stadiumManagerBody" runat="server">
            <div id="smID" runat="server" class="form registerFrm">
                <asp:Label CssClass="labels" runat="server" Text="Register"></asp:Label>
                <div class="labelClass">
                    <asp:Label runat="server" Text="Name"></asp:Label>
                    <asp:TextBox ID="manName" runat="server"></asp:TextBox>
                </div>
                <div class="labelClass">
                    <asp:Label runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="usernameMan" runat="server"></asp:TextBox>
                </div>
                <div class="labelClass">
                    <asp:Label runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="passwordMan" TextMode="Password" runat="server"></asp:TextBox>
                </div>
                <div class="labelClass">
                    <asp:Label runat="server" Text="Stadium Name"></asp:Label>
                    <asp:TextBox ID="stadiumNameMan" runat="server"></asp:TextBox>
                </div>
                <asp:Button ID="ButtonMan" CssClass="btn" runat="server" Text="Register" OnClick="registerSM" />
            </div>
        </form>
    </body>
</html>
