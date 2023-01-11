Friend Module FleetsHandler
    Friend Sub Run(data As InterstellarInterloperData)
        AnsiConsole.Clear()
        Dim fleets = data.Fleets.Where(Function(fleet) fleet.Owner = data.OwnersTurn)
        Dim index = 1
        For Each fleet In fleets
            AnsiConsole.MarkupLine($"#{index:d2} - Ships: {fleet.Ships}, Destination: #{fleet.Destination + 1:d2}, Distance: {fleet.Distance}, Speed: {fleet.Speed}")
            index += 1
        Next
        OkPrompt()
    End Sub
End Module
