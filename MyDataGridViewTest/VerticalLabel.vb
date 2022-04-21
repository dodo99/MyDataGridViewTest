
Option Strict On

Imports System.ComponentModel

Public Class VerticalLabel
    Inherits UserControl

    Private mintAngle As Integer = 90
    Private mdblRadians As Double = Math.PI / 2
    Private mintQuadrant As Integer = 2
    Private mintAlignment As ContentAlignment = ContentAlignment.MiddleCenter

    <Category("Appearance")>
    <Description("Indicates the angle to which the text will be displayed.")>
    Public Property Angle As Integer
        Get
            Return mintAngle
        End Get
        Set(ByVal pValue As Integer)
            mintAngle = ((pValue Mod 360) + 360) Mod 360 'range must be in 0-360 degrees.
            mdblRadians = Math.PI * mintAngle / 180.0
            CalculateQuadrant()
            Refresh()
        End Set
    End Property

    <Category("Appearance")>
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal pValue As String)
            MyBase.Text = pValue
            Refresh()
        End Set
    End Property

    <Category("Appearance")>
    <Description("Indicates how the text should be aligned.")>
    Public Property TextAlign As ContentAlignment
        Get
            Return mintAlignment
        End Get
        Set(ByVal pValue As ContentAlignment)
            mintAlignment = pValue
            Refresh()
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal paintEventArgs As PaintEventArgs)
        'Calculate the text size.
        Dim textSize As SizeF = paintEventArgs.Graphics.MeasureString(Text, Font, Parent.Width)

        Dim x As Integer = Math.Abs(CInt(Math.Ceiling(textSize.Height * Math.Sin(mdblRadians))))
        Dim y As Integer = Math.Abs(CInt(Math.Ceiling(textSize.Height * Math.Cos(mdblRadians))))
        Dim rotatedHeight As Point = New Point(x, y)

        x = Math.Abs(CInt(Math.Ceiling(textSize.Width * Math.Cos(mdblRadians))))
        y = Math.Abs(CInt(Math.Ceiling(textSize.Width * Math.Sin(mdblRadians))))
        Dim rotatedWidth As Point = New Point(x, y)

        Dim textBoundingBox As Size = New Size(rotatedWidth.X + rotatedHeight.X, rotatedWidth.Y + rotatedHeight.Y)
        SetControlSize(textBoundingBox)

        Dim rotationOffset As Point = CalculateOffsetForRotation(rotatedHeight, rotatedWidth, textBoundingBox)
        Dim alignmentOffset As Point = CalculateOffsetForAlignment(textBoundingBox)

        'Apply the transformation and rotation to the graphics.
        paintEventArgs.Graphics.TranslateTransform(rotationOffset.X + alignmentOffset.X, rotationOffset.Y + alignmentOffset.Y)
        paintEventArgs.Graphics.RotateTransform(mintAngle)

        paintEventArgs.Graphics.DrawString(Text, Font, New SolidBrush(ForeColor), 0F, 0F)
        MyBase.OnPaint(paintEventArgs)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Refresh()
    End Sub


    Private Function CalculateOffsetForRotation(ByRef pRotatedHeight As Point, ByRef pRotatedWidth As Point, ByRef pTextBoundingBox As Size) As Point
        Dim offset As Point = New Point(0, 0)

        'case 2
        offset.X = pTextBoundingBox.Width
        offset.Y = pRotatedHeight.Y

        'Select Case mintQuadrant
        '    Case 1
        '        offset.X = pRotatedHeight.X
        '    Case 2
        '        offset.X = pTextBoundingBox.Width
        '        offset.Y = pRotatedHeight.Y
        '    Case 3
        '        offset.X = pRotatedWidth.X
        '        offset.Y = pTextBoundingBox.Height
        '    Case 4
        '        offset.Y = pRotatedWidth.Y
        'End Select

        Return offset
    End Function

    Private Function CalculateOffsetForAlignment(ByRef pTextBoundingBox As Size) As Point
        Dim offset As Point = New Point(0, 0)

        Select Case mintAlignment
            Case ContentAlignment.TopLeft
                'nothing to do
            Case ContentAlignment.TopCenter
                offset.X = CInt((0.5 * Width - 0.5 * pTextBoundingBox.Width))
            Case ContentAlignment.TopRight
                offset.X = (Width - pTextBoundingBox.Width)
            Case ContentAlignment.MiddleLeft
                offset.Y = CInt((0.5 * Height - 0.5 * pTextBoundingBox.Height))
            Case ContentAlignment.MiddleCenter
                offset.X = CInt((0.5 * Width - 0.5 * pTextBoundingBox.Width))
                offset.Y = CInt((0.5 * Height - 0.5 * pTextBoundingBox.Height))
            Case ContentAlignment.MiddleRight
                offset.X = (Width - pTextBoundingBox.Width)
                offset.Y = CInt((0.5 * Height - 0.5 * pTextBoundingBox.Height))
            Case ContentAlignment.BottomLeft
                offset.Y = (Height - pTextBoundingBox.Height)
            Case ContentAlignment.BottomCenter
                offset.X = CInt((0.5 * Width - 0.5 * pTextBoundingBox.Width))
                offset.Y = (Height - pTextBoundingBox.Height)
            Case ContentAlignment.BottomRight
                offset.X = (Width - pTextBoundingBox.Width)
                offset.Y = (Height - pTextBoundingBox.Height)
        End Select

        Return offset
    End Function

    Private Sub SetControlSize(ByVal pTextBoundingBox As Size)
        If DesignMode Then Return
        If Not AutoSize Then Return

        Width = pTextBoundingBox.Width
        Height = pTextBoundingBox.Height
    End Sub

    Private Sub CalculateQuadrant()
        'If mintAngle >= 0 AndAlso mintAngle < 90 Then
        '    mintQuadrant = 1
        'ElseIf mintAngle < 180 Then
        '    mintQuadrant = 2
        'ElseIf mintAngle < 270 Then
        '    mintQuadrant = 3
        'ElseIf mintAngle < 360 Then
        '    mintQuadrant = 4
        'Else
        '    mintQuadrant = 0
        'End If
        mintQuadrant = 2
    End Sub

End Class
