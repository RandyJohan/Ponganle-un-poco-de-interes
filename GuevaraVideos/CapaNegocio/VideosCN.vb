Imports CapaDatos
Imports CapaEntidad

Public Class VideosCN
    Public Sub New()
    End Sub
    Private Shared ReadOnly _instancia As New VideosCN
    Public Shared ReadOnly Property Instancia() As VideosCN
        Get
            Return _instancia
        End Get
    End Property
    Public Function ListarTodos() As DataSet
        Return VideoDA.Instancia.ListarTodos
    End Function

End Class