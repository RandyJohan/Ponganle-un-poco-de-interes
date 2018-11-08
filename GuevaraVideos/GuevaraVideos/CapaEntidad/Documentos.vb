Public Class Documentos
    Private _fechadocumento As Date
    Public Property fechadocumento() As Date
        Get
            Return _fechadocumento
        End Get
        Set(ByVal value As Date)
            _fechadocumento = value
        End Set
    End Property

    Public Sub New(ByVal fechadocumento As Date)
        _fechadocumento = fechadocumento
    End Sub
End Class
