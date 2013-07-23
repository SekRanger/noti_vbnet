Public Class MainForm
  Private mPairForm As New PairingForm()
  Public Sub ClosePairForm()
    mPairForm.Close()
  End Sub
  Private Sub ButtonPair_Click(sender As System.Object, e As System.EventArgs) Handles ButtonPair.Click
    mPairForm.ShowDialog(Me)
  End Sub
End Class