Imports System.Net
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Net.Mail

Public Class Email
    Dim m As New MailMessage
    Dim smtp As New SmtpClient
    Sub enviar(ByVal from As String, ByVal password As String, ByVal recibir As String, ByVal mensaje As String, ByVal ruta As String)
        Try
            m.From = New MailAddress(from)
            m.To.Add(New MailAddress(recibir))
            m.Body = mensaje
            m.IsBodyHtml = True
            Dim archivo As New Attachment(ruta)
            m.Attachments.Add(archivo)
            smtp.Host = "smtp.gmail.com"
            smtp.Port = 587
            smtp.Credentials = New NetworkCredential(from, password)
            smtp.EnableSsl = True
            smtp.Send(m)

        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
    End Sub
End Class
