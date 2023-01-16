Friend Enum TerrainType
    None
    Floor
    Ladder
    WalkedFloor
End Enum
Friend Enum ItemType
    None
    Money
End Enum
Friend Enum CharacterType
    None
    Player
End Enum
Friend Class Character
    Property CharacterType As CharacterType
End Class
Friend Class BoardCell
    Property TerrainType As TerrainType
    Property ItemType As ItemType
    Property Character As Character
    Property IsDirty As Boolean
    Property Column As Integer
    Property Row As Integer
End Class
Friend Class Board
    Private _rows As Integer
    Private _columns As Integer
    Private _cells As New List(Of BoardCell)
    Friend ReadOnly Property IsCompleted As Boolean
        Get
            Return Not _cells.Any(Function(cell) cell.TerrainType = TerrainType.Floor)
        End Get
    End Property
    Friend ReadOnly Property Rows As Integer
        Get
            Return _rows
        End Get
    End Property
    Friend ReadOnly Property Columns As Integer
        Get
            Return _columns
        End Get
    End Property
    Sub New(data As IEnumerable(Of String))
        LoadData(data)
    End Sub
    Friend Function GetCell(column As Integer, row As Integer) As BoardCell
        If Not IsInBounds(column, row) Then
            Return Nothing
        End If
        Return _cells(row * _columns + column)
    End Function
    Friend Function GetCell(position As (Integer, Integer)) As BoardCell
        Return GetCell(position.Item1, position.Item2)
    End Function

    Private Sub LoadData(data As IEnumerable(Of String))
        _rows = data.Count
        _columns = data.First.Count
        _cells.Clear()
        Dim row As Integer = 0
        For Each line In data
            Dim column As Integer = 0
            For Each character In line
                Dim cell As New BoardCell With
                    {
                        .TerrainType = TerrainType.None,
                        .ItemType = ItemType.None,
                        .Character = Nothing,
                        .IsDirty = True,
                        .Column = column,
                        .Row = row
                    }
                column += 1
                Select Case character
                    Case "="c
                        cell.TerrainType = TerrainType.Floor
                    Case "$"c
                        cell.ItemType = ItemType.Money
                    Case "@"c
                        cell.Character = New Character With {.CharacterType = CharacterType.Player}
                    Case "|"c
                        cell.TerrainType = TerrainType.Ladder
                End Select
                _cells.Add(cell)
            Next
            row += 1
        Next
    End Sub

    Friend Sub MovePlayerLeft()
        Dim position As (Integer, Integer) = GetPlayerPosition()
        Dim nextPosition = (position.Item1 - 1, position.Item2)
        MoveCharacter(position, nextPosition)
    End Sub
    Friend Function IsInBounds(column As Integer, row As Integer) As Boolean
        Return column >= 0 AndAlso row >= 0 AndAlso column < _columns AndAlso row < _rows
    End Function

    Private Sub MoveCharacter(position As (Integer, Integer), nextPosition As (Integer, Integer))
        If Not IsInBounds(position.Item1, position.Item2) Then
            Return
        End If
        If Not IsInBounds(nextPosition.Item1, nextPosition.Item2) Then
            Return
        End If
        Dim fromCell = GetCell(position)
        Dim toCell = GetCell(nextPosition)
        Dim character = fromCell.Character
        toCell.Character = character
        toCell.IsDirty = True
        fromCell.Character = Nothing
        fromCell.IsDirty = True
    End Sub

    Friend Sub UpdateCharacters()
        Dim characterCells = _cells.Where(Function(x) x.Character IsNot Nothing)
        For Each characterCell In characterCells
            UpdateCharacterCell(characterCell)
        Next
    End Sub

    Private Sub UpdateCharacterCell(characterCell As BoardCell)
        If characterCell.Character.CharacterType <> CharacterType.Player Then
            Return
        End If
        Dim underCell = GetCell(characterCell.Column, characterCell.Row + 1)
        If underCell IsNot Nothing Then
            If underCell.TerrainType = TerrainType.Floor Then
                underCell.TerrainType = TerrainType.WalkedFloor
                underCell.IsDirty = True
            End If
        End If
    End Sub

    Private Function GetPlayerPosition() As (Integer, Integer)
        Dim index = _cells.FindIndex(Function(cell) cell.Character IsNot Nothing AndAlso cell.Character.CharacterType = CharacterType.Player)
        Return (index Mod _columns, index \ _columns)
    End Function

    Friend Sub MovePlayerRight()
        Dim position As (Integer, Integer) = GetPlayerPosition()
        Dim nextPosition = (position.Item1 + 1, position.Item2)
        MoveCharacter(position, nextPosition)
    End Sub

    Friend Sub MovePlayerUp()
        Dim position As (Integer, Integer) = GetPlayerPosition()
        Dim cell = GetCell(position.Item1, position.Item2)
        If cell.TerrainType <> TerrainType.Ladder Then
            Return
        End If
        Dim nextPosition = (position.Item1, position.Item2 - 1)
        MoveCharacter(position, nextPosition)
    End Sub

    Friend Sub MovePlayerDown()
        Dim position As (Integer, Integer) = GetPlayerPosition()
        Dim nextPosition = (position.Item1, position.Item2 + 1)
        Dim cell = GetCell(nextPosition.Item1, nextPosition.Item2)
        If cell.TerrainType <> TerrainType.Ladder Then
            Return
        End If
        MoveCharacter(position, nextPosition)
    End Sub
End Class
