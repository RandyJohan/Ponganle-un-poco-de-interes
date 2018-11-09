Imports CapaNegocio
Imports CapaEntidad
Imports CapaDatos
Public Class WebCarrito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim ds As DataSet
            ds = VideosCN.Instancia.ListarTodos
            DataList1.DataSource = VideosCN.Instancia.ListarTodos
            DataList1.DataBind()
        End If
    End Sub

    Private Sub DataList1_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles DataList1.ItemCommand
        Dim cod, des As String
        Dim pre As Double
        If e.CommandName = "cmdSeleccionar" Then
            DataList1.SelectedIndex = e.Item.ItemIndex
            cod = CType(DataList1.SelectedItem.FindControl("codvideoLabel"), Label).Text
            des = CType(DataList1.SelectedItem.FindControl("tituloLabel"), Label).Text
            pre = CType(DataList1.SelectedItem.FindControl("precioLabel"), Label).Text
            AgregarIdvideo(cod, des, pre)
        End If
    End Sub

    Public Function Video() As DSVideo
        Dim obj As DSVideo = CType(Session("Canasta"), DSVideo)
        If obj Is Nothing Then
            obj = New DSVideo()
            Session("Canasta") = obj
        End If
        Return obj
    End Function

    Public Sub AgregarIdvideo(ByVal cod As String, ByVal des As String, ByVal pre As Double)
        Dim obj As DSVideo = Me.Video
        Dim fila As DSVideo.VideosRow = obj.Videos.NewVideosRow()

        fila.codvideo = cod
        fila.titulo = des
        fila.precio = pre
        fila.cantidad = 1

        If (Me.Video.Tables("Videos").Rows.Find(cod) IsNot Nothing) Then
            MsgBox("Ya has añadido ese elemento a la Canasta")
        Else
            obj.Videos.Rows.Add(fila)
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("WebCanasta.aspx")
    End Sub


End Class