Friend Module PlayGameHandler
    Friend Function Run(data As InterstellarInterloperData) As Boolean
        'TODO: handle "nothing" owners after all owners have moved
        Dim owner = data.Owners(data.OwnersTurn)
        If owner.IsHuman Then
            Return PlayHumanTurnHandler.Run(data)
        Else
            PlayComputerTurnHandler.Run(data)
        End If
        Return True
    End Function
End Module
