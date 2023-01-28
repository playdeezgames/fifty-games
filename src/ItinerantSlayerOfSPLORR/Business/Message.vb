Friend Class Message
    Implements IMessage

    Private _worldData As WorldData
    Private _data As MessageData

    Public Sub New(worldData As WorldData, message As MessageData)
        _worldData = worldData
        _data = message
    End Sub

    Public ReadOnly Property Text As String Implements IMessage.Text
        Get
            Return _data.Text
        End Get
    End Property
End Class
