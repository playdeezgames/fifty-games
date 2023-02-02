Public Class BoardCellData
    Public Property Feature As FeatureData
    Public Property Ships As New List(Of ShipData)
    Public Property IsVisible As Boolean
    Friend Function HasPlayerShips() As Boolean
        Return Ships.Any(Function(x) x.PlayerOwned)
    End Function
End Class
