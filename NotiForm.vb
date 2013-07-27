Imports System.Threading
Imports Microsoft.Win32

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

  Public WM_NCLBUTTONDOWN As Integer = &HA1
  Public HTCAPTION As Integer = &H2
  <System.Runtime.InteropServices.DllImport("User32.dll")> _
  Public Shared Function ReleaseCapture() As Boolean
  End Function

  <System.Runtime.InteropServices.DllImport("User32.dll")> _
  Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
  End Function

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

  Public Function LoadLocationFromHistory() As Point
    Dim kSoftware As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("software", True)
    Dim kAmeebaa As RegistryKey = kSoftware.CreateSubKey("Ameebaa")
    Dim kPaired As RegistryKey = kAmeebaa.CreateSubKey("paired")
    Dim kAttr As RegistryKey = kAmeebaa.CreateSubKey("attr")
    Dim names() As String = kPaired.GetValueNames()
    Dim kDevice As RegistryKey = kAttr.CreateSubKey(mDeviceId)
    Dim posX As Integer = kDevice.GetValue("pos.x", -1)
    'If (posX < 0) Then
    'posX = 0
    'End If
    Dim posY As Integer = kDevice.GetValue("pos.y", -1)
    'If (posY < 0) Then
    'posY = 0
    'End If
    kDevice.Close()
    kAttr.Close()
    kPaired.Close()
    kAmeebaa.Close()
    kSoftware.Close()

    If (posX < 0 OrElse posY < 0) Then
      Return Point.Empty
    Else
      Return New Point(posX, posY)
    End If
  End Function

  Public Sub New(ByVal deviceId As String)
    mDeviceId = deviceId
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    'Debug.Print(My.Computer.Screen.WorkingArea.ToString())



    Dim savedLocation As Point = LoadLocationFromHistory()
    If (savedLocation = Point.Empty) Then
      Dim r As Rectangle = My.Computer.Screen.WorkingArea
      Me.Top = r.Bottom - Me.Bottom - 8
      Me.Left = r.Right - Me.Right - 8
    Else
      Me.Location = savedLocation
    End If
    Me.LabelMessage.Text = String.Empty
    Me.TopMost = True
  End Sub

  Private Sub NotiForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    StopShake()
    StopCounter()
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

  Private mMouseDowned As Boolean = False
  Private mFormMoving As Boolean = False
  Private Const WM_WINDOWPOSCHANGED As Integer = &H47

  Private Sub LabelTitle_MouseDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LabelTitle.MouseDown
    If (e.Button = MouseButtons.Left) Then
      ReleaseCapture()
      SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
      mMouseDowned = True
    End If
  End Sub

  Private Sub LabelTitle_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LabelTitle.MouseUp
    If (e.Button = MouseButtons.Left AndAlso mMouseDowned) Then
      mMouseDowned = False
    End If
  End Sub

  Private Sub SaveLocationToRegistry()
    Dim kSoftware As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("software", True)
    Dim kAmeebaa As RegistryKey = kSoftware.CreateSubKey("Ameebaa")
    Dim kAttr As RegistryKey = kAmeebaa.CreateSubKey("attr")
    Dim kDev As RegistryKey = kAttr.CreateSubKey(Me.mDeviceId)
    kDev.SetValue("pos.x", Me.Location.X, RegistryValueKind.DWord)
    kDev.SetValue("pos.y", Me.Location.Y, RegistryValueKind.DWord)
    kDev.Close()
    kAttr.Close()
    kAmeebaa.Close()
    kSoftware.Close()
  End Sub

  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    If (m.Msg = WM_WINDOWPOSCHANGED) Then
      SaveLocationToRegistry()
    End If
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

  Private Sub CloseButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton1.Click
    Me.Close()
  End Sub
End Class