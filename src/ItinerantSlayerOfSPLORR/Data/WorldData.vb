Public Class WorldData
    Public Property PlayerData As PlayerData
    Public Property Boards As New List(Of BoardData)
    Public Property Encounter As EncounterData
    Public Property Flags As New HashSet(Of String)
End Class
