Friend Enum TerrainType
    None
    Floor
    Ladder
End Enum
Friend Enum ItemType
    None
    Money
End Enum
Friend Enum CharacterType
    None
    Player
End Enum
Friend Class BoardCell
    Property TerrainType As TerrainType
    Property ItemType As ItemType
    Property CharacterType As CharacterType
    Property IsDirty As Boolean
End Class
Friend Class Board
    Private _rows As Integer
    Private _columns As Integer
    Private _cells As New List(Of BoardCell)
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

    Private Sub LoadData(data As IEnumerable(Of String))
        _rows = data.Count
        _columns = data.First.Count
        _cells.Clear()
        For Each line In data
            For Each character In line
                Dim cell As New BoardCell With
                    {
                        .TerrainType = TerrainType.None,
                        .ItemType = ItemType.None,
                        .CharacterType = CharacterType.None,
                        .IsDirty = True
                    }
                Select Case character
                    Case "="c
                        cell.TerrainType = TerrainType.Floor
                    Case "$"c
                        cell.ItemType = ItemType.Money
                    Case "@"c
                        cell.CharacterType = CharacterType.Player
                    Case "|"c
                        cell.TerrainType = TerrainType.Ladder
                End Select
                _cells.Add(cell)
            Next
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
        Dim fromCell = GetCell(position.Item1, position.Item2)
        Dim toCell = GetCell(nextPosition.Item1, nextPosition.Item2)
        Dim characterType = fromCell.CharacterType
        toCell.CharacterType = characterType
        toCell.IsDirty = True
        fromCell.CharacterType = CharacterType.None
        fromCell.IsDirty = True
    End Sub

    Private Function GetPlayerPosition() As (Integer, Integer)
        Dim index = _cells.FindIndex(Function(cell) cell.CharacterType = CharacterType.Player)
        Return (index Mod _columns, index \ _columns)
    End Function

    Friend Sub MovePlayerRight()
        Dim position As (Integer, Integer) = GetPlayerPosition()
        Dim nextPosition = (position.Item1 + 1, position.Item2)
        MoveCharacter(position, nextPosition)
    End Sub
End Class
