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
        Dim random As New Random
        'TODO: fleet from owner arrives at star 
        'TODO: if owner same, add ships from fleet to star
        'TODO: if owner different, combat
    End Sub
End Module
