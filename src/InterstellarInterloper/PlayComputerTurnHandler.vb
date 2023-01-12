Friend Module PlayComputerTurnHandler
    Friend Sub Run(data As InterstellarInterloperData, random As Random)
        Dim stars = data.Stars.Where(Function(star) star.Owner.HasValue AndAlso star.Owner.Value = data.OwnersTurn)
        Dim destinations = data.Stars.Where(Function(star) Not star.Owner.HasValue OrElse star.Owner.Value <> data.OwnersTurn)
        If destinations.Any Then
            Dim destination = random.Next(destinations.Count)
            For Each star In stars
                data.Fleets.Add(New FleetData With {.Destination = destination, .Owner = data.OwnersTurn, .Ships = star.Ships, .Speed = 3})
                star.Ships = 0
            Next
        End If
        data.OwnersTurn += 1
    End Sub
End Module
