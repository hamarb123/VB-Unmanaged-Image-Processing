#If DESKTOP = True Then
Imports Point = System.Drawing.Point
Imports Rectangle = System.Drawing.Rectangle
Imports Size = System.Drawing.Size
#End If

Public NotInheritable Class ImageCustom
    Public Sub Clear(Color As ColourCustom)
        SyncLock Me.Locker
            Dim Size = _GetSize()
            ResetToColor(Color, _GetSize)
        End SyncLock
    End Sub

#Disable Warning IDE1006 ' Naming Styles
    Protected Function _GetSize() As Size
#Enable Warning IDE1006 ' Naming Styles
        Return New Size(Me.Width, Me.Pixels.Length / BPP / Me.Width)
    End Function

    Public Function GetSize() As Size
        SyncLock Me.Locker
            Return _GetSize()
        End SyncLock
    End Function

    Public Function GetAllPixelsRaw() As Byte()
        SyncLock Me.Locker
            Return Me.Pixels
        End SyncLock
    End Function

    Public Function GetPixel(Location As Point) As ColourCustom
        SyncLock Me.Locker
            If New Rectangle(Point.Empty, _GetSize()).Contains(Location) Then
                Return New ColourCustom(Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 2), Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 1), Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 0))
            Else
                Throw New IndexOutOfRangeException()
            End If
        End SyncLock
    End Function

    Public Function GetPixel(X As Integer, Y As Integer) As ColourCustom
        Return Me.GetPixel(New Point(X, Y))
    End Function

    Public Sub SetPixel(Location As Point, Color As ColourCustom)
        SyncLock Me.Locker
            If New Rectangle(Point.Empty, _GetSize()).Contains(Location) Then
                Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 0) = Color.Blue
                Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 1) = Color.Green
                Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 2) = Color.Red
                Me.Pixels((Location.X + Location.Y * Me.Width) * BPP + 3) = Color.Alpha
            Else
                Throw New IndexOutOfRangeException()
            End If
        End SyncLock
    End Sub

    Public Sub SetPixel(X As Integer, Y As Integer, Color As ColourCustom)
        Me.SetPixel(New Point(X, Y), Color)
    End Sub

    Public Sub New(Width As Integer, Height As Integer)
        Me.New(New Size(Width, Height))
    End Sub

    Public Sub New(Size As Size)
        Me.Width = Size.Width
        ResetToColor(New ColourCustom(255, 255, 255), Size)
    End Sub

    Private Sub ResetToColor(Color As ColourCustom, Size As Size)
        Me.Pixels = New Byte(Size.Height * Size.Width * BPP - 1) {}
        Dim whatToRepeat As Byte() = {Color.Blue, Color.Green, Color.Red, Color.Alpha}
        For x As Integer = 0 To Size.Height * Size.Width * BPP - 1
            Me.Pixels(x) = whatToRepeat(x Mod BPP)
        Next
    End Sub

    Public Sub FillArea(Area As Rectangle, Color As ColourCustom)
        SyncLock Me.Locker
            _FillArea(Area, Color)
        End SyncLock
    End Sub

#Disable Warning IDE1006 ' Naming Styles
    Private Sub _FillArea(Area As Rectangle, Color As ColourCustom)
#Enable Warning IDE1006 ' Naming Styles
        Dim ContRect = New Rectangle(Point.Empty, _GetSize())
        If ContRect.IntersectsWith(Area) Then
            If Not ContRect.Contains(Area) Then
                If Area.X < ContRect.X Then
                    Dim old = Area.X
                    Area.X = ContRect.X
                    Area.Width = Area.Width + old
                End If
                If Area.Y < ContRect.Y Then
                    Dim old = Area.Y
                    Area.Y = ContRect.Y
                    Area.Height = Area.Height + old
                End If
                If Area.Right > ContRect.Right Then
                    Area.Width = ContRect.Right - Area.X
                End If
                If Area.Bottom > ContRect.Bottom Then
                    Area.Height = ContRect.Bottom - Area.Y
                End If
            End If
            Dim pixels() As Byte = New Byte((Area.Width * BPP) - 1) {}
            For x As Integer = 0 To Area.Width - 1
                pixels(x * BPP + 0) = Color.Blue
                pixels(x * BPP + 1) = Color.Green
                pixels(x * BPP + 2) = Color.Red
                pixels(x * BPP + 3) = Color.Alpha
            Next
            For y As Integer = Area.Y To Area.Y + Area.Height - 1
                Array.Copy(pixels, 0, Me.Pixels, (Area.X + y * Me.Width) * BPP, pixels.Length)
            Next
        End If
    End Sub

    Public Sub DrawInstructions(rect As Rectangle, Instructions As InstructionsCustom)
        SyncLock Me.Locker
            Dim size = _GetSize()
            For Each Instruction In Instructions.Item1
                For x As Integer = 0 To Instruction.Item1.Length - 1
                    _FillArea(New Rectangle(
                                            Math.Floor(rect.X + (Instruction.Item1(x).X / Instructions.Item2.Width * rect.Width)),
                                            Math.Floor(rect.Y + (Instruction.Item1(x).Y / Instructions.Item2.Height * rect.Height)),
                                            Math.Ceiling(Instruction.Item1(x).Width / Instructions.Item2.Width * rect.Width),
                                            Math.Ceiling(Instruction.Item1(x).Height / Instructions.Item2.Height * rect.Height)
                                           ), Instruction.Item2)
                Next
            Next
        End SyncLock
    End Sub

    Public Sub DrawRectangle(Area As Rectangle, Size As UInteger, Color As ColourCustom)
        SyncLock Me.Locker
            If Size > 0 Then
                _FillArea(New Rectangle(Area.X, Area.Y, Area.Width, Size), Color)
                _FillArea(New Rectangle(Area.X, Area.Bottom - Size, Area.Width, Size), Color)
                _FillArea(New Rectangle(Area.X, Area.Y + Size, Size, Area.Height - Size - Size), Color)
                _FillArea(New Rectangle(Area.Right - Size, Area.Y + Size, Size, Area.Height - Size - Size), Color)
            End If
        End SyncLock
    End Sub

    Private Pixels As Byte()
    Friend Const BPP = 4
    Private Width As UInteger
    Private Locker As New Object

    Public Sub SetAllPixelsRaw(Pixels As Byte())
        SyncLock Me.Locker
            If Not Pixels.Length = Me.Pixels.Length Then
                Throw New IndexOutOfRangeException("Length is not correct")
            End If
            Me.Pixels = Pixels
        End SyncLock
    End Sub
End Class
