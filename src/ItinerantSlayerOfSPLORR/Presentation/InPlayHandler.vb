Friend Module InPlayHandler
    Friend Sub Run(world As IWorld)
        Dim random As New Random
        AnsiConsole.Clear()
        Do
            If world.PlayerCharacter.IsDead Then
                ShowGameOver(world)
                Exit Do
            ElseIf world.PlayerCharacter.IsInInn Then
                ShowInn(world)
            ElseIf world.PlayerCharacter.IsInShoppe Then
                ShowShoppe(world)
            ElseIf world.PlayerCharacter.IsInMessage Then
                ShowMessage(world)
            ElseIf world.PlayerCharacter.Minigame <> Minigame.None Then
                ShowMinigame(world)
            ElseIf world.HasMoreTriggers Then
                world.ProceedToNextTrigger()
            ElseIf world.IsInAnEncounter Then
                ShowEncounter(random, world)
            Else
                If ShowBoard(random, world) Then
                    Exit Do
                End If
            End If
        Loop
    End Sub

    Private Sub ShowMinigame(world As IWorld)
        Select Case world.PlayerCharacter.Minigame
            Case Minigame.None
                Return
            Case Minigame.RSP
                ShowRSP(world)
        End Select
    End Sub

    Private ReadOnly rspChoices As New List(Of String) From {"Rock", "Scissors", "Paper"}

    Private Sub ShowRSP(world As IWorld)
        Dim random As New Random
        AnsiConsole.Clear()
        Dim character = world.PlayerCharacter
        If character.Jools < 1 Then
            AnsiConsole.MarkupLine("Come back when you've got jools!")
            OkPrompt()
            character.Minigame = Minigame.None
            Return
        End If
        Dim bet = Math.Clamp(AnsiConsole.Ask("How much do you want to bet(0-10)?", 0), 0, Math.Min(10, character.Jools))
        If bet = 0 Then
            AnsiConsole.MarkupLine("Maybe next time!")
            OkPrompt()
            character.Minigame = Minigame.None
            Return
        End If
        Dim npcMove = random.Next(3)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Choose:[/]"}
        prompt.AddChoices(rspChoices)
        Dim playerMove = rspChoices.IndexOf(AnsiConsole.Prompt(prompt))
        AnsiConsole.MarkupLine($"I chose {rspChoices(npcMove)}, you chose {rspChoices(playerMove)}.")
        If npcMove = playerMove Then
            AnsiConsole.MarkupLine("It's a tie!")
            OkPrompt()
            Return
        End If
        If (npcMove = 0 AndAlso playerMove = 1) OrElse (npcMove = 1 AndAlso playerMove = 2) OrElse (npcMove = 2 AndAlso playerMove = 0) Then
            AnsiConsole.MarkupLine("I win!")
            character.Jools -= bet
            OkPrompt()
            Return
        End If
        AnsiConsole.MarkupLine("You win!")
        character.Jools += bet
        OkPrompt()
        Return
    End Sub

    Private Sub ShowMessage(world As IWorld)
        Dim character = world.PlayerCharacter
        AnsiConsole.Clear()
        Dim message As IMessage = world.Message
        AnsiConsole.MarkupLine(message.Text)
        OkPrompt()
        character.IsInMessage = False
    End Sub

    Friend Sub ShowMessages(messages As IEnumerable(Of String))
        For Each message In messages
            AnsiConsole.MarkupLine(message)
        Next
        OkPrompt()
    End Sub
End Module
