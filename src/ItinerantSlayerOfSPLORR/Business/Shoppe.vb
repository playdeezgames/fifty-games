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

    Public ReadOnly Property SellsThings As Boolean Implements IShoppe.SellsThings
        Get
            Return _data.Prices.Any
        End Get
    End Property

    Public ReadOnly Property Prices As IReadOnlyDictionary(Of ItemType, Integer) Implements IShoppe.Prices
        Get
            Return _data.Prices
        End Get
    End Property
End Class
