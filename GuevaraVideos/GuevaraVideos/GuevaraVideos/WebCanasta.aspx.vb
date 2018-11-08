Imports CapaNegocio
Imports CapaEntidad
Imports CapaDatos
Imports iTextSharp
Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf
Imports System.Windows.Forms

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

    Sub generarPDf(document As Document)

    End Sub
    Private Sub To_pdf()
        Dim doc As Document = New Document(PageSize.A4.Rotate(), 10, 10, 10, 10)
        Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()
        saveFileDialog1.InitialDirectory = "C:"
        saveFileDialog1.Title = "Guardar Reporte"
        saveFileDialog1.DefaultExt = "pdf"
        saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True
        Dim filename As String = ""

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            filename = saveFileDialog1.FileName
        End If

        If filename.Trim() <> "" Then
            Dim file As FileStream = New FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
            PdfWriter.GetInstance(doc, file)
            doc.Open()
            Dim remito As String = "Autorizo: OSVALDO SANTIAGO ESTRADA"
            Dim envio As String = "Fecha:" & DateTime.Now.ToString()
            Dim chunk As Chunk = New Chunk("Reporte de General Usuarios", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD))
            doc.Add(New Paragraph(chunk))
            doc.Add(New Paragraph("                       "))
            doc.Add(New Paragraph("                       "))
            doc.Add(New Paragraph("------------------------------------------------------------------------------------------"))
            doc.Add(New Paragraph("Lagos de moreno Jalisco"))
            doc.Add(New Paragraph(remito))
            doc.Add(New Paragraph(envio))
            doc.Add(New Paragraph("------------------------------------------------------------------------------------------"))
            doc.Add(New Paragraph("                       "))
            doc.Add(New Paragraph("                       "))
            doc.Add(New Paragraph("                       "))
            GenerarDocumento(doc)
            doc.AddCreationDate()
            doc.Add(New Paragraph("______________________________________________", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD)))
            doc.Add(New Paragraph("Firma", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD)))
            doc.Close()
            Process.Start(filename)
        End If
    End Sub
    Public Function GetTamañoColumnas(ByVal dg As GridView) As Single()
        Dim values As Single() = New Single(dg.Columns.Count - 1) {}

        For i As Integer = 0 To dg.Columns.Count - 1
            values(i) = CSng(dg.Columns(i).ControlStyle.Width.ToString)
        Next

        Return values
    End Function

    Public Sub GenerarDocumento(ByVal document As Document)
        Dim i, j As Integer
        Dim datatable As PdfPTable = New PdfPTable(GvwCarrito.Columns.Count)
        datatable.DefaultCell.Padding = 3
        Dim headerwidths As Single() = GetTamañoColumnas(GvwCarrito)
        datatable.SetWidths(headerwidths)
        datatable.WidthPercentage = 100
        datatable.DefaultCell.BorderWidth = 2
        datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER

        For i = 0 To GvwCarrito.Columns.Count - 1
            datatable.AddCell(GvwCarrito.Columns(i).HeaderText)
        Next

        datatable.HeaderRows = 1
        datatable.DefaultCell.BorderWidth = 1

        For i = 0 To GvwCarrito.Rows.Count - 1

            For j = 0 To GvwCarrito.Columns.Count - 1

                If GvwCarrito.Rows(i).Cells(j).Text IsNot Nothing Then
                    datatable.AddCell(New Phrase(GvwCarrito.Rows(i).Cells(j).Text.ToString()))
                End If
            Next

            datatable.CompleteRow()
        Next

        document.Add(datatable)
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
            'cant = CType(GvwCarrito.Rows(i).Cells(2).FindControl("TextBox1"), TextBox).Text
            cant = GvwCarrito.Rows(i).Cells(2).Text
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
        To_pdf()
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

            ' cant = GvwCarrito.Rows(i).Cells(2).Text
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
End Class