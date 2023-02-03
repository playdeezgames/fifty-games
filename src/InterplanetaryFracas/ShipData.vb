Public Class ShipData
    Public Property ShipType As ShipType
    Public Property PlayerOwned As Boolean
    Public Property HasMoved As Boolean

    Friend Sub EndTurn()
        HasMoved = False
    End Sub

    Friend Function IsMovable() As Boolean
        Return Not HasMoved
    End Function
End Class
