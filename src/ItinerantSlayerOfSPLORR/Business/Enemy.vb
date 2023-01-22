Friend Class Enemy
    Implements IEnemy
    Private _worldData As WorldData
    Private _data As EnemyData

    Public Sub New(worldData As WorldData, x As EnemyData)
        _worldData = worldData
        _data = x
    End Sub

    Public ReadOnly Property Name As String Implements IEnemy.Name
        Get
            Return _data.EnemyType.ToDescriptor().Name
        End Get
    End Property

    Public ReadOnly Property HitPoints As Integer Implements IEnemy.HitPoints
        Get
            Return Math.Clamp(MaximumHitPoints - _data.Wounds, 0, MaximumHitPoints)
        End Get
    End Property

    Public ReadOnly Property MaximumHitPoints As Integer Implements IEnemy.MaximumHitPoints
        Get
            Return _data.EnemyType.ToDescriptor().HitPoints
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements IEnemy.IsDead
        Get
            Return HitPoints = 0
        End Get
    End Property

    Public ReadOnly Property XP As Integer Implements IEnemy.XP
        Get
            Return _data.EnemyType.ToDescriptor().XP
        End Get
    End Property

    Public Sub TakeDamage(damage As Integer) Implements IEnemy.TakeDamage
        _data.Wounds += damage
    End Sub

    Public Function RollDefend(random As Random) As Integer Implements IEnemy.RollDefend
        Return random.Next(_data.EnemyType.ToDescriptor().Defend) + 1
    End Function

    Public Function Attack(character As ICharacter, random As Random) As IEnumerable(Of String) Implements IEnemy.Attack
        Dim messages As New List(Of String)
        Dim attackRoll = RollAttack(random)
        messages.Add($"{Name} rolls an attack of {attackRoll}!")
        Dim defendRoll = character.RollDefend(random)
        messages.Add($"{character.Name} rolls a defend of {defendRoll}!")
        If attackRoll > defendRoll Then
            Dim damage = attackRoll - defendRoll
            messages.Add($"{Name} hits for {damage} damage!")
            character.TakeDamage(damage)
        Else
            messages.Add($"{Name} misses!")
        End If
        Return messages
    End Function

    Public Function RollJools(random As Random) As Integer Implements IEnemy.RollJools
        Return random.Next(_data.EnemyType.ToDescriptor.MinimumJools, _data.EnemyType.ToDescriptor.MaximumJools + 1)
    End Function

    Private Function RollAttack(random As Random) As Integer
        Return random.Next(_data.EnemyType.ToDescriptor.Attack) + 1
    End Function
End Class
