Public Class ShipData
    Public Property ShipType As ShipType
    Public Property PlayerOwned As Boolean
    Public Property HasMoved As Boolean
    Public Property HasFired As Boolean
    Public Property Torpedos As Integer

    Friend Sub EndTurn()
        HasMoved = False
        HasFired = False
    End Sub

    Friend Function IsMovable() As Boolean
        Return Not HasMoved
    End Function

    Friend Function IsFirable() As Boolean
        Return Not HasFired AndAlso Torpedos > 0
    End Function
End Class
