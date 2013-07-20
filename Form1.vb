Public Class Form1
  Private Shared IS_RINGING As Boolean = False

  Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'BgWk.RunWorkerAsync()
  End Sub

  Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
    Application.Exit()
  End Sub

  Private Delegate Sub SetLabelDelegate(txt As String)
  Public Sub SetLabel(txt As String)
    If (Label1.InvokeRequired) Then
      Dim cb As New SetLabelDelegate(AddressOf SetLabel)
      Me.Label1.Invoke(cb, txt)
    Else
      Me.Label1.Text = txt
    End If
  End Sub

  Public Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    'Debug.Print(My.Computer.Screen.WorkingArea.ToString())
    Dim r As Rectangle = My.Computer.Screen.WorkingArea
    Me.Top = r.Bottom - Me.Bottom - 8
    Me.Left = r.Right - Me.Right - 8
    NotifyIcon1.Icon = Me.Icon
    'BgWk.RunWorkerAsync()
    Me.SetTopLevel(True)
  End Sub

  Private Sub BgWk_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BgWk.DoWork
    Debug.Print(Me.InvokeRequired.ToString() & "dsfsdfasdfasdf")
  End Sub
End Class
