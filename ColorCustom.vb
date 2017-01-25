Public NotInheritable Class ColourCustom
    Inherits Tuple(Of Byte, Byte, Byte)

    Public Sub New(Red As Byte, Green As Byte, Blue As Byte)
        MyBase.New(Red, Green, Blue)
    End Sub

    Public ReadOnly Property Red As Byte
        Get
            Return Me.Item1
        End Get
    End Property

    Public ReadOnly Property Green As Byte
        Get
            Return Me.Item2
        End Get
    End Property

    Public ReadOnly Property Blue As Byte
        Get
            Return Me.Item3
        End Get
    End Property

    Public ReadOnly Property Alpha As Byte
        Get
            Return 255
        End Get
    End Property
End Class
