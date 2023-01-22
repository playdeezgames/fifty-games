Friend Class Encounter
    Implements IEncounter
    Private _worldData As WorldData
    Friend _data As EncounterData

    Public Sub New(worldData As WorldData, data As EncounterData)
        _worldData = worldData
        _data = data
    End Sub

    Public Sub PurgeCorpses() Implements IEncounter.PurgeCorpses
        _data.Enemies = _data.Enemies.Where(Function(x) x.Wounds < x.EnemyType.ToDescriptor.HitPoints).ToList
    End Sub

    Public Function CounterAttack(character As ICharacter, random As Random) As IEnumerable(Of String) Implements IEncounter.CounterAttack
        Dim messages As New List(Of String)
        For Each enemy In Enemies
            messages.AddRange(enemy.Attack(character, random))
            If character.IsDead Then
                messages.Add($"{character.Name} has been slain by {enemy.Name}.")
                Exit For
            End If
        Next
        Return messages
    End Function

    Public ReadOnly Property EncounterType As EncounterType Implements IEncounter.EncounterType
        Get
            Return _data.EncounterType
        End Get
    End Property

    Public ReadOnly Property Enemies As IEnumerable(Of IEnemy) Implements IEncounter.Enemies
        Get
            Return _data.Enemies.Select(Function(x) New Enemy(_worldData, x))
        End Get
    End Property
End Class
