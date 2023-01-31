Friend Class ItemRemoval
    Implements IItemRemoval

    Private _worldData As WorldData
    Private _data As ItemRemovalData

    Public Sub New(worldData As WorldData, data As ItemRemovalData)
        Me._worldData = worldData
        Me._data = data
    End Sub

    Public ReadOnly Property ItemType As ItemType Implements IItemRemoval.ItemType
        Get
            Return _data.ItemType
        End Get
    End Property

    Public ReadOnly Property ItemCount As Integer Implements IItemRemoval.ItemCount
        Get
            Return _data.ItemCount
        End Get
    End Property
End Class
