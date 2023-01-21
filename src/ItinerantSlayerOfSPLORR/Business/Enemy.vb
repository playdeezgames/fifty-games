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
End Class
