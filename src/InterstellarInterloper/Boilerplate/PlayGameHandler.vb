Friend Module PlayGameHandler
    Friend Function Run(data As InterstellarInterloperData) As Boolean
        If data.OwnersTurn >= data.Owners.Count Then
            FleetUpdateHandler.Run(data)
            ShipProductionHandler.Run(data)
            data.OwnersTurn = 0
            data.TurnsRemaining -= 1
            Return True
        End If
        Dim owner = data.Owners(data.OwnersTurn)
        If owner.IsHuman Then
            Return PlayHumanTurnHandler.Run(data)
        Else
            PlayComputerTurnHandler.Run(data)
        End If
        Return True
    End Function
End Module
