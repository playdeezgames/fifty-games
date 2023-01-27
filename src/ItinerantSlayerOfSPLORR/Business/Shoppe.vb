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

    Public ReadOnly Property Prices As IReadOnlyDictionary(Of ItemType, (Integer, Integer)) Implements IShoppe.Prices
        Get
            Return _data.Prices
        End Get
    End Property

    Public ReadOnly Property BuysThings As Boolean Implements IShoppe.BuysThings
        Get
            Return _data.Offers.Any
        End Get
    End Property

    Public ReadOnly Property Offers As IReadOnlyDictionary(Of ItemType, Integer) Implements IShoppe.Offers
        Get
            Return _data.Offers
        End Get
    End Property

    Public Sub ReduceQuantity(itemType As ItemType, quantity As Integer) Implements IShoppe.ReduceQuantity
        If Not _data.Prices.ContainsKey(itemType) Then
            Return
        End If
        If _data.Prices(itemType).Item2 <= quantity Then
            _data.Prices.Remove(itemType)
            Return
        End If
        _data.Prices(itemType) = (_data.Prices(itemType).Item1, _data.Prices(itemType).Item2 - quantity)
    End Sub
End Class
