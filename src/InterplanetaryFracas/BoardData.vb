Public Class BoardData
    Public Property Columns As Integer
    Public Property Rows As Integer
    Public Property Cells As New List(Of BoardCellData)

    Friend Sub UpdateVisibility()
        For Each cell In Cells
            cell.IsVisible = False
        Next
        For column = 0 To Columns - 1
            For row = 0 To Rows - 1
                Dim cell = GetCell(column, row)
                If cell.HasPlayerShips Then
                    MakeVisible(column - 1, row - 1, column + 1, row + 1)
                End If
            Next
        Next
    End Sub

    Private Sub MakeVisible(firstColumn As Integer, firstRow As Integer, lastColumn As Integer, lastRow As Integer)
        For column = firstColumn To lastColumn
            For row = firstRow To lastRow
                Dim cell = GetCell(column, row)
                If cell IsNot Nothing Then
                    cell.IsVisible = True
                End If
            Next
        Next
    End Sub

    Function GetCell(column As Integer, row As Integer) As BoardCellData
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return Cells(row * Columns + column)
    End Function
End Class
