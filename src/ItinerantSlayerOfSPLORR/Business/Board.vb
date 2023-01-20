Friend Class Board
    Implements IBoard
    Private _worldData As WorldData
    Private _data As BoardData
    Private _boardIndex As Integer
    Sub New(worldData As WorldData, boardIndex As Integer, data As BoardData)
        _worldData = worldData
        _data = data
        _boardIndex = boardIndex
    End Sub

    Public ReadOnly Property DefaultTerrain As TerrainType Implements IBoard.DefaultTerrain
        Get
            Return _data.DefaultTerrain
        End Get
    End Property

    Public ReadOnly Property BoardIndex As Integer Implements IBoard.BoardIndex
        Get
            Return _boardIndex
        End Get
    End Property

    Public Function GetCell(column As Integer, row As Integer) As IBoardCell Implements IBoard.GetCell
        If column < 0 OrElse column >= _data.BoardColumns.Count Then
            Return Nothing
        End If
        If row < 0 OrElse row >= _data.BoardColumns(column).Cells.Count Then
            Return Nothing
        End If
        Return New BoardCell(_worldData, _data.BoardColumns(column).Cells(row))
    End Function
End Class
