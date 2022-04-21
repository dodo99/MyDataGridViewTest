Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class DataGridViewMergedTextBoxColumn
    Inherits DataGridViewTextBoxColumn

    Public Sub New()
        CellTemplate = New DataGridViewMergedTextBoxCell()
    End Sub

    Private Class DataGridViewMergedTextBoxCell
        Inherits DataGridViewTextBoxCell

        Private Function IsRepeatedCellValue(ByVal rowIndex As Integer, ByVal colIndex As Integer) As Boolean
            If rowIndex = 0 Then Return False
            Dim currCell As DataGridViewCell = Me.DataGridView.Rows(rowIndex).Cells(colIndex)
            Dim prevCell As DataGridViewCell = Me.DataGridView.Rows(rowIndex - 1).Cells(colIndex)
            Return Object.Equals(currCell.Value, prevCell.Value)
        End Function

        Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, ByVal valueTypeConverter As TypeConverter, ByVal formattedValueTypeConverter As TypeConverter, ByVal context As DataGridViewDataErrorContexts) As Object
            If rowIndex > 0 AndAlso IsRepeatedCellValue(rowIndex, Me.ColumnIndex) Then
                Return String.Empty
            Else
                Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
            End If
        End Function

        Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal cellState As DataGridViewElementStates, ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, ByVal paintParts As DataGridViewPaintParts)
            If rowIndex < Me.DataGridView.Rows.Count - 1 Then
                If IsRepeatedCellValue(rowIndex + 1, Me.ColumnIndex) Then
                    advancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None
                End If
            End If

            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)
        End Sub
    End Class
End Class

