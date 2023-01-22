Friend Interface ICharacter
    ReadOnly Property CharacterType As CharacterType
    Function RollAttack(random As Random) As Integer
    Sub AddXP(amount As Integer)
    ReadOnly Property Name As String
    ReadOnly Property XP As Integer
End Interface
