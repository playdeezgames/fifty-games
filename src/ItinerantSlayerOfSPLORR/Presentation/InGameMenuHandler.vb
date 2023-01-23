Module InGameMenuHandler
    Friend Function Run(world As IWorld) As Boolean
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Game Menu:[/]"}
        prompt.AddChoice(CancelText)
        prompt.AddChoice(StatusText)
        prompt.AddChoice(MainMenuText)
        Select Case AnsiConsole.Prompt(prompt)
            Case CancelText
                'do nothing!
            Case MainMenuText
                Return True
            Case StatusText
                ShowStatus(world)
        End Select
        Return False
    End Function

    Private Sub ShowStatus(world As IWorld)
        AnsiConsole.Clear()
        Dim character = world.PlayerCharacter
        AnsiConsole.MarkupLine($"{character.Name}'s Status:")
        AnsiConsole.MarkupLine($"Attack Strength: {character.AttackStrength}")
        AnsiConsole.MarkupLine($"Defend Strength: {character.DefendStrength}")
        AnsiConsole.MarkupLine($"HP: {character.HitPoints}/{character.MaximumHitPoints}")
        OkPrompt()
    End Sub
End Module
