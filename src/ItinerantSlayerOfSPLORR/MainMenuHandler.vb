Friend Module MainMenuHandler
    Friend Sub Run(world As World)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Main Menu:[/]"}
            If world.CanContinue Then
                prompt.AddChoice(ContinueGameText)
            End If
            If world.CanAbandon Then
                prompt.AddChoice(AbandonGameText)
            End If
            If world.CanStart Then
                prompt.AddChoice(StartGameText)
            End If
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ContinueGameText
                    InPlayHandler.Run(world)
                Case StartGameText
                    world.StartGame()
                    InPlayHandler.Run(world)
                Case AbandonGameText
                    If Confirm("[red]Are you sure you want to abandon the game?[/]") Then
                        world.AbandonGame()
                    End If
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
