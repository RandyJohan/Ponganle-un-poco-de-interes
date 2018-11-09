Imports CapaDatos
Imports CapaEntidad
Public Class DocumentosCN
    Public Sub New()
    End Sub
    Private Shared ReadOnly _instancia As New DocumentosCN
    Public Shared ReadOnly Property Instancia() As DocumentosCN
        Get
            Return _instancia
        End Get
    End Property
    Public Function Agregar(ByVal documento As Documentos) As Boolean
        Return DocumentoDA.Instancia.Agregar(documento)
    End Function
    Public Function CogerCoddocumento() As Integer
        Return DocumentoDA.Instancia.CogerCoddocumento()
    End Function

End Class
