<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VBUnmanagedImageProcessingControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DisplayImage = New System.Windows.Forms.PictureBox()
        Me.ByteDrawer = New System.Windows.Forms.Timer(Me.components)
        Me.ControlDrawer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DisplayImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DisplayImage
        '
        Me.DisplayImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DisplayImage.Location = New System.Drawing.Point(0, 0)
        Me.DisplayImage.Name = "DisplayImage"
        Me.DisplayImage.Size = New System.Drawing.Size(960, 540)
        Me.DisplayImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.DisplayImage.TabIndex = 0
        Me.DisplayImage.TabStop = False
        '
        'ByteDrawer
        '
        Me.ByteDrawer.Enabled = True
        Me.ByteDrawer.Interval = 1
        '
        'ControlDrawer
        '
        Me.ControlDrawer.Enabled = True
        Me.ControlDrawer.Interval = 1
        '
        'VBUnmanagedImageProcessingControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.DisplayImage)
        Me.Name = "VBUnmanagedImageProcessingControl"
        Me.Size = New System.Drawing.Size(960, 540)
        CType(Me.DisplayImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DisplayImage As Windows.Forms.PictureBox
    Friend WithEvents ByteDrawer As Windows.Forms.Timer
    Friend WithEvents ControlDrawer As Windows.Forms.Timer
End Class
