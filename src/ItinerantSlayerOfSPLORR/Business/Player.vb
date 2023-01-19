Friend Class Player
    Implements IPlayer
    Private _data As PlayerData
    Sub New(data As PlayerData)
        _data = data
    End Sub

    Public ReadOnly Property X As Integer Implements IPlayer.X
        Get
            Return _data.BoardColumn
        End Get
    End Property

    Public ReadOnly Property Y As Integer Implements IPlayer.Y
        Get
            Return _data.BoardRow
        End Get
    End Property
End Class
