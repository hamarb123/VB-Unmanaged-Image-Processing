#If DESKTOP = True Then
Imports System.Drawing
#End If

Public Class ColourRectsCustom
    Inherits Tuple(Of Rectangle(), ColourCustom)

    Public Sub New(Rects As Rectangle(), Color As ColourCustom)
        MyBase.New(Rects, Color)
    End Sub

    Public ReadOnly Property Rects As Rectangle()
        Get
            Return Me.Item1
        End Get
    End Property

    Public ReadOnly Property Color As ColourCustom
        Get
            Return Me.Item2
        End Get
    End Property
End Class
