Imports System.Data.SqlClient

Public Class VideoDA
    Private Shared ReadOnly _instancia As New VideoDA
    Public Shared ReadOnly Property Instancia() As VideoDA
        Get
            Return _instancia
        End Get
    End Property
    Public Function ListarTodos() As DataSet
        Dim ds As New DataSet
        Try
            Dim cnn As New SqlConnection(Conexion.Instancia.cadenaconexion)
            cnn.Open()
            Dim da As New SqlDataAdapter("listar_video", cnn)
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.Fill(ds, "Videos")
            cnn.Close()
            cnn.Dispose()
            da.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return ds
    End Function

End Class
