Public Class ShipData
    Public Property ShipType As ShipType
    Public Property PlayerOwned As Boolean
    Public Property HasMoved As Boolean
    Public Property HasFired As Boolean

    Friend Sub EndTurn()
        HasMoved = False
        HasFired = False
    End Sub

    Friend Function IsMovable() As Boolean
        Return Not HasMoved
    End Function

    Friend Function IsFirable() As Boolean
        Return Not HasFired
    End Function
End Class
