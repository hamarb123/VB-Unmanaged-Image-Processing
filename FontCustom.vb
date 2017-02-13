Public Structure FontCustom
#If DESKTOP Then
    Private _FontFamily As Drawing.FontFamily
    Private _GdiVerticalFont As Boolean?
    Private _GdiCharSet As Byte?
    Private _Style As Drawing.FontStyle?
    Private _Size As Single?
    Private _Unit As Drawing.GraphicsUnit?

    Public Property FontFamily As Drawing.FontFamily
        Get
            Return If(Me._FontFamily, New Drawing.FontFamily(Drawing.Text.GenericFontFamilies.SansSerif))
        End Get
        Set(value As Drawing.FontFamily)
            Me._FontFamily = value
        End Set
    End Property
    Public Property GdiVerticalFont As Boolean?
        Get
            Return If(Me._GdiVerticalFont, False)
        End Get
        Set(value As Boolean?)
            Me._GdiVerticalFont = value
        End Set
    End Property
    Public Property GdiCharSet As Byte?
        Get
            Return If(Me._GdiCharSet, 1)
        End Get
        Set(value As Byte?)
            Me._GdiCharSet = value
        End Set
    End Property
    Public Property Style As Drawing.FontStyle?
        Get
            Return If(Me._Style, Drawing.FontStyle.Regular)
        End Get
        Set(value As Drawing.FontStyle?)
            Me._Style = value
        End Set
    End Property
    Public Property Size As Single?
        Get
            Return If(Me._Size, 16)
        End Get
        Set(value As Single?)
            Me._Style = value
        End Set
    End Property
    Public Property Unit As Drawing.GraphicsUnit?
        Get
            Return If(Me._Unit, Drawing.GraphicsUnit.Pixel)
        End Get
        Set(value As Drawing.GraphicsUnit?)
            Me._Unit = value
        End Set
    End Property

    Public Sub New(Font As Drawing.Font)
        Me.FontFamily = Font.FontFamily
        Me.GdiCharSet = Font.GdiCharSet
        Me.GdiVerticalFont = Font.GdiVerticalFont
        Me.Size = Font.Size
        Me.Style = Font.Style
        Me.Unit = Font.Unit
    End Sub

    Public Function ToFont() As Drawing.Font
        Return New Drawing.Font(Me.FontFamily, Me.Size, Me.Style, Me.Unit, Me.GdiCharSet, Me.GdiVerticalFont)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf (obj) Is FontCustom Then
            Return CType(obj, FontCustom).FontFamily.Equals(Me.FontFamily) And
                CType(obj, FontCustom).GdiCharSet = Me.GdiCharSet And
                CType(obj, FontCustom).GdiVerticalFont = Me.GdiVerticalFont And
                CType(obj, FontCustom).Size = Me.Size And
                CType(obj, FontCustom).Style = Me.Style And
                CType(obj, FontCustom).Unit = Me.Unit
        End If
        Return False
    End Function
#End If
End Structure
