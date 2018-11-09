Imports CapaNegocio
Imports CapaEntidad
Imports CapaDatos
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text

Public Class WebCanasta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            cargarcarrito()
        End If
    End Sub
    Sub cargarcarrito()
        Dim GV As New GridView
        GvwCarrito.DataSource = Session("Canasta")
        GvwCarrito.DataBind()
        Call Button1_Click(Button1, Nothing)
        txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Protected Sub btnGenerarDocumento_Click(sender As Object, e As EventArgs) Handles btnGenerarDocumento.Click
        Dim oDocumento As New DocumentosCN
        Dim errorCodCliente As Boolean
        errorCodCliente = False
        If (txtFecha.Text = "") Then
            MsgBox("Por favor ingrese la fecha")
        Else
            oDocumento.Agregar(New CapaEntidad.Documentos(txtFecha.Text))
            Dim oCoger As New DocumentosCN
            txtCoddocumento.Text = oCoger.CogerCoddocumento()
        End If
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Dim i As Integer
        Dim prec As Double
        Dim cod As String
        Dim cant As Integer
        Dim obj As DSVideo = CType(Session("Canasta"), DSVideo)
        Dim cont = 0
        Dim errorDoc As Boolean
        errorDoc = False
        For i = 0 To GvwCarrito.Rows.Count - 1
            cod = (GvwCarrito.Rows(i).Cells(0).Text)
            prec = Double.Parse(GvwCarrito.Rows(i).Cells(1).Text)
            cant = CType(GvwCarrito.Rows(i).Cells(2).FindControl("TextBox1"), TextBox).Text

            If (txtCoddocumento.Text <> "") Then
                Dim oDocumentoDetalle As New DetalleDocumentosCN
                Dim ok As Boolean
                Dim documentoDetalleInfo As New CapaEntidad.DetalleDocumentos(txtCoddocumento.Text, cod, prec, cant)
                'Crear nuevo documento detalle
                ok = oDocumentoDetalle.Agregar(documentoDetalleInfo)
                cont = cont + 1
            Else
                errorDoc = True
            End If

        Next
        If (errorDoc = True) Then
            MsgBox("Te olvidaste de ingresa el codigo del documento")
        End If
        If (cont = GvwCarrito.Rows.Count) Then
            MsgBox("Registro Guardado")
            Response.Redirect("WebCarrito.aspx")
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer
        Dim prec As Double
        Dim cod As String
        Dim cant As Integer
        Dim obj As DSVideo = CType(Session("Canasta"), DSVideo)
        For i = 0 To GvwCarrito.Rows.Count - 1
            cod = (GvwCarrito.Rows(i).Cells(0).Text)
            prec = Double.Parse(GvwCarrito.Rows(i).Cells(1).Text)
            cant = CType(GvwCarrito.Rows(i).Cells(2).FindControl("TextBox1"), TextBox).Text
            'Actualiza la canasta
            For Each objDR In obj.Videos.Rows
                If objDR("codvideo") = cod Then
                    objDR("cantidad") = cant
                    Exit For
                End If
            Next
        Next
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("WebCarrito.aspx")
    End Sub

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        enviar_correo()
    End Sub

    Sub enviar_correo()
        Dim from As String
        from = txtEnvia.Text
        Dim password As String
        password = txtpassword.Text
        Dim recibe As String
        recibe = txtRecibe.Text

        Dim mensaje As String
        mensaje = "Hola, mensaje de prueba"

        Dim email As New Email
        email.enviar(from, password, recibe, mensaje)
    End Sub

    Protected Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        Dim pdfTable As New PdfPTable(GvwCarrito.HeaderRow.Cells.Count)

        For Each headercell As TableCell In GvwCarrito.HeaderRow.Cells
            Dim pdfCell As New PdfPCell(New Phrase(headercell.Text))
            pdfTable.AddCell(pdfCell)

        Next



        For Each gridviewrow As GridViewRow In GvwCarrito.Rows
            For Each tablecell As TableCell In gridviewrow.Cells
                Dim pdfCell As New PdfPCell(New Phrase(tablecell.Text))
                pdfTable.AddCell(pdfCell)

            Next
        Next

        Dim pdfdocument As New Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 10.0F)
        PdfWriter.GetInstance(pdfdocument, Response.OutputStream)

        pdfdocument.Open()
        pdfdocument.Add(pdfTable)
        pdfdocument.Close()

        Response.ContentType = "application/pdf"
        Response.AppendHeader("content-disposition", "attachment;filename=Compra.pdf")
        Response.Write(pdfdocument)
        Response.Flush()
        Response.End()




    End Sub
End Class