<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PairingForm
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
    Me.LabelTimeout = New System.Windows.Forms.Label()
    Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
    Me.Label2 = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'LabelTimeout
    '
    Me.LabelTimeout.AutoSize = True
    Me.LabelTimeout.Location = New System.Drawing.Point(60, 16)
    Me.LabelTimeout.Name = "LabelTimeout"
    Me.LabelTimeout.Size = New System.Drawing.Size(41, 13)
    Me.LabelTimeout.TabIndex = 0
    Me.LabelTimeout.Text = "timeout"
    '
    'Timer1
    '
    Me.Timer1.Interval = 1000
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 16)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(48, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Timeout:"
    '
    'PairingForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(113, 46)
    Me.ControlBox = False
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.LabelTimeout)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "PairingForm"
    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Pairing..."
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents LabelTimeout As System.Windows.Forms.Label
  Friend WithEvents Timer1 As System.Windows.Forms.Timer
  Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
