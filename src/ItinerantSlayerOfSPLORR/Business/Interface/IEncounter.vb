Public Interface IEncounter
    ReadOnly Property EncounterType As EncounterType
    ReadOnly Property Enemies As IEnumerable(Of IEnemy)
    Sub PurgeCorpses()
End Interface
