Friend Module StarHandler
    Friend Sub Run(data As InterstellarInterloperData, starIndex As Integer)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Star #{starIndex + 1:d2}")
            Dim star = data.Stars(starIndex)
            AnsiConsole.MarkupLine($"Ships: { star.Ships}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]Orders?[/]"}
            prompt.AddChoice(NeverMindText)
            If star.Ships > 0 Then
                prompt.AddChoice(SendFleetText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case NeverMindText
                    Exit Do
            End Select
        Loop

    End Sub
End Module
