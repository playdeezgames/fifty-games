Friend Class Inn
    Implements IInn

    Private _worldData As WorldData
    Private _data As InnData

    Public Sub New(worldData As WorldData, inn As InnData)
        _worldData = worldData
        _data = inn
    End Sub

    Public ReadOnly Property Price As Integer Implements IInn.Price
        Get
            Return _data.Price
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IInn.Name
        Get
            Return _data.Name
        End Get
    End Property
End Class
