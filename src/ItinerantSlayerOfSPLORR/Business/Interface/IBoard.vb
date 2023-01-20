Friend Interface IBoard
    Function GetCell(column As Integer, row As Integer) As IBoardCell
    ReadOnly Property DefaultTerrain As TerrainType
    ReadOnly Property BoardIndex As Integer
End Interface
