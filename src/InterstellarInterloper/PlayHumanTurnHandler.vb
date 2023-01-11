Friend Module PlayHumanTurnHandler
    Friend Function Run(data As InterstellarInterloperData) As Boolean
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]Player #{data.OwnersTurn + 1}:[/]"}
        prompt.AddChoice(StarsText)
        prompt.AddChoice(MapText)
        prompt.AddChoice(QuitText)
        Select Case AnsiConsole.Prompt(prompt)
            Case StarsText
                StarsHandler.Run(data)
            Case MapText
                MapHandler.Run(data)
            Case QuitText
                Return Not ConfirmQuit()
        End Select
        Return True
    End Function
End Module
