
Imports CapaEntidad
    Imports System.Data.SqlClient
    Public Class DetalleDocumentoDA
    Private Shared ReadOnly _instancia As New DetalleDocumentoDA
    Public Shared ReadOnly Property Instancia() As DetalleDocumentoDA
        Get
            Return _instancia
        End Get
    End Property
    Public Function ListarTodos() As List(Of DetalleDocumentos)
        Dim Coleccion As New List(Of DetalleDocumentos)
        Try
            Dim cnn As New SqlConnection(Conexion.Instancia.cadenaconexion)
            cnn.Open()
            Dim sqlcmd As New SqlCommand("listar_detalle", cnn)
            sqlcmd.CommandType = CommandType.StoredProcedure
            Dim PaTable As SqlDataReader = sqlcmd.ExecuteReader

            While PaTable.Read
                Coleccion.Add(New DetalleDocumentos(PaTable.Item(0), PaTable.Item(1), PaTable.Item(2), PaTable.Item(3)))
            End While
            cnn.Close()
            cnn.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Coleccion
    End Function
    Public Function Agregar(ByVal DetalleDocumento As DetalleDocumentos) As Boolean
        Try
            Dim cnn As New SqlConnection(Conexion.Instancia.cadenaconexion)
            cnn.Open()
            Dim Sqlcmd As New SqlCommand("agregar_detalle", cnn)
            Sqlcmd.CommandType = CommandType.StoredProcedure
            Sqlcmd.Parameters.Add("@iddocumento", SqlDbType.Int).Value = DetalleDocumento.iddocumento
            Sqlcmd.Parameters.Add("@codvideo", SqlDbType.Int).Value = DetalleDocumento.codvideo
            Sqlcmd.Parameters.Add("@precio", SqlDbType.Int).Value = DetalleDocumento.precio
            Sqlcmd.Parameters.Add("@cantidad", SqlDbType.Decimal).Value = DetalleDocumento.cantidad

            Sqlcmd.ExecuteNonQuery()
            cnn.Close()
            cnn.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True
    End Function
End Class

