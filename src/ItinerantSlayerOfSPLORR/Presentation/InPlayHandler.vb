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
                'TODO: next trigger?
            ElseIf world.PlayerCharacter.IsInShoppe Then
                ShowShoppe(world)
                'TODO: next trigger?
            ElseIf world.PlayerCharacter.IsInMessage Then
                ShowMessage(world)
                'TODO: next trigger?
            ElseIf world.IsInAnEncounter Then
                ShowEncounter(random, world)
            Else
                If ShowBoard(random, world) Then
                    Exit Do
                End If
            End If
        Loop
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
