Friend Class Shoppe
    Implements IShoppe

    Private _worldData As WorldData
    Private _data As ShoppeData

    Public Sub New(worldData As WorldData, shoppe As ShoppeData)
        _worldData = worldData
        _data = shoppe
    End Sub

    Public ReadOnly Property Name As String Implements IShoppe.Name
        Get
            Return _data.Name
        End Get
    End Property
End Class
