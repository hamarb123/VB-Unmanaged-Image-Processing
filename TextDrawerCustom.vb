#If DESKTOP = True Then
Imports StringAlignment = System.Drawing.StringAlignment
Imports Bitmap = System.Drawing.Bitmap
Imports Graphics = System.Drawing.Graphics
Imports TextRenderingHint = System.Drawing.Text.TextRenderingHint
Imports StringFormat = System.Drawing.StringFormat
Imports Color = System.Drawing.Color
Imports RectangleF = System.Drawing.RectangleF
Imports Brushes = System.Drawing.Brushes
Imports Rectangle = System.Drawing.Rectangle
Imports Point = System.Drawing.Point
Imports Size = System.Drawing.Size
Imports ContentAlignment = System.Drawing.ContentAlignment
#End If

Public Module TextDrawerCustom
    Private Characters As Dictionary(Of Tuple(Of FontCustom, Char), InstructionsCustom)

    Public Sub DrawTextToImageCustom(ByRef ImageCustom As ImageCustom, Text As String, Font As FontCustom, Alignment As ContentAlignment, Rect As Rectangle)
        Dim origin As Point
        Select Case Alignment
            Case ContentAlignment.TopLeft
                origin = New Point(Rect.X, Rect.Y)
            Case ContentAlignment.TopCenter
                origin = New Point(Rect.X + Rect.Width / 2, Rect.Y)
            Case ContentAlignment.TopRight
                origin = New Point(Rect.X + Rect.Width, Rect.Y)
            Case ContentAlignment.MiddleLeft
                origin = New Point(Rect.X, Rect.Y + Rect.Height / 2)
            Case ContentAlignment.MiddleCenter
                origin = New Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2)
            Case ContentAlignment.MiddleRight
                origin = New Point(Rect.X + Rect.Width, Rect.Y + Rect.Height / 2)
            Case ContentAlignment.BottomLeft
                origin = New Point(Rect.X, Rect.Y + Rect.Height)
            Case ContentAlignment.BottomCenter
                origin = New Point(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height)
            Case ContentAlignment.BottomRight
                origin = New Point(Rect.X + Rect.Width, Rect.Y + Rect.Height)
        End Select
        Dim Size = Rect.Width / Text.Length
        For x As Integer = 0 To Text.Length - 1
            Dim pt = New Point() With {
                .Y = Rect.Y
            }

            If Alignment And ContentAlignment.BottomLeft = ContentAlignment.BottomLeft Or Alignment And ContentAlignment.MiddleLeft _
                = ContentAlignment.MiddleLeft Or Alignment And ContentAlignment.TopLeft = ContentAlignment.TopLeft Then
                pt.X = Rect.X + Size * x
            End If
            If Alignment And ContentAlignment.BottomCenter = ContentAlignment.BottomCenter Or Alignment And ContentAlignment.MiddleCenter _
                = ContentAlignment.MiddleCenter Or Alignment And ContentAlignment.TopCenter = ContentAlignment.TopCenter Then
                pt.X = Rect.X + Rect.Width / 2 + Size * (x - ((Text.Length - 1) / 2))
            End If
            If Alignment And ContentAlignment.BottomRight = ContentAlignment.BottomRight Or Alignment And ContentAlignment.MiddleRight _
                = ContentAlignment.MiddleRight Or Alignment And ContentAlignment.TopRight = ContentAlignment.TopRight Then
                pt.X = Rect.Right - Size * (Text.Length - x)
            End If
            ImageCustom.DrawInstructions(New Rectangle(pt, New Size(Size, Rect.Height)), GetCharacter(Text(x), Font))
        Next
    End Sub

    Public Function GetCharacter(Character As Char, Font As FontCustom) As InstructionsCustom
        Dim chr = New Tuple(Of FontCustom, Char)(Font, Character)
        Dim Size = Font.Size.Value / 72 * 96
1:      If Not Characters.ContainsKey(New Tuple(Of FontCustom, Char)(Font, Character)) Then
            Dim b = New Bitmap(CInt(Math.Ceiling(Size)), CInt(Math.Ceiling(Size)))
            Dim g = Graphics.FromImage(b)
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit
            g.Clear(Color.Transparent)
            Dim stringFormat As New StringFormat() With {
                        .Alignment = StringAlignment.Center,
                        .LineAlignment = StringAlignment.Center
                    }
            Dim SizeRem = 1 - (Size Mod 1)
            g.DrawString(Character, Font.ToFont(), Brushes.Black, New RectangleF(SizeRem, SizeRem, Size, Size), stringFormat)
            Dim ret = GetAllStepsFromImage(b)
            Characters.Add(chr, ret)
            GoTo 1
        Else
            Return Characters(chr)
        End If
    End Function
End Module
