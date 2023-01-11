Friend Module GameOverHandler
    Friend Function Run(data As InterstellarInterloperData) As Boolean
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]No Game In Progress![/]"}
        If data.Owners.Any Then
            'TODO: last game summary
        End If
        prompt.AddChoice(StartGameText)
        prompt.AddChoice(QuitText)
        Select Case AnsiConsole.Prompt(prompt)
            Case StartGameText
                StartGameHandler.Run(data)
            Case QuitText
                If ConfirmQuit() Then
                    Return False
                End If
        End Select
        Return True
    End Function
End Module
