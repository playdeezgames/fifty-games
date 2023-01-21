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

    Public Sub TakeDamage(damage As Integer) Implements IEnemy.TakeDamage
        _data.Wounds += damage
    End Sub

    Public Function RollDefend(random As Random) As Integer Implements IEnemy.RollDefend
        Return random.Next(_data.EnemyType.ToDescriptor().Defend)
    End Function
End Class
