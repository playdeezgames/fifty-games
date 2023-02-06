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

    Friend Sub EndTurn()
        For Each cell In Cells
            cell.EndTurn()
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
        If Not HasCell(column, row) Then
            Return Nothing
        End If
        Return Cells(row * Columns + column)
    End Function
    Friend Function GetMovableShipLocations() As IEnumerable(Of (Integer, Integer))
        Dim result As New List(Of (Integer, Integer))
        For column = 0 To Columns - 1
            For row = 0 To Rows - 1
                Dim cell = GetCell(column, row)
                If cell.HasMovablePlayerShips Then
                    result.Add((column, row))
                End If
            Next
        Next
        Return result
    End Function

    Friend Function GetFirableShipLocations() As IEnumerable(Of (Integer, Integer))
        Dim result As New List(Of (Integer, Integer))
        For column = 0 To Columns - 1
            For row = 0 To Rows - 1
                Dim cell = GetCell(column, row)
                If cell.HasFirablePlayerShips Then
                    result.Add((column, row))
                End If
            Next
        Next
        Return result
    End Function

    Friend Function HasCell(column As Integer, row As Integer) As Boolean
        Return column >= 0 AndAlso row >= 0 AndAlso column < Columns AndAlso row < Rows
    End Function
End Class
