
Public Class DetalleDocumentos
    Private _iddocumento As Integer
    Public Property iddocumento() As Integer
        Get
            Return _iddocumento
        End Get
        Set(ByVal value As Integer)
            _iddocumento = value
        End Set
    End Property
    Private _codvideo As Integer
    Public Property codvideo() As Integer
        Get
            Return _codvideo
        End Get
        Set(ByVal value As Integer)
            _codvideo = value
        End Set
    End Property
    Private _precio As Decimal
    Public Property precio() As Decimal
        Get
            Return _precio
        End Get
        Set(ByVal value As Decimal)
            _precio = value
        End Set
    End Property
    Private _cantidad As Integer
    Public Property cantidad() As Integer
        Get
            Return _cantidad
        End Get
        Set(ByVal value As Integer)
            _cantidad = value
        End Set
    End Property

    Public Sub New(ByVal iddocumento As Integer, ByVal codvideo As Integer, ByVal precio As Decimal, ByVal cantidad As Integer)
        _iddocumento = iddocumento
        _codvideo = codvideo
        _precio = precio
        _cantidad = cantidad
    End Sub
End Class

