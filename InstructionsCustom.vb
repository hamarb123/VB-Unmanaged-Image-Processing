#If DESKTOP = True Then
Imports System.Drawing
#End If

Public Class InstructionsCustom
    Inherits Tuple(Of ColourRectsCustom(), Size)

    Public Sub New(Colors As ColourRectsCustom(), Size As Size)
        MyBase.New(Colors, Size)
    End Sub

    Public ReadOnly Property Colors As ColourRectsCustom()
        Get
            Return Me.Item1
        End Get
    End Property

    Public ReadOnly Property Size As Size
        Get
            Return Me.Item2
        End Get
    End Property
End Class
