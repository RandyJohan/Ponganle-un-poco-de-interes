Imports CapaDatos
Imports CapaEntidad
Public Class DetalleDocumentosCN
    Private Shared ReadOnly _instancia As New DetalleDocumentosCN
    Public Shared ReadOnly Property Instancia() As DetalleDocumentosCN
        Get
            Return _instancia
        End Get
    End Property
    Public Function Agregar(ByVal detalledocumento As DetalleDocumentos) As Boolean
        Return DetalleDocumentoDA.Instancia.Agregar(detalledocumento)
    End Function
End Class

