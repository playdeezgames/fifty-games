Friend Interface ICharacter
    ReadOnly Property CharacterType As CharacterType
    Sub Take(item As IItem)
    Function CanTake(item As IItem) As Boolean
    ReadOnly Property Items As IEnumerable(Of IItem)
End Interface
