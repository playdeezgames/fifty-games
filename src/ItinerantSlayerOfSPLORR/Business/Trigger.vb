Friend Class Trigger
    Implements ITrigger

    Private _worldData As WorldData
    Private _data As TriggerData

    Public Sub New(worldData As WorldData, trigger As TriggerData)
        _worldData = worldData
        _data = trigger
    End Sub

    Public ReadOnly Property TriggerType As TriggerType Implements ITrigger.TriggerType
        Get
            Return _data.TriggerType
        End Get
    End Property

    Public ReadOnly Property Teleport As ITeleport Implements ITrigger.Teleport
        Get
            If _data.Teleport Is Nothing Then
                Return Nothing
            End If
            Return New Teleport(_worldData, _data.Teleport)
        End Get
    End Property

    Public ReadOnly Property Inn As IInn Implements ITrigger.Inn
        Get
            If _data.Inn Is Nothing Then
                Return Nothing
            End If
            Return New Inn(_worldData, _data.Inn)
        End Get
    End Property
End Class
