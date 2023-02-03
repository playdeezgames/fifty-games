Public Class BoardCellData
    Public Property Feature As FeatureData
    Public Property Ships As New List(Of ShipData)
    Public Property IsVisible As Boolean

    Friend Sub RemoveShip(ship As ShipData)
        Ships.Remove(ship)
    End Sub

    Friend Sub AddShip(ship As ShipData)
        Ships.Add(ship)
    End Sub

    Friend Sub EndTurn()
        For Each ship In Ships
            ship.EndTurn()
        Next
    End Sub

    Friend Function HasPlayerShips() As Boolean
        Return Ships.Any(Function(x) x.PlayerOwned)
    End Function

    Friend Function GetPlayerShips() As IEnumerable(Of ShipData)
        Return Ships.Where(Function(x) x.PlayerOwned)
    End Function

    Friend Function HasMovablePlayerShips() As Boolean
        Return Ships.Any(Function(x) x.PlayerOwned AndAlso x.IsMovable)
    End Function
End Class
