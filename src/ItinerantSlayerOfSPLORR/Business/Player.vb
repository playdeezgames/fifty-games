Friend Class Player
    Implements IPlayer
    Private _data As PlayerData
    Sub New(data As PlayerData)
        _data = data
    End Sub

    Public Property X As Integer Implements IPlayer.X
        Get
            Return _data.BoardColumn
        End Get
        Set(value As Integer)
            _data.BoardColumn = value
        End Set
    End Property

    Public Property Y As Integer Implements IPlayer.Y
        Get
            Return _data.BoardRow
        End Get
        Set(value As Integer)
            _data.BoardRow = value
        End Set
    End Property
End Class
