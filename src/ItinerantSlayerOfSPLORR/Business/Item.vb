Friend Class Item
    Implements IItem

    Private _data As ItemData

    Public Sub New(item As ItemData)
        Me._data = item
    End Sub

    Public ReadOnly Property ItemType As ItemType Implements IItem.ItemType
        Get
            Return _data.ItemType
        End Get
    End Property
End Class
