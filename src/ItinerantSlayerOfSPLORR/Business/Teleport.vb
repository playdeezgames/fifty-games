Friend Class Teleport
    Implements ITeleport

    Private _worldData As WorldData
    Private _data As TeleportData

    Public Sub New(worldData As WorldData, data As TeleportData)
        _worldData = worldData
        _data = data
    End Sub

    Public ReadOnly Property DestinationX As Integer Implements ITeleport.DestinationX
        Get
            Return _data.DestinationX
        End Get
    End Property

    Public ReadOnly Property DestinationY As Integer Implements ITeleport.DestinationY
        Get
            Return _data.DestinationY
        End Get
    End Property

    Public ReadOnly Property DestinationBoard As IBoard Implements ITeleport.DestinationBoard
        Get
            Return New Board(_worldData, _data.DestinationBoard, _worldData.Boards(_data.DestinationBoard))
        End Get
    End Property
End Class
