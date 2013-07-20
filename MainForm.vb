Public Class MainForm
  Private Shared IS_RINGING As Boolean = False
  Private Delegate Sub ToggleDelegate(b As Boolean)
  Private mJustStart As Boolean = True
  Private mSocket As System.Net.Sockets.UdpClient
  Private mRE As New System.Threading.ManualResetEvent(False)

  Protected Overrides Sub SetVisibleCore(value As Boolean)
    If (Not mJustStart) Then
      MyBase.SetVisibleCore(False)
      mJustStart = True
    Else
      MyBase.SetVisibleCore(value)
    End If
  End Sub

  Public Sub Toggle(b As Boolean)
    If (Me.InvokeRequired) Then
      Dim cb As New ToggleDelegate(AddressOf Toggle)
      Me.Invoke(cb, b)
    Else
      Me.Visible = b
    End If
  End Sub

  Private Sub aaaaaa()
    Dim bytes() As Byte
    Try
      mSocket = New System.Net.Sockets.UdpClient(60069)

      Debug.Print("Wait for data")
      bytes = mSocket.Receive(New System.Net.IPEndPoint(System.Net.IPAddress.Any, 60069))
      mSocket.Close()
      Dim msg As String = System.Text.UTF8Encoding.UTF8.GetString(bytes)
      Dim arr() As String = msg.Split(Chr(8))
      SetLabelTitle(arr(1))
      SetLabelMsg(arr(3))
      mRE.Set()
    Catch ex As Exception
      Debug.Print(ex.Message)
      Try
        mSocket.Close()
      Catch ex1 As Exception
      End Try
    End Try
  End Sub

  Private Sub BgWk_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgWk.DoWork
    Dim bytes() As Byte

    Try
      Do Until e.Cancel = True
        Dim t As New Threading.Thread(AddressOf aaaaaa)
        t.Start()
        mRE.WaitOne()
        Toggle(True)
        mRE.Reset()
      Loop
    Catch ex As Exception
      Debug.Print(ex.Message)
      Try
        mSocket.Close()
      Catch ex1 As Exception
      End Try
    End Try
  End Sub

  Private Sub MainForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      mSocket.Close()
    Catch ex As Exception
    End Try
  End Sub

  Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  End Sub

  Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
    Application.Exit()
  End Sub

  Private Delegate Sub SetLabelTitleDelegate(txt As String)
  Public Sub SetLabelTitle(txt As String)
    If (LabelTitle.InvokeRequired) Then
      Dim cb As New SetLabelTitleDelegate(AddressOf SetLabelTitle)
      Me.LabelTitle.Invoke(cb, txt)
    Else
      Me.LabelTitle.Text = txt
    End If
  End Sub

  Private Delegate Sub SetLabelMsgDelegate(txt As String)
  Public Sub SetLabelMsg(txt As String)
    If (LabelTitle.InvokeRequired) Then
      Dim cb As New SetLabelMsgDelegate(AddressOf SetLabelMsg)
      Me.LabelTitle.Invoke(cb, txt)
    Else
      Me.LabelTitle.Text = txt
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
    BgWk.RunWorkerAsync()
    Me.TopMost = True
  End Sub

  Private Sub NotifyIcon1_DoubleClick(sender As Object, e As System.EventArgs) Handles NotifyIcon1.DoubleClick
    Toggle(Not Visible)
  End Sub
End Class
