Public Interface IEncounter
    ReadOnly Property EncounterType As EncounterType
    ReadOnly Property Enemies As IEnumerable(Of IEnemy)
    Sub PurgeCorpses()
    Function CounterAttack(character As ICharacter, random As Random) As IEnumerable(Of String)
End Interface
