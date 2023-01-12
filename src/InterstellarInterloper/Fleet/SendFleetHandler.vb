Friend Module SendFleetHandler
    Friend Sub Run(data As InterstellarInterloperData, starIndex As Integer)
        Dim star = data.Stars(starIndex)
        Dim shipCount = Math.Clamp(AnsiConsole.Ask("[olive]How many ships?[/]", 0), 0, star.Ships)
        If shipCount = 0 Then
            Return
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Destination?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table As New Dictionary(Of String, Integer)
        For index = 1 To data.Stars.Count
            If index <> starIndex + 1 Then
                table.Add($"#{index:d2}", index - 1)
            End If
        Next
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                Dim destination = data.Stars(table(answer))

                Dim fleet As New FleetData With
                    {
                        .Destination = table(answer),
                        .Owner = data.OwnersTurn,
                        .Speed = 3,
                        .Distance = CInt(Math.Sqrt((destination.X - star.X) * (destination.X - star.X) + (destination.Y - star.Y) * (destination.Y - star.Y))),
                        .Ships = shipCount
                    }
                data.Fleets.Add(fleet)
                data.Stars(starIndex).Ships -= shipCount
        End Select
    End Sub
End Module
