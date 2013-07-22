Public Class MainForm
  Private Sub ButtonPair_Click(sender As System.Object, e As System.EventArgs) Handles ButtonPair.Click
    Dim pf As New PairingForm()
    pf.ShowDialog(Me)
  End Sub
End Class