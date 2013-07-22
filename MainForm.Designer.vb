<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.ButtonPair = New System.Windows.Forms.Button()
    Me.ButtonRemove = New System.Windows.Forms.Button()
    Me.ListView1 = New System.Windows.Forms.ListView()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
    Me.Label1.Location = New System.Drawing.Point(8, 8)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(77, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Paired devices"
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.ButtonPair)
    Me.Panel1.Controls.Add(Me.ButtonRemove)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.Panel1.Location = New System.Drawing.Point(8, 334)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(268, 34)
    Me.Panel1.TabIndex = 1
    '
    'ButtonPair
    '
    Me.ButtonPair.Location = New System.Drawing.Point(112, 8)
    Me.ButtonPair.Name = "ButtonPair"
    Me.ButtonPair.Size = New System.Drawing.Size(75, 23)
    Me.ButtonPair.TabIndex = 1
    Me.ButtonPair.Text = "Pair"
    Me.ButtonPair.UseVisualStyleBackColor = True
    '
    'ButtonRemove
    '
    Me.ButtonRemove.Location = New System.Drawing.Point(192, 8)
    Me.ButtonRemove.Name = "ButtonRemove"
    Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
    Me.ButtonRemove.TabIndex = 0
    Me.ButtonRemove.Text = "Remove"
    Me.ButtonRemove.UseVisualStyleBackColor = True
    '
    'ListView1
    '
    Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListView1.Location = New System.Drawing.Point(8, 21)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(268, 313)
    Me.ListView1.TabIndex = 2
    Me.ListView1.UseCompatibleStateImageBehavior = False
    '
    'MainForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(284, 376)
    Me.Controls.Add(Me.ListView1)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.Label1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.Name = "MainForm"
    Me.Padding = New System.Windows.Forms.Padding(8)
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Noti"
    Me.Panel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ButtonPair As System.Windows.Forms.Button
  Friend WithEvents ButtonRemove As System.Windows.Forms.Button
End Class
