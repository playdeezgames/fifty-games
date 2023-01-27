Public Interface IShoppe
    ReadOnly Property Name As String
    ReadOnly Property SellsThings As Boolean
    ReadOnly Property Prices As IReadOnlyDictionary(Of ItemType, (Integer, Integer))
    ReadOnly Property BuysThings As Boolean
    ReadOnly Property Offers As IReadOnlyDictionary(Of ItemType, Integer)
    Sub ReduceQuantity(itemType As ItemType, quantity As Integer)
End Interface
