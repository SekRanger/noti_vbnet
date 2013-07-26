<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotiForm
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NotiForm))
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.LabelMessage = New System.Windows.Forms.Label()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.Blink = New System.Windows.Forms.Timer(Me.components)
    Me.Panel3 = New System.Windows.Forms.Panel()
    Me.LabelTitle = New System.Windows.Forms.Label()
    Me.CloseButton1 = New noti_vb.net.CloseButton()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel3.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    Me.Panel1.Controls.Add(Me.Panel2)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel1.Location = New System.Drawing.Point(2, 20)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(316, 58)
    Me.Panel1.TabIndex = 1
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.LabelMessage)
    Me.Panel2.Controls.Add(Me.PictureBox1)
    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
    Me.Panel2.Location = New System.Drawing.Point(0, 0)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(316, 58)
    Me.Panel2.TabIndex = 1
    '
    'LabelMessage
    '
    Me.LabelMessage.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
    Me.LabelMessage.Location = New System.Drawing.Point(60, 0)
    Me.LabelMessage.Name = "LabelMessage"
    Me.LabelMessage.Padding = New System.Windows.Forms.Padding(8)
    Me.LabelMessage.Size = New System.Drawing.Size(256, 58)
    Me.LabelMessage.TabIndex = 1
    Me.LabelMessage.Text = "Message"
    Me.LabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'PictureBox1
    '
    Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
    Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(60, 58)
    Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
    Me.PictureBox1.TabIndex = 2
    Me.PictureBox1.TabStop = False
    Me.PictureBox1.Visible = False
    '
    'ImageList1
    '
    Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    Me.ImageList1.Images.SetKeyName(0, "iconmonstr-phone-5-icon.png")
    '
    'Blink
    '
    Me.Blink.Interval = 300
    '
    'Panel3
    '
    Me.Panel3.BackColor = System.Drawing.Color.Transparent
    Me.Panel3.Controls.Add(Me.CloseButton1)
    Me.Panel3.Controls.Add(Me.LabelTitle)
    Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
    Me.Panel3.Location = New System.Drawing.Point(2, 2)
    Me.Panel3.Name = "Panel3"
    Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
    Me.Panel3.Size = New System.Drawing.Size(316, 18)
    Me.Panel3.TabIndex = 2
    '
    'LabelTitle
    '
    Me.LabelTitle.BackColor = System.Drawing.Color.Black
    Me.LabelTitle.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
    Me.LabelTitle.ForeColor = System.Drawing.Color.White
    Me.LabelTitle.Location = New System.Drawing.Point(0, 0)
    Me.LabelTitle.Name = "LabelTitle"
    Me.LabelTitle.Size = New System.Drawing.Size(316, 16)
    Me.LabelTitle.TabIndex = 3
    Me.LabelTitle.Text = "กิ้งกือสุนัข"
    Me.LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'CloseButton1
    '
    Me.CloseButton1.Cursor = System.Windows.Forms.Cursors.Hand
    Me.CloseButton1.Dock = System.Windows.Forms.DockStyle.Right
    Me.CloseButton1.Location = New System.Drawing.Point(288, 0)
    Me.CloseButton1.Name = "CloseButton1"
    Me.CloseButton1.Size = New System.Drawing.Size(28, 16)
    Me.CloseButton1.TabIndex = 4
    Me.CloseButton1.Text = "CloseButton1"
    '
    'NotiForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.Color.Black
    Me.ClientSize = New System.Drawing.Size(320, 80)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.Panel3)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.KeyPreview = True
    Me.Name = "NotiForm"
    Me.Padding = New System.Windows.Forms.Padding(2)
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
    Me.Text = "Form1"
    Me.Panel1.ResumeLayout(False)
    Me.Panel2.ResumeLayout(False)
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel3.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents LabelMessage As System.Windows.Forms.Label
  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
  Friend WithEvents Blink As System.Windows.Forms.Timer
  Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
  Friend WithEvents Panel3 As System.Windows.Forms.Panel
  Friend WithEvents LabelTitle As System.Windows.Forms.Label
  Friend WithEvents CloseButton1 As noti_vb.net.CloseButton
End Class
