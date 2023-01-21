Friend Class Encounter
    Implements IEncounter
    Private _worldData As WorldData
    Friend _data As EncounterData

    Public Sub New(worldData As WorldData, data As EncounterData)
        _worldData = worldData
        _data = data
    End Sub

    Public ReadOnly Property EncounterType As EncounterType Implements IEncounter.EncounterType
        Get
            Return _data.EncounterType
        End Get
    End Property
End Class
