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

    Public Function CheckForEncounter(random As Random, x As Integer, y As Integer) As IEncounter Implements IBoard.CheckForEncounter
        Dim candidates = _data.EncounterZones.Where(Function(zone) x >= zone.Left AndAlso y >= zone.Top AndAlso x <= zone.Right AndAlso y < zone.Bottom)
        If Not candidates.Any Then
            Return Nothing
        End If
        Dim totalWeight = candidates.Sum(Function(zone) zone.Weight)
        Dim generated = random.Next(totalWeight)
        For Each candidate In candidates
            generated -= candidate.Weight
            If generated < 0 Then
                If candidate.EncounterZoneType.HasValue Then
                    Return New Encounter(_worldData, New EncounterData With {.EncounterType = candidate.EncounterZoneType.Value})
                End If
                Return Nothing
            End If
        Next
        Return Nothing
    End Function
End Class
