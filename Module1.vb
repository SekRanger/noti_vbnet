Module Module1
  Private f As New Form1()

  Public Sub rrr()
    Debug.Print(f.InvokeRequired)
  End Sub

  Public Sub aaa()
    Do
      Debug.Print(f.InvokeRequired)
      f.Visible = Not f.Visible
      Threading.Thread.Sleep(1000)
    Loop
  End Sub

  Private Delegate Sub ToggleDelegate(b As Boolean)
  Public Sub Toggle(b As Boolean)
    If (f.InvokeRequired) Then
      Debug.Print(f.InvokeRequired)
      Dim cb As New ToggleDelegate(AddressOf Toggle)
      f.Invoke(cb, b)
    Else
      f.Visible = b
    End If
  End Sub

  Private Sub BgWk_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
    Dim bytes() As Byte
    Do Until e.Cancel = True
      Dim u As New System.Net.Sockets.UdpClient(60069)
      'System.Net.IPAddress.Parse("10.33.42.98")
      bytes = u.Receive(New System.Net.IPEndPoint(System.Net.IPAddress.Any, 0))
      u.Close()
      Toggle(True)

      Dim msg As String = System.Text.UTF8Encoding.UTF8.GetString(bytes)
      f.SetLabel(msg)
    Loop
  End Sub

  Sub main()
    Dim t As New Threading.Thread(New Threading.ThreadStart(AddressOf rrr))
    t.Start()

    Dim tt As New Threading.Thread(New Threading.ThreadStart(AddressOf aaa))
    tt.Start()
  End Sub
End Module
