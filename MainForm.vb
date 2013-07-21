Public Class MainForm
  Private Shared IS_RINGING As Boolean = False
  Private Delegate Sub ToggleDelegate(b As Boolean)
  Private mJustStart As Boolean = True
  Private mSocket As System.Net.Sockets.UdpClient
  Private mRE As New System.Threading.ManualResetEvent(False)
  Private mWaitShown As New System.Threading.ManualResetEvent(False)
  Private mDeviceId As String
  Public ReadOnly Property DeviceId As String
    Get
      Return mDeviceId
    End Get
  End Property

  Public Sub Toggle(b As Boolean)
    If (Me.InvokeRequired) Then
      Dim cb As New ToggleDelegate(AddressOf Toggle)
      Me.Invoke(cb, b)
    Else
      Me.Visible = b
    End If
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
      Me.LabelMessage.Invoke(cb, txt)
    Else
      Me.LabelMessage.Text = txt
    End If
  End Sub

  Private Delegate Sub CloseMeDelegate()
  Public Sub CloseMe()
    If (Me.InvokeRequired) Then
      Dim cb As New CloseMeDelegate(AddressOf CloseMe)
      Me.Invoke(cb)
    Else
      Me.Close()
    End If
  End Sub

  Public Sub New(deviceId As String)
    mDeviceId = deviceId
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    'Debug.Print(My.Computer.Screen.WorkingArea.ToString())
    Dim r As Rectangle = My.Computer.Screen.WorkingArea
    Me.Top = r.Bottom - Me.Bottom - 8
    Me.Left = r.Right - Me.Right - 8
    'BgWk.RunWorkerAsync()
    Me.TopMost = True
  End Sub

  Private Sub NotifyIcon1_DoubleClick(sender As Object, e As System.EventArgs)
    Toggle(Not Visible)
  End Sub

  Private Sub MainForm_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
    If (Not mJustStart) Then
      mJustStart = True
      'mWaitShown.Set()
    End If
    Me.TopMost = True
    Blink.Start()
  End Sub

  Private Sub Blink_Tick(sender As Object, e As System.EventArgs) Handles Blink.Tick
    'Me.Visible = Not Me.Visible()
    If (Me.Opacity = 1) Then
      Me.Opacity = 0
    Else
      Me.Opacity = 1
    End If
  End Sub

  Private Sub LabelTitle_DoubleClick(sender As Object, e As System.EventArgs) Handles LabelTitle.DoubleClick
    Me.TopMost = True
  End Sub
End Class
