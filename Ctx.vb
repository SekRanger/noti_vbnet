Imports System.Threading
Imports Microsoft.Win32

Public Class Ctx
  Inherits ApplicationContext

  Private WithEvents c As New Component1()
  Private mSocket As System.Net.Sockets.UdpClient = Nothing
  Private mMainForm As New MainForm()
  Private Shared mPairingLock As New Object()
  Public Shared Pairing As Boolean = False
  Private mShouldClose As Boolean = False

  Public Shared Sub SetPairing(b As Boolean)
    SyncLock (mPairingLock)
      Pairing = b
    End SyncLock
  End Sub

  Public Shared Function GetPairing() As Boolean
    SyncLock (mPairingLock)
      Return Pairing
    End SyncLock
  End Function

  Private Sub ToolStripMenuItem1_Click(sender As Object, e As System.EventArgs)
    mShouldClose = True
    Dim keys As New List(Of String)

    For Each k As String In mDictForm.Keys
      keys.Add(k)
    Next

    For Each k As String In keys
      Try
        mDictForm(k).CloseMe()
      Catch ex As Exception
        Debug.Print(ex.Message)
      End Try
    Next
    Me.ExitThread()
  End Sub

  Private Sub FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs)
    e.Cancel = True
    If (mShouldClose) Then
      e.Cancel = False
    End If

    If (e.Cancel) Then
      CType(sender, NotiForm).Toggle(False)
    Else
      mDictForm.Remove(CType(sender, NotiForm).DeviceId)
    End If
  End Sub

  Private Sub NtfIcon_Doubleclick(sender As Object, e As System.EventArgs)
    mMainForm.Visible = Not mMainForm.Visible
  End Sub

  Dim mDictForm As New Dictionary(Of String, NotiForm)

  Private Sub ShowMsg(msg As Object)
    Dim m As New AndroidUdpBroadMsg(CType(msg, String))
    Dim f As NotiForm

    If (Not mDictForm.ContainsKey(m.DeviceId)) Then
      Debug.Print(m.DeviceId)
      f = New NotiForm(m.DeviceId)
      mDictForm.Add(m.DeviceId, f)
      f.TopMost = True

      f.ProcessThisShit(m)

      AddHandler f.FormClosing, AddressOf FormClosing
      Application.Run(f)
    Else
      f = mDictForm(m.DeviceId)
      f.ProcessThisShit(m)
      f.Toggle(True)
    End If
  End Sub

  Private Sub AddNewDevice(msg As String)
    Try
      Dim m As New AndroidUdpBroadMsg(msg)
      Dim kSoftware As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("software", True)
      Dim kAmeebaa As RegistryKey = kSoftware.CreateSubKey("Ameebaa")
      Dim kPaired As RegistryKey = kAmeebaa.CreateSubKey("paired")
      kPaired.SetValue(m.DeviceId, m.Model, RegistryValueKind.String)
      kPaired.Close()
      kAmeebaa.Close()
      kSoftware.Close()
    Catch ex As Exception
    End Try
  End Sub

  Private Sub MessageLoop()

    Dim bytes() As Byte
    Try
      Do
        mSocket = New System.Net.Sockets.UdpClient(60069)

        Debug.Print("Wait for data")
        bytes = mSocket.Receive(New System.Net.IPEndPoint(System.Net.IPAddress.Any, 60069))
        mSocket.Close()
        Dim msg As String = System.Text.UTF8Encoding.UTF8.GetString(bytes)
        Dim arr() As String = msg.Split(Chr(8))
        If ((arr.Length >= 2) AndAlso (arr(2) = "PAIR")) Then
          If (GetPairing()) Then
            Ctx.SetPairing(False)
            mMainForm.ClosePairForm()
            AddNewDevice(msg)
          Else
            Continue Do
          End If
        End If
        Dim t As New Thread(AddressOf ShowMsg)
        t.Start(msg)
      Loop
    Catch ex As Exception
      Try
        mSocket.Close()
      Catch ex1 As Exception
      End Try
    End Try
  End Sub

  Public Sub Init()
    AddHandler c.ToolStripMenuItem1.Click, AddressOf ToolStripMenuItem1_Click
    AddHandler c.NtfIcon.DoubleClick, AddressOf NtfIcon_Doubleclick

    Dim kSoftware As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("software", True)
    Dim kAmeebaa As RegistryKey = kSoftware.CreateSubKey("Ameebaa")
    Dim kPaired As RegistryKey = kAmeebaa.CreateSubKey("paired")
    Dim names() As String = kPaired.GetValueNames()
    For Each n As String In names
      Dim model As String = kPaired.GetValue(n, n)
      Dim msg As New AndroidUdpBroadMsg(n, model)
      Dim t As New Thread(AddressOf ShowNoti)
      t.Start(msg)
    Next
    kPaired.Close()
    kAmeebaa.Close()
    kSoftware.Close()
  End Sub

  Private Sub ShowNoti(msg As Object)
    Dim m As AndroidUdpBroadMsg = CType(msg, AndroidUdpBroadMsg)
    Dim f As NotiForm
    If (Not mDictForm.ContainsKey(m.DeviceId)) Then
      f = New NotiForm(m.DeviceId)
      Debug.Print("mDictForm is nothing: " & (mDictForm Is Nothing))
      Debug.Print("m is nothing: " & (m Is Nothing))
      Debug.Print("f is nothing: " & (f Is Nothing))
      mDictForm.Add(m.DeviceId, f)
      f.SetLabelTitle(m.Model)

      f.TopMost = True
      AddHandler f.FormClosing, AddressOf FormClosing
      Application.Run(f)
    Else
      f = mDictForm(m.Model)
      f.Toggle(True)
    End If
  End Sub

  Public Sub New()
    Init()
    Dim tLoop As New Thread(AddressOf MessageLoop)
    tLoop.Start()
  End Sub

  Private Sub Ctx_ThreadExit(sender As Object, e As System.EventArgs) Handles Me.ThreadExit
    c.Dispose()
    
    Try
      mSocket.Close()
    Catch ex As Exception
    End Try
  End Sub
End Class