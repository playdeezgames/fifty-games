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

    Public ReadOnly Property Shoppe As IShoppe Implements ITrigger.Shoppe
        Get
            If _data.Shoppe Is Nothing Then
                Return Nothing
            End If
            Return New Shoppe(_worldData, _data.Shoppe)
        End Get
    End Property

    Public ReadOnly Property IsActive As Boolean Implements ITrigger.IsActive
        Get
            For Each condition In _data.Conditions
                Select Case condition.Condition
                    Case TriggerConditionType.WhenFlagClear
                        If _worldData.Flags.Contains(condition.ConditionFlag) Then
                            Return False
                        End If
                    Case TriggerConditionType.WhenFlagSet
                        If Not _worldData.Flags.Contains(condition.ConditionFlag) Then
                            Return False
                        End If
                    Case TriggerConditionType.WhenItemCountAtLeast
                        If (New World(_worldData)).PlayerCharacter.ItemCount(condition.ItemType) < condition.ItemCount Then
                            Return False
                        End If
                    Case Else
                        Throw New NotImplementedException
                End Select
            Next
            Return True
        End Get
    End Property

    Public ReadOnly Property Message As IMessage Implements ITrigger.Message
        Get
            If _data.Message Is Nothing Then
                Return Nothing
            End If
            Return New Message(_worldData, _data.Message)
        End Get
    End Property

    Public ReadOnly Property Flag As String Implements ITrigger.Flag
        Get
            Return _data.Flag
        End Get
    End Property

    Public ReadOnly Property ItemRemoval As IItemRemoval Implements ITrigger.ItemRemoval
        Get
            Return New ItemRemoval(_worldData, _data.ItemRemoval)
        End Get
    End Property

    Public ReadOnly Property Minigame As Minigame Implements ITrigger.Minigame
        Get
            Return _data.Minigame
        End Get
    End Property
End Class
