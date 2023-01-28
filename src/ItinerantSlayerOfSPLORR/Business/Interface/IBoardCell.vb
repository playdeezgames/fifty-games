Friend Interface IBoardCell
    ReadOnly Property Terrain As TerrainType
    Property Character As ICharacter
    ReadOnly Property Triggers As IEnumerable(Of ITrigger)
End Interface
