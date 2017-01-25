Imports System.Drawing
Imports System.Linq

Public Class VBUnmanagedImageProcessingControl
    Private Sub VBUnmanagedImageProcessingControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetImageVariable()
    End Sub

    Dim Pixels As Byte()
    Dim Image As ImageCustom
    Dim G As GraphicsCustom
    Dim DefaultPixels As Byte()

    Public Overrides Property BackColor As Color
        Get
#Disable Warning IDE0009 ' Member access should be qualified.
            Return MyBase.BackColor
#Enable Warning IDE0009 ' Member access should be qualified.
        End Get
        Set(value As Color)
            If value.A <> 255 Then
                value = Color.FromArgb(value.R, value.G, value.B)
            End If
#Disable Warning IDE0009 ' Member access should be qualified.
            MyBase.BackColor = value
#Enable Warning IDE0009 ' Member access should be qualified.
            ResetImageVariable()
        End Set
    End Property

    Private Locker As New Object

    Private RegSize As Size = New Size(960, 540)
    Public Property RegisterSize As Size
        Get
            Return Me.RegSize
        End Get
        Set(value As Size)
            If value.Width * value.Height > 0 Then
                Me.RegSize = value
                ResetImageVariable()
            Else
            End If
        End Set
    End Property

    Private Sub ResetImageVariable()
        SyncLock Me.Locker
            Me.Image = New ImageCustom(Me.RegisterSize.Width, Me.RegisterSize.Height)
            Me.G = New GraphicsCustom(Me.Image.GetSize())
            Me.Image.Clear(New ColourCustom(Me.BackColor.R, Me.BackColor.G, Me.BackColor.B))
            Me.DefaultPixels = Me.Image.GetAllPixelsRaw()
            Me.Pixels = Me.DefaultPixels.ToArray()
        End SyncLock
    End Sub

    Private Sub ResetDefaultImageVariable()
        Dim I = New ImageCustom(Me.Image.GetSize().Width, Me.Image.GetSize().Height)
        I.Clear(New ColourCustom(Me.BackColor.R, Me.BackColor.G, Me.BackColor.B))
        Me.DefaultPixels = I.GetAllPixelsRaw()
    End Sub

    Private Sub ControlDrawer_Tick(sender As Object, e As EventArgs) Handles ControlDrawer.Tick
        Dim StartDraw = Date.Now
        If Me.ClientSize.Width * Me.ClientSize.Height > 0 Then
            Dim b As New Bitmap(Me.ClientSize.Width, Me.ClientSize.Height)

            Me.G.L = Me.Pixels
            Me.G.Invalidate(Function() Graphics.FromImage(b), b.Size)

            Me.DisplayImage.Image = b
        End If
        Me.TimeDraw = Date.Now.Subtract(StartDraw).TotalSeconds
    End Sub

    Private Sub ByteDrawer_Tick(sender As Object, e As EventArgs) Handles ByteDrawer.Tick
        Dim StartMake = Date.Now
        SyncLock Me.Locker
            Me.Image.SetAllPixelsRaw(Me.DefaultPixels.ToArray())
#Disable Warning IDE0009 ' Member access should be qualified.
            RaiseEvent Draw(Me.Image)
#Enable Warning IDE0009 ' Member access should be qualified.
            Me.Pixels = Me.Image.GetAllPixelsRaw
        End SyncLock
        Me.TimeMake = Date.Now.Subtract(StartMake).TotalSeconds
    End Sub

    Dim TimeDraw As Double
    Dim TimeMake As Double

    Public Event Draw(ByRef Graphics As ImageCustom)

    Public ReadOnly Property FPS As Double
        Get
            Return Math.Round(1 / Math.Max(Me.TimeDraw, Me.TimeMake))
        End Get
    End Property

    Public Function GetImage() As Image
        Dim i = New Bitmap(Me.Image.GetSize().Width, Me.Image.GetSize().Height)
        Dim g As GraphicsCustom = New GraphicsCustom(i.Size)
        Dim gfx As Graphics = Graphics.FromImage(i)
        g.L = Me.Pixels
        g.Invalidate(Function() gfx, Me.Image.GetSize())
        Return i
    End Function
End Class
