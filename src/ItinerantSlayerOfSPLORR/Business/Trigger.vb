Friend Class Trigger
    Implements ITrigger

    Private _data As TriggerData

    Public Sub New(trigger As TriggerData)
        _data = trigger
    End Sub
End Class
