Imports hamarb123.VBUMIP.Desktop.ImageCustom
#If DESKTOP = True Then
Imports Image = System.Drawing.Image
Imports Graphics = System.Drawing.Graphics
Imports Size = System.Drawing.Size
Imports CompositingMode = System.Drawing.Drawing2D.CompositingMode
Imports InterpolationMode = System.Drawing.Drawing2D.InterpolationMode
Imports PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode
Imports CompositingQuality = System.Drawing.Drawing2D.CompositingQuality
Imports Rectangle = System.Drawing.Rectangle
Imports Bitmap = System.Drawing.Bitmap
Imports PixelFormat = System.Drawing.Imaging.PixelFormat
Imports ImageLockMode = System.Drawing.Imaging.ImageLockMode
Imports System.Linq.Enumerable
Imports Enumerable = System.Linq.Enumerable
Imports Marshal = System.Runtime.InteropServices.Marshal
#End If

Public NotInheritable Class GraphicsCustom
#If DESKTOP = True Then
    Public Sub New(size As Size)
        Me.ImageSize = size
        Me.L = Enumerable.Repeat(Of Byte)(Nothing, size.Height * size.Width * BPP).ToArray()
    End Sub

    Public ReadOnly ImageSize As Size

    Public L As Byte()

    Public Sub Invalidate(getGraphics As Func(Of Graphics), renderSize As Size)
        Dim mybytearray As Byte() = Me.L

        Dim x1 As Image = ImageFromRawBgraArray(mybytearray, Me.ImageSize.Width, Me.ImageSize.Height, PixelFormat.Format32bppArgb)

        Dim g = getGraphics.Invoke

        g.CompositingMode = CompositingMode.SourceCopy
        g.InterpolationMode = InterpolationMode.NearestNeighbor
        g.PixelOffsetMode = PixelOffsetMode.Half
        g.CompositingQuality = CompositingQuality.Default
        Dim drawRectangle As Rectangle
        drawRectangle = New Rectangle(0, 0, renderSize.Width, renderSize.Height)

        g.DrawImage(x1, drawRectangle)

        g.Dispose()
        x1.Dispose()
    End Sub

    Public Shared Function ImageFromRawBgraArray(arr As Byte(), width As Integer, height As Integer, pixelFormat As PixelFormat) As Image
        Dim output = New Bitmap(width, height, pixelFormat)
        Dim rect = New Rectangle(0, 0, width, height)
        Dim bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat)

        Dim arrLength = width * CInt(Image.GetPixelFormatSize(output.PixelFormat) / 8) * height
        Dim ptr = bmpData.Scan0
        Marshal.Copy(arr, 0, ptr, arrLength)

        output.UnlockBits(bmpData)
        Return output
    End Function
#End If
End Class
