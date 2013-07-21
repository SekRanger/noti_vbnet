Public Class AndroidUdpBroadMsg
  Public DeviceId As String
  Public Model As String
  Public MsgKey As String
  Public Msg As String

  Public Sub New(raw As String)
    Dim arr() As String = raw.Split(Chr(8))
    DeviceId = arr(0)
    Model = arr(1)
    MsgKey = arr(2)
    Msg = arr(3)
  End Sub
End Class
