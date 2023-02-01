Friend Interface ITrigger
    ReadOnly Property TriggerType As TriggerType
    ReadOnly Property Teleport As ITeleport
    ReadOnly Property Inn As IInn
    ReadOnly Property Shoppe As IShoppe
    ReadOnly Property IsActive As Boolean
    ReadOnly Property Message As IMessage
    ReadOnly Property Flag As String
    ReadOnly Property ItemRemoval As IItemRemoval
    ReadOnly Property Minigame As Minigame
End Interface
