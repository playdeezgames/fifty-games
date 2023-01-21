Friend Interface IBoard
    Function GetCell(column As Integer, row As Integer) As IBoardCell
    Function CheckForEncounter(random As Random, x As Integer, y As Integer) As IEncounter
    ReadOnly Property DefaultTerrain As TerrainType
    ReadOnly Property BoardIndex As Integer
End Interface
