Public Interface IShoppe
    ReadOnly Property Name As String
    ReadOnly Property SellsThings As Boolean
    ReadOnly Property Prices As IReadOnlyDictionary(Of ItemType, Integer)
    ReadOnly Property BuysThings As Boolean
    ReadOnly Property Offers As IReadOnlyDictionary(Of ItemType, Integer)
End Interface
