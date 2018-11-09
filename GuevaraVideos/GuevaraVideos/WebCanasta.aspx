<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PaginaMaestraExamen.Master" CodeBehind="WebCanasta.aspx.vb" Inherits="GuevaraVideos.WebCanasta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="form1" runat="server">
        <asp:GridView ID="GvwCarrito" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="codvideo" HeaderText="Codigo" />
                <asp:BoundField DataField="precio" HeaderText="Precio" />
                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cantidad") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSubtotal" runat="server" Text='<%# Bind("cantidad") * Bind("precio") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label>
                    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                    <asp:Label ID="CodDocumento" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtCoddocumento" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Actualizar Datos" />
                    <asp:Button ID="Button2" runat="server" Text="Continuar Comprando" />
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar Compra" />

                    <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar pdf" />

                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

                    <br />
                    <br />
                    <asp:Label ID="lblRecibe" runat="server" Text="To: "></asp:Label>
                    <asp:TextBox ID="txtRecibe" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar correo" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>

</asp:Content>
