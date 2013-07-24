Public Class MainForm
  Private mPairForm As New PairingForm()

  Private Delegate Sub ClosePairFormDelegate()
  Public Sub ClosePairForm()
    If (mPairForm.InvokeRequired) Then
      mPairForm.Invoke(New ClosePairFormDelegate(AddressOf ClosePairForm))
    Else
      mPairForm.Close()
    End If
  End Sub
  Private Sub ButtonPair_Click(sender As System.Object, e As System.EventArgs) Handles ButtonPair.Click
    mPairForm.ShowDialog(Me)
  End Sub
End Class