Imports CapaEntidad
Imports System.Data.SqlClient
Public Class DocumentoDA
    Private Shared ReadOnly _instancia As New DocumentoDA
    Public Shared ReadOnly Property Instancia() As DocumentoDA
        Get
            Return _instancia
        End Get
    End Property
    Public Function Agregar(ByVal Documento As Documentos) As Boolean
        Try
            Dim cnn As New SqlConnection(Conexion.Instancia.cadenaconexion)
            cnn.Open()
            Dim Sqlcmd As New SqlCommand("agregar_documento", cnn)
            Sqlcmd.CommandType = CommandType.StoredProcedure
            Sqlcmd.Parameters.Add("@fechadocumento", SqlDbType.Date).Value = Documento.fechadocumento

            Sqlcmd.ExecuteNonQuery()
            cnn.Close()
            cnn.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True
    End Function
    Public Function CogerCoddocumento() As Integer
        Dim Coleccion As New Integer
        Try
            Dim cnn As New SqlConnection(Conexion.Instancia.cadenaconexion)
            cnn.Open()
            Dim sqlcmd As New SqlCommand("coger_coddocumento", cnn)
            sqlcmd.CommandType = CommandType.StoredProcedure
            Dim PaTable As SqlDataReader = sqlcmd.ExecuteReader

            While PaTable.Read
                Coleccion = PaTable.Item(0)
            End While
            cnn.Close()
            cnn.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Coleccion
    End Function
End Class
