Friend Module InnHandler
    Friend Sub ShowInn(world As IWorld)
        AnsiConsole.Clear()
        Dim character = world.PlayerCharacter
        Dim inn = world.Inn
        AnsiConsole.MarkupLine($"{character.Name} enters {inn.Name}.")
        AnsiConsole.MarkupLine($"Price: {inn.Price} jools")
        If character.Jools >= inn.Price Then
            If AnsiConsole.Confirm("[olive]Would you like to rest here?[/]", False) Then
                character.RestAtInn(inn)
                AnsiConsole.MarkupLine("Thank you for yer patronage!")
            Else
                AnsiConsole.MarkupLine("Maybe next time!")
            End If
        Else
            AnsiConsole.MarkupLine($"{character.Name} can't afford it!")
        End If
        OkPrompt()
        character.IsInInn = False
    End Sub


End Module
