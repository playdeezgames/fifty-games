Friend Module FleetUpdateHandler
    Friend Sub Run(data As InterstellarInterloperData)
        For Each fleet In data.Fleets
            fleet.Distance -= fleet.Speed
        Next
        Dim arrivedFleets = data.Fleets.Where(Function(fleet) fleet.Distance <= 0).ToList
        data.Fleets = data.Fleets.Where(Function(fleet) fleet.Distance > 0).ToList
        For Each fleet In arrivedFleets
            HandleArrival(data, fleet)
        Next
    End Sub

    Private Sub HandleArrival(data As InterstellarInterloperData, fleet As FleetData)
        Dim destination = data.Stars(fleet.Destination)
        If destination.Owner = fleet.Owner Then
            HandleFriendlyArrival(destination, fleet)
        Else
            HandleEnemyArrival(destination, fleet)
        End If
    End Sub

    Private Sub HandleEnemyArrival(destination As StarData, fleet As FleetData)
        Dim defenderName = If(destination.Owner.HasValue, $"Player #{destination.Owner.Value + 1:d2}", "Neutral Player")
        Dim attackerName = $"Player #{fleet.Owner + 1:d2}"
        AnsiConsole.MarkupLine($"{attackerName} attacks Star #{fleet.Destination + 1:d2} with {fleet.Ships} ships, currently owned by {defenderName} and defended by {destination.Ships} ships.")
        Dim random As New Random
        Dim round = 1
        While fleet.Ships > 0 AndAlso destination.Ships > 0
            Dim attack = Math.Min(random.Next(0, fleet.Ships + 1), destination.Ships)
            Dim defend = Math.Min(random.Next(0, destination.Ships + 1), fleet.Ships)
            AnsiConsole.MarkupLine($"Round #{round}:")
            AnsiConsole.MarkupLine($"The invaders destroy {attack} ships!")
            AnsiConsole.MarkupLine($"The defenders destroy {defend} ships!")
            fleet.Ships = Math.Max(0, fleet.Ships - defend)
            destination.Ships = Math.Max(0, destination.Ships - attack)
            AnsiConsole.MarkupLine($"The attack fleet has {fleet.Ships} ships remaining!")
            AnsiConsole.MarkupLine($"The star has {destination.Ships} remaining!")
            OkPrompt()
            round += 1
        End While
        If fleet.Ships > 0 Then
            AnsiConsole.MarkupLine($"{attackerName} takes over Star #{fleet.Destination + 1:d2}!")
            destination.Owner = fleet.Owner
            destination.Ships = fleet.Ships
        Else
            AnsiConsole.MarkupLine($"{defenderName} keeps control over Star #{fleet.Destination + 1:d2}.")
        End If
        OkPrompt()
    End Sub

    Private Sub HandleFriendlyArrival(destination As StarData, fleet As FleetData)
        AnsiConsole.MarkupLine($"Player #{destination.Owner + 1:d2} fleet of {fleet.Ships} ships arrives at Star #{fleet.Destination + 1:d2}")
        destination.Ships += fleet.Ships
    End Sub
End Module
