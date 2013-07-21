Imports System.Threading

Public Class Ctx
  Inherits ApplicationContext

  Private WithEvents c As New Component1()
  Private mSocket As System.Net.Sockets.UdpClient = Nothing

  Private Sub ToolStripMenuItem1_Click(sender As Object, e As System.EventArgs)
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
    mDictForm.Remove(CType(sender, MainForm).DeviceId)
  End Sub

  Dim mDictForm As New Dictionary(Of String, MainForm)

  Private Sub ShowMsg(msg As Object)
    Dim m As New AndroidUdpBroadMsg(CType(msg, String))
    Dim f As MainForm

    If (Not mDictForm.ContainsKey(m.DeviceId)) Then
      f = New MainForm(m.DeviceId)
      mDictForm.Add(m.DeviceId, f)
      f.SetLabelTitle(m.Model)
      f.SetLabelMsg(m.Msg)
      f.TopMost = True
      AddHandler f.FormClosing, AddressOf FormClosing
      Application.Run(f)
    Else
      f = mDictForm(m.DeviceId)
      f.Toggle(True)
    End If
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

  End Sub

  Public Sub New()
    Init()
    Dim t As New Thread(AddressOf MessageLoop)
    t.Start()
  End Sub

  Private Sub Ctx_ThreadExit(sender As Object, e As System.EventArgs) Handles Me.ThreadExit
    c.Dispose()
    
    Try
      mSocket.Close()
    Catch ex As Exception
    End Try
  End Sub
End Class
