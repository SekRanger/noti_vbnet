Partial Class Component1
    Inherits System.ComponentModel.Component

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

    End Sub

    'Component overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Component1))
    Me.NtfIcon = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.NtfMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    Me.NtfMenu.SuspendLayout()
    '
    'NtfIcon
    '
    Me.NtfIcon.ContextMenuStrip = Me.NtfMenu
    Me.NtfIcon.Icon = CType(resources.GetObject("NtfIcon.Icon"), System.Drawing.Icon)
    Me.NtfIcon.Text = "NotifyIcon1"
    Me.NtfIcon.Visible = True
    '
    'NtfMenu
    '
    Me.NtfMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
    Me.NtfMenu.Name = "NtfMenu"
    Me.NtfMenu.Size = New System.Drawing.Size(93, 26)
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(92, 22)
    Me.ToolStripMenuItem1.Text = "Exit"
    Me.NtfMenu.ResumeLayout(False)

  End Sub
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents NtfIcon As System.Windows.Forms.NotifyIcon
  Public WithEvents NtfMenu As System.Windows.Forms.ContextMenuStrip

End Class
