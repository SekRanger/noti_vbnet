Public Class PairingForm
  Private mCountDown As Integer = 10

  Private Sub CountDown()
    mCountDown -= 1
    LabelTimeout.Text = mCountDown.ToString()
    If (mCountDown = 0) Then
      Me.Close()
    End If
  End Sub

  Private Sub PairingForm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Ctx.SetPairing(False)
  End Sub

  Private Sub PairingForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    LabelTimeout.Text = mCountDown.ToString()
    Timer1.Start()
  End Sub

  Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
    CountDown()
  End Sub

  Public Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    Ctx.SetPairing(True)
  End Sub
End Class