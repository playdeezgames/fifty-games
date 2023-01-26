Module InGameMenuHandler
    Friend Function Run(world As IWorld, random As Random) As Boolean
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Game Menu:[/]"}
        prompt.AddChoice(CancelText)
        prompt.AddChoice(StatusText)
        If world.PlayerCharacter.HasLeveledUp Then
            prompt.AddChoice(LevelUpText)
        End If
        If world.PlayerCharacter.HasItems Then
            prompt.AddChoice(InventoryText)
        End If
        prompt.AddChoice(MainMenuText)
        Select Case AnsiConsole.Prompt(prompt)
            Case LevelUpText
                DoLevelUp(world.PlayerCharacter)
            Case CancelText
                'do nothing!
            Case InventoryText
                ShowInventory(random, world)
            Case MainMenuText
                Return True
            Case StatusText
                ShowStatus(world)
        End Select
        Return False
    End Function

    Private Sub DoLevelUp(character As ICharacter)
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Level Up...[/]"}
        prompt.AddChoice(CancelText)
        prompt.AddChoice(AttackStrengthText)
        prompt.AddChoice(DefendStrengthText)
        prompt.AddChoice(HitPointText)
        Select Case AnsiConsole.Prompt(prompt)
            Case CancelText
                'do nothing
            Case AttackStrengthText
                LevelUpAttackStrength(character)
            Case DefendStrengthText
                LevelUpDefendStrength(character)
            Case HitPointText
                LevelUpHitPoints(character)
        End Select
    End Sub

    Private Sub LevelUpHitPoints(character As ICharacter)
        If AnsiConsole.Confirm($"Increase yer Hit Points from {character.HitPoints} to {character.HitPoints + character.HitPointIncrease}?", False) Then
            character.LevelUpHitPoints()
        End If
    End Sub

    Private Sub LevelUpDefendStrength(character As ICharacter)
        If AnsiConsole.Confirm($"Increase yer Defend Strength from {character.DefendStrength} to {character.DefendStrength + character.DefendStrengthIncrease}?", False) Then
            character.LevelUpDefendStrength()
        End If
    End Sub

    Private Sub LevelUpAttackStrength(character As ICharacter)
        If AnsiConsole.Confirm($"Increase yer Attack Strength from {character.AttackStrength} to {character.AttackStrength + character.AttackStrengthIncrease}?", False) Then
            character.LevelUpAttackStrength()
        End If
    End Sub

    Private Sub ShowStatus(world As IWorld)
        AnsiConsole.Clear()
        Dim character = world.PlayerCharacter
        AnsiConsole.MarkupLine($"{character.Name}'s Status:")
        AnsiConsole.MarkupLine($"Level: {character.Level}")
        If character.HasLeveledUp Then
            AnsiConsole.MarkupLine($"XP: LEVEL UP!")
        Else
            AnsiConsole.MarkupLine($"XP: {character.XP}/{character.XPGoal}")
        End If
        AnsiConsole.MarkupLine($"Attack Strength: {character.AttackStrength}")
        AnsiConsole.MarkupLine($"Defend Strength: {character.DefendStrength}")
        AnsiConsole.MarkupLine($"HP: {character.HitPoints}/{character.MaximumHitPoints}")
        OkPrompt()
    End Sub
End Module
