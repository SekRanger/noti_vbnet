Imports System.Threading

Public Class NotiForm
  Private Shared IS_RINGING As Boolean = False
  Private Delegate Sub ToggleDelegate(ByVal b As Boolean)
  Private mJustStart As Boolean = True
  Private mWaitShown As New System.Threading.ManualResetEvent(False)
  Private mDeviceId As String
  Private mShaking As Boolean = False
  Private mThreadShake As Thread
  Private mRinging As Boolean = False
  Private mTalking As Boolean = False
  Private mRingPhoneNo As String = String.Empty
  'Private mDictionary
  Private mCountMissedCall As Integer = 0
  Private mThreadCounter As Thread

  Private Sub counter(ByVal o As Object)
    Dim startMsg As String = o.ToString()
    Dim c As Integer = 0
    Try
      Do
        SetLabelMsg(String.Format("{0} ({1} s.)", startMsg, c))
        Threading.Thread.Sleep(1000)
        c += 1
      Loop
    Catch ex As Exception
    End Try
  End Sub

  Public ReadOnly Property DeviceId As String
    Get
      Return mDeviceId
    End Get
  End Property

  Public Sub Toggle(ByVal b As Boolean)
    If (Me.InvokeRequired) Then
      Dim cb As New ToggleDelegate(AddressOf Toggle)
      Me.Invoke(cb, b)
    Else
      Me.Visible = b
    End If
  End Sub

  Private Delegate Sub SetLabelTitleDelegate(ByVal txt As String)
  Public Sub SetLabelTitle(ByVal txt As String)
    If (LabelTitle.InvokeRequired) Then
      Dim cb As New SetLabelTitleDelegate(AddressOf SetLabelTitle)
      Me.LabelTitle.Invoke(cb, txt)
    Else
      Me.LabelTitle.Text = txt
    End If
  End Sub

  Private Delegate Sub SetLabelMsgDelegate(ByVal txt As String)
  Public Sub SetLabelMsg(ByVal txt As String)
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

  Public Sub New(ByVal deviceId As String)
    mDeviceId = deviceId
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    'Debug.Print(My.Computer.Screen.WorkingArea.ToString())
    Dim r As Rectangle = My.Computer.Screen.WorkingArea
    Me.Top = r.Bottom - Me.Bottom - 8
    Me.Left = r.Right - Me.Right - 8
    'BgWk.RunWorkerAsync()
    Me.LabelMessage.Text = String.Empty
    Me.TopMost = True
  End Sub

  Private Sub NotiForm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    StopShake()
    StopCounter()
  End Sub

  Private Sub NotiForm_MouseEnter(sender As Object, e As System.EventArgs) Handles Me.MouseEnter
    Me.Opacity = 100
  End Sub

  Private Sub NotiForm_MouseLeave(sender As Object, e As System.EventArgs) Handles Me.MouseLeave
    Debug.Print("asdfasdfasdf")
    Me.Opacity = 0
  End Sub

  Private Sub NotiForm_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    Debug.Print("asdfasdfasdf")
  End Sub

  Private Sub MainForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    If (Not mJustStart) Then
      mJustStart = True
      'mWaitShown.Set()
    End If
    Me.TopMost = True
  End Sub

  Private Sub BlinkStart()
    If (Not Blink.Enabled) Then
      Blink.Start()
    End If
  End Sub

  Private Sub BlinkStop()
    If (Blink.Enabled) Then
      Blink.Stop()
    End If
  End Sub

  Private Sub Blink_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Blink.Tick
    'Me.Visible = Not Me.Visible()
    If (Me.Opacity = 1) Then
      Me.Opacity = 0
    Else
      Me.Opacity = 1
    End If
  End Sub

  <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=False)> _
  Private Shared Function GetDesktopWindow() As IntPtr
  End Function

  Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
    Get
      Dim cp As CreateParams = MyBase.CreateParams
      cp.Style = &H40000000 Or &H4000000
      cp.ExStyle = cp.ExStyle And &H80000
      cp.Parent = GetDesktopWindow()
      Return MyBase.CreateParams
    End Get
  End Property

  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    Select Case m.Msg
      Case &H84
        MyBase.WndProc(m)
        If (CType(m.Result, Integer) = 1) Then
          m.Result = 2
        End If
        Return
    End Select
    MyBase.WndProc(m)
  End Sub

  Private Delegate Sub SetLocDelegate(ByVal p As Point)
  Private Sub SetLoc(ByVal p As Point)
    If (Me.InvokeRequired) Then
      Dim cb As New SetLocDelegate(AddressOf SetLoc)
      Me.Invoke(cb, p)
    Else
      Me.Location = p
    End If
  End Sub

  Private Delegate Function GetLocDelegate() As Point
  Private Function GetLoc() As Point
    If (Me.InvokeRequired) Then
      Dim cb As New GetLocDelegate(AddressOf GetLoc)
      Return Me.Invoke(cb)
    Else
      Return Me.Location
    End If
  End Function

  Private Sub _shake()
    If (Not mShaking) Then
      mShaking = True
      Dim o As Point = GetLoc()
      Dim r As New System.Random()
      Dim rs As New System.Random()
      Dim d1 As DateTime
      Dim d2 As DateTime

      Try
        Do
          d1 = DateTime.Now
          Do
            Dim newX As Integer = r.Next(8)
            If (rs.Next(8) > 4) Then
              newX = 0 - newX
            End If
            Dim newY As Integer = r.Next(8)
            If (rs.Next(8) > 4) Then
              newY = 0 - newY
            End If
            Dim n As New Point(o.X + newX, o.Y + newY)
            'Me.Location = n
            SetLoc(n)
            Threading.Thread.Sleep(28)
            d2 = DateTime.Now
          Loop Until (d2.Subtract(d1).TotalMilliseconds >= 1000)
          SetLoc(o)
          Threading.Thread.Sleep(3000)
        Loop
      Catch ex As Threading.ThreadInterruptedException
      End Try

      SetLoc(o)
      mShaking = False
    End If
  End Sub

  Public Sub Shake()
    If (Not mShaking) Then
      mThreadShake = New Thread(AddressOf _shake)
      mThreadShake.Start()
    End If
  End Sub

  Public Sub StopShake()
    Try
      mThreadShake.Interrupt()
    Catch ex As Exception
    End Try
  End Sub

  Private Sub StartCounter(ByVal startMsg As String)
    mThreadCounter = New Thread(AddressOf counter)
    mThreadCounter.Start(startMsg)
  End Sub

  Private Sub StopCounter()
    Try
      mThreadCounter.Interrupt()
    Catch ex As Exception
    End Try
  End Sub

  Public Sub ProcessThisShit(ByVal m As AndroidUdpBroadMsg)
    Select Case m.MsgKey
      Case "PAIR"
        SetLabelTitle(m.Model)
      Case "RINGING"
        StopCounter()
        mRinging = True
        mRingPhoneNo = m.Msg
        Shake()
        StartCounter("Ringing: " & m.Msg)
      Case "OFFHOOK"
        StopCounter()
        mRinging = False
        mTalking = True
        StartCounter("In call: " & mRingPhoneNo)
        StopShake()
      Case "IDLE"
        StopCounter()
        StopShake()
        SetLabelMsg(String.Empty)
        If (mRinging AndAlso Not mTalking) Then
          mCountMissedCall += 1
          If (mCountMissedCall = 1) Then
            SetLabelTitle(m.Model & " - " & mCountMissedCall & " missed-call.")
          Else
            SetLabelTitle(m.Model & " - " & mCountMissedCall & " missed-call(s).")
          End If
        End If
        mRinging = False
        mTalking = False
        mRingPhoneNo = String.Empty
    End Select
  End Sub

  Private mDragged As Boolean = False
  Private mStartDragPoint As Point

  Private Sub LabelTitle_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    mDragged = True
    mStartDragPoint = e.Location
  End Sub

  Private Sub LabelTitle_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    If (mDragged) Then
      Dim p1 As Point = e.Location
      Dim p2 As Point = PointToScreen(p1)
      Dim p3 As Point = New Point(p2.X - mStartDragPoint.X,
                                     p2.Y - mStartDragPoint.Y)
      Me.Location = p3
    End If
  End Sub

  Private Sub LabelTitle_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    mDragged = False
  End Sub

  Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click
    Me.Close()
  End Sub
End Class
