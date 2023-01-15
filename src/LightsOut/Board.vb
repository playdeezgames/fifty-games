Public Class Board
    Private _cells As List(Of Boolean)
    Private _columns As Integer
    Private _rows As Integer
    Sub New(columns As Integer, rows As Integer)
        _columns = columns
        _rows = rows
        Clear()
    End Sub
    Sub Clear()
        _cells = New List(Of Boolean)
        While _cells.Count < _columns * _rows
            _cells.Add(False)
        End While
    End Sub
    Function IsLit(column As Integer, row As Integer) As Boolean
        If column < 0 OrElse row < 0 OrElse column >= _columns OrElse row >= _rows Then
            Return False
        End If
        Return _cells(column + row * _columns)
    End Function
    Sub Toggle(column As Integer, row As Integer)
        If column < 0 OrElse row < 0 OrElse column >= _columns OrElse row >= _rows Then
            Return
        End If
        _cells(column + row * _columns) = Not _cells(column + row * _columns)
    End Sub
    Function AnyLit() As Boolean
        Return _cells.Any(Function(cell) cell)
    End Function
    Sub MakeMove(column As Integer, row As Integer)
        Toggle(column, row)
        Toggle(column + 1, row)
        Toggle(column - 1, row)
        Toggle(column, row + 1)
        Toggle(column, row - 1)
    End Sub
    Sub GenerateBoard(level As Integer, random As Random)
        Dim chosen As New HashSet(Of (Integer, Integer))
        For index = 1 To level
            Dim column = random.Next(0, _columns)
            Dim row = random.Next(0, _rows)
            Do While chosen.Contains((column, row))
                column = random.Next(0, _columns)
                row = random.Next(0, _rows)
            Loop
            chosen.Add((column, row))
            MakeMove(column, row)
        Next
    End Sub
End Class
