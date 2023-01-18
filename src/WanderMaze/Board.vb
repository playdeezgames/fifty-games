Friend Enum BoardCell
    None
    Dude
    Wall
    Fire
    Water
    Money
End Enum
Friend Class Board
    Friend Const Columns = 50
    Friend Const Rows = 25
    Private _cells As New List(Of BoardCell)
    Private _level As Integer
    Private _water As Integer
    Private _score As Integer
    Private _moves As Integer
    Private random As New Random
    Friend ReadOnly Property Level As Integer
        Get
            Return _level + 1
        End Get
    End Property
    Sub New()
        _level = 0
        ResetLevel()
    End Sub
    Private Const MoneyCount = 32
    Private Const FireCount = 64
    Private Const MaximumFire = 256
    Private Const FireIncrement = 16
    Private Const WallCount = 16
    Private Const MaximumWall = 64
    Private Const WallIncrement = 4
    Private Const WaterCount = 2
    Private Const MaximumWater = 8
    Private Const WaterIncrement = 1
    ReadOnly Property Score As Integer
        Get
            Return _score
        End Get
    End Property
    Private Sub ResetLevel()
        _cells.Clear()
        While _cells.Count < Columns * Rows
            _cells.Add(BoardCell.None)
        End While
        Place(MoneyCount, BoardCell.Money)
        Place(Math.Min(MaximumFire, FireCount + FireIncrement * _level), BoardCell.Fire)
        Place(Math.Min(MaximumWall, WallCount + WallIncrement * _level), BoardCell.Wall)
        Place(Math.Min(MaximumWater, WaterCount + WaterIncrement * _level), BoardCell.Water)
        Place(1, BoardCell.Dude)
    End Sub

    Private Sub Place(count As Integer, cell As BoardCell)
        While count > 0
            Dim column = random.Next(Columns)
            Dim row = random.Next(Rows)
            If GetCell(column, row) = BoardCell.None Then
                count -= 1
                SetCell(column, row, cell)
            End If
        End While
    End Sub

    Private Sub SetCell(column As Integer, row As Integer, cell As BoardCell)
        If column >= 0 AndAlso column < Columns AndAlso row >= 0 AndAlso row < Rows Then
            _cells(column + row * Columns) = cell
        End If
    End Sub

    Friend ReadOnly Property IsGameOver As Boolean
        Get
            Return _cells.All(Function(x) x <> BoardCell.Dude)
        End Get
    End Property
    Private Function GetCell(column As Integer, row As Integer) As BoardCell
        If column >= 0 AndAlso column < Columns AndAlso row >= 0 AndAlso row < Rows Then
            Return _cells(column + row * Columns)
        End If
        Return BoardCell.None
    End Function

    Friend Sub Draw()
        For row = 0 To Rows - 1
            For column = 0 To Columns - 1
                Select Case GetCell(column, row)
                    Case BoardCell.None
                        AnsiConsole.Markup("[black on teal] [/]")
                    Case BoardCell.Dude
                        AnsiConsole.Markup("[purple on teal]@[/]")
                    Case BoardCell.Water
                        AnsiConsole.Markup("[navy on teal]+[/]")
                    Case BoardCell.Fire
                        AnsiConsole.Markup("[maroon on red]~[/]")
                    Case BoardCell.Wall
                        AnsiConsole.Markup("[black on grey]#[/]")
                    Case BoardCell.Money
                        AnsiConsole.Markup("[yellow on teal]$[/]")
                End Select
            Next
            AnsiConsole.WriteLine()
        Next
        AnsiConsole.MarkupLine($"Level: {Level} | Score: {_score} | Water: {_water} | Remaining: {Remaining}                                     ")
        If _moves > 0 Then
            AnsiConsole.MarkupLine($"Moves: {_moves} | Points Per Move: {_score / _moves:f}                ")
        End If
    End Sub
    ReadOnly Property Remaining As Integer
        Get
            Return _cells.Where(Function(x) x = BoardCell.Money).Count
        End Get
    End Property

    Friend Sub Move(deltaX As Integer, deltaY As Integer)
        Dim index = _cells.IndexOf(BoardCell.Dude)
        If index = -1 Then
            Return
        End If
        Dim column = index Mod Columns
        Dim row = index \ Columns
        Dim nextColumn = column + deltaX
        Dim nextRow = row + deltaY
        If nextColumn < 0 OrElse nextRow < 0 OrElse nextColumn >= Columns OrElse nextRow >= Rows Then
            Return
        End If
        Select Case GetCell(nextColumn, nextRow)
            Case BoardCell.Wall
                Return
            Case BoardCell.Fire
                _moves += 1
                SetCell(column, row, BoardCell.Fire)
                If _water > 0 Then
                    _water -= 1
                    SetCell(nextColumn, nextRow, BoardCell.Dude)
                End If
            Case BoardCell.Water
                _moves += 1
                SetCell(column, row, BoardCell.Fire)
                SetCell(nextColumn, nextRow, BoardCell.Dude)
                _water += 1
            Case BoardCell.None
                _moves += 1
                SetCell(column, row, BoardCell.Fire)
                SetCell(nextColumn, nextRow, BoardCell.Dude)
            Case BoardCell.Money
                _moves += 1
                SetCell(column, row, BoardCell.Fire)
                SetCell(nextColumn, nextRow, BoardCell.Dude)
                _score += 1
                If Remaining = 0 Then
                    _level += 1
                    ResetLevel()
                End If
        End Select
    End Sub
End Class
