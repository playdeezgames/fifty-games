Public Class BoardCellData
    Public Property Feature As FeatureData
    Public Property Ship As ShipData
    Public Property IsVisible As Boolean

    Friend Sub EndTurn()
        If Ship IsNot Nothing Then
            Ship.EndTurn()
        End If
    End Sub

    Friend Function HasPlayerShips() As Boolean
        Return Ship IsNot Nothing AndAlso Ship.PlayerOwned
    End Function

    Friend Function GetPlayerShips() As IEnumerable(Of ShipData)
        If Ship Is Nothing Then
            Return Array.Empty(Of ShipData)
        End If
        Return New List(Of ShipData) From {Ship}
    End Function

    Friend Function HasMovablePlayerShips() As Boolean
        Return HasPlayerShips() AndAlso Ship.IsMovable
    End Function

    Friend Function HasFirablePlayerShips() As Boolean
        Return HasPlayerShips() AndAlso Ship.IsFirable
    End Function
End Class
