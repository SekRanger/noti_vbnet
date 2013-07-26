Imports System.Windows.Forms.VisualStyles
Public Class CloseButton
  Inherits Control
  Private mRp As New VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Pressed)
  Private mRh As New VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Hot)
  Private mRn As New VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Normal)
  Private mEntered As Boolean = False
  Private mClicked As Boolean = False

  Public Sub New()
    DoubleBuffered = True
    Cursor = Cursors.Hand
    'BackColor = Color.Transparent
  End Sub

  Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
    'MyBase.OnMouseEnter(e)
    mEntered = True
    Invalidate()
  End Sub

  Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
    'MyBase.OnMouseLeave(e)
    mEntered = False
    Invalidate()
  End Sub

  Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
    'MyBase.OnMouseDown(e)

    mClicked = mEntered AndAlso e.Button.Equals(MouseButtons.Left)
    Invalidate()
  End Sub

  Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
    If (mClicked AndAlso mEntered AndAlso e.Button.Equals(MouseButtons.Left)) Then
      mClicked = False
      Invalidate()
    End If
  End Sub

  Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    MyBase.OnPaint(e)
    If (mEntered) Then
      If (mClicked) Then
        mRp.DrawBackground(e.Graphics, Me.DisplayRectangle)
      Else
        mRh.DrawBackground(e.Graphics, Me.DisplayRectangle)
      End If
    Else
      mRn.DrawBackground(e.Graphics, Me.DisplayRectangle)
    End If
    'mRn.DrawParentBackground(e.Graphics, Me.DisplayRectangle, Me.Parent)
  End Sub
End Class
