#If DESKTOP = True Then
Imports Bitmap = System.Drawing.Bitmap
Imports Point = System.Drawing.Point
Imports Rectangle = System.Drawing.Rectangle
Imports Size = System.Drawing.Size
#End If

Public Module ImageToDrawInstructions
    Public Function GetAllStepsFromImage(Bitmap As Bitmap) As InstructionsCustom
        Dim Colors As New List(Of ColourCustom)
        Dim Points As New List(Of List(Of Point))
        For x As Integer = 0 To Bitmap.Width - 1
            For y As Integer = 0 To Bitmap.Height - 1
                Dim color1 = Bitmap.GetPixel(x, y)
                If color1.A = 255 Then
                    Dim color = New ColourCustom(color1.R, color1.G, color1.B)
                    If Not Colors.Contains(color) Then
                        Colors.Add(color)
                        Points.Add(New List(Of Point))
                    End If
                    Points(Colors.IndexOf(color)).Add(New Point(x, y))
                End If
            Next
        Next
        Dim ColorRects As New List(Of ColourRectsCustom)
        For z As Integer = 0 To Colors.Count - 1
            Dim matrix(,) = New Integer(Bitmap.Height - 1, Bitmap.Width - 1) {}
            For x As Integer = 0 To Bitmap.Width - 1
                For y As Integer = 0 To Bitmap.Height - 1
                    If Points(z).Contains(New Point(x, y)) Then
                        matrix(y, x) = 1
                    End If
                Next
            Next
            ColorRects.Add(New ColourRectsCustom(GetAllSteps(matrix), Colors(z)))
        Next
        Return New InstructionsCustom(ColorRects.ToArray(), New Size(Bitmap.Width, Bitmap.Height))
    End Function

    Public Function GetInstructionsPBPFromImage(Bitmap As Bitmap) As InstructionsCustom
        Dim Colors As New List(Of ColourCustom)
        Dim Points As New List(Of List(Of Point))
        For x As Integer = 0 To Bitmap.Width - 1
            For y As Integer = 0 To Bitmap.Height - 1
                Dim color1 = Bitmap.GetPixel(x, y)
                If color1.A = 255 Then
                    Dim color = New ColourCustom(color1.R, color1.G, color1.B)
                    If Not Colors.Contains(color) Then
                        Colors.Add(color)
                        Points.Add(New List(Of Point))
                    End If
                    Points(Colors.IndexOf(color)).Add(New Point(x, y))
                End If
            Next
        Next
        Dim ColorRects As New List(Of ColourRectsCustom)
        For z As Integer = 0 To Colors.Count - 1
            ColorRects.Add(New ColourRectsCustom(Points(z).ConvertAll(Function(p As Point) New Rectangle(p, New Size(1, 1))).ToArray(), Colors(z)))
        Next
        Return New InstructionsCustom(ColorRects.ToArray(), New Size(Bitmap.Width, Bitmap.Height))
    End Function

    Public Function GetAllSteps(matrix As Integer(,)) As Rectangle()
        Dim rects As New List(Of Rectangle)
1:
        Dim any1 As Boolean = False
        For x As Integer = 0 To matrix.GetLength(0) - 1
            For y As Integer = 0 To matrix.GetLength(1) - 1
                If matrix(x, y) = 1 Then
                    any1 = True
                    Exit For
                End If
            Next
            If any1 Then
                Exit For
            End If
        Next
        If any1 Then
            Dim newRect = MaxSubmatrix(matrix)
            rects.Add(newRect)
            For x As Integer = newRect.X To newRect.X + newRect.Width - 1
                For y As Integer = newRect.Y To newRect.Y + newRect.Height - 1
                    matrix(y, x) = 0
                Next
            Next
            GoTo 1
        Else
            Return rects.ToArray
        End If
    End Function

    Public Function MaxSubmatrix(matrix As Integer(,)) As Rectangle
        Dim n As Integer = matrix.GetLength(0)
        Dim m As Integer = matrix.GetLength(1)
        Dim maxArea As Integer = -1, tempArea As Integer = -1

        Dim x1 As Integer = 0, y1 As Integer = 0, x2 As Integer = 0, y2 As Integer = 0

        Dim d As Integer() = New Integer(m - 1) {}

        For i As Integer = 0 To m - 1
            d(i) = -1
        Next

        Dim d1 As Integer() = New Integer(m - 1) {}

        Dim d2 As Integer() = New Integer(m - 1) {}

        Dim stack As New Stack(Of Integer)()

        For i As Integer = 0 To n - 1
            For j As Integer = 0 To m - 1
                If matrix(i, j) = 0 Then
                    d(j) = i
                End If
            Next

            stack.Clear()

            For j As Integer = 0 To m - 1
                While stack.Count > 0 AndAlso d(stack.Peek()) <= d(j)
                    stack.Pop()
                End While

                d1(j) = If((stack.Count = 0), -1, stack.Peek())

                stack.Push(j)
            Next

            stack.Clear()

            For j As Integer = m - 1 To 0 Step -1
                While stack.Count > 0 AndAlso d(stack.Peek()) <= d(j)
                    stack.Pop()
                End While

                d2(j) = If((stack.Count = 0), m, stack.Peek())

                stack.Push(j)
            Next

            For j As Integer = 0 To m - 1
                tempArea = (i - d(j)) * (d2(j) - d1(j) - 1)

                If tempArea > maxArea Then
                    maxArea = tempArea

                    x1 = d1(j) + 1
                    y1 = d(j) + 1

                    x2 = d2(j) - 1
                    y2 = i
                End If
            Next
        Next

        Return New Rectangle(x1, y1, (x2 - x1 + 1), (y2 - y1 + 1))
    End Function
End Module
