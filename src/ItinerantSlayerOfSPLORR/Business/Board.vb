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
                If candidate.EncounterType.HasValue Then
                    Return GenerateEncounter(random, candidate.EncounterType.Value)
                End If
                Return Nothing
            End If
        Next
        Return Nothing
    End Function

    Private Function GenerateEncounter(random As Random, encounterType As EncounterType) As IEncounter
        Dim encounterData = New EncounterData With {.EncounterType = encounterType}
        Dim descriptor = encounterType.ToDescriptor()
        Dim enemyType = descriptor.EnemyType
        Dim enemyCount = descriptor.Generator.Generate(random)
        While encounterData.Enemies.Count < enemyCount
            encounterData.Enemies.Add(New EnemyData With {.EnemyType = enemyType, .Wounds = 0})
        End While
        Return New Encounter(_worldData, encounterData)
    End Function
End Class
