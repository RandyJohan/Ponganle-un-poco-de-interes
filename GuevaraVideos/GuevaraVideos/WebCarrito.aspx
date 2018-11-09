<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMaestraExamen.Master" CodeBehind="WebCarrito.aspx.vb" Inherits="GuevaraVideos.WebCarrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">

            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="descategoria" DataValueField="codcategoria">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GuevaraVideos.My.MySettings.BD_GuevaraConnectionString %>" SelectCommand="SELECT [descategoria], [codcategoria] FROM [Categorias]"></asp:SqlDataSource>

            <asp:DataList ID="DataList1" runat="server">
                <ItemTemplate>
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" Height="68px" Width="117px" ImageUrl='<%# "~/imagen/" + Eval("foto") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="codvideoLabel" runat="server" Text='<%# Eval("codvideo") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="tituloLabel" runat="server" Text='<%# Eval("titulo") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="codcategoriaLabel" runat="server" Text='<%# Eval("codcategoria") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="fechaLabel" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            
                            <td>
                                <asp:Label ID="precioLabel" runat="server" Text='<%# Eval("precio") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Añadir al Carrito de Compras" CommandName="cmdSeleccionar" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>

            <asp:Button ID="Button2" runat="server" Text="Ver Canasta" />

            </form>
</asp:Content>
