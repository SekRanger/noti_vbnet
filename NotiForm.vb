Public Class NotiForm
  Inherits Form

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
End Class