Friend Module EncounterHandler
    Friend Sub ShowEncounter(random As Random, world As IWorld)
        'fight
        'use item
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Encounter!")
        AnsiConsole.WriteLine()

        Dim character = world.PlayerCharacter
        AnsiConsole.MarkupLine($"{character.Name} HP {character.HitPoints}/{character.MaximumHitPoints}")
        AnsiConsole.WriteLine()
        Dim index = 1
        For Each enemy In world.Encounter.Enemies
            AnsiConsole.MarkupLine($"Enemy #{index}: {enemy.Name} ({enemy.HitPoints}/{enemy.MaximumHitPoints})")
            index += 1
        Next

        AnsiConsole.WriteLine()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(FightText)
        If character.CanUseItem Then
            prompt.AddChoice(ItemText)
        End If
        prompt.AddChoice(FleeText)

        Select Case AnsiConsole.Prompt(prompt)
            Case FightText
                FightEnemy(random, world)
            Case FleeText
                world.FleeEncounter()
            Case ItemText
                CombatUseItem(random, world)
        End Select
    End Sub
    Private Sub CombatUseItem(random As Random, world As IWorld)
        Dim items As IEnumerable(Of ItemType) = world.PlayerCharacter.UsableItems
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Use what?[/]"}
        Dim table = items.ToDictionary(Of String, ItemType)(Function(x) x.Name, Function(x) x)
        prompt.AddChoices(table.Keys)
        prompt.AddChoice(NeverMindText)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                Dim messages = world.UseItem(table(answer), random)
                ShowMessages(messages)
        End Select
    End Sub

    Private Sub FightEnemy(random As Random, world As IWorld)
        Dim messages = world.Attack(If(world.Encounter.Enemies.Count = 1, world.Encounter.Enemies.Single, PickEnemy(world.Encounter.Enemies)), random)
        ShowMessages(messages)
    End Sub

    Private Function PickEnemy(enemies As IEnumerable(Of IEnemy)) As IEnemy
        Dim table As New Dictionary(Of String, IEnemy)
        Dim index = 1
        For Each enemy In enemies
            table.Add($"#{index}: {enemy.Name} ({enemy.HitPoints}/{enemy.MaximumHitPoints})", enemy)
        Next
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which One?[/]"}
        prompt.AddChoices(table.Keys)
        Return table(AnsiConsole.Prompt(prompt))
    End Function
End Module
