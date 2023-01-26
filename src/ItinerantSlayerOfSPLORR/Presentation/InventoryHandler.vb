Friend Module InventoryHandler

    Friend Sub ShowInventory(random As Random, world As IWorld)
        Dim character = world.PlayerCharacter
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"{character.Name}'s Inventory")
        For Each item In character.Items
            AnsiConsole.MarkupLine($"{item.Item1.ToDescriptor.Name}(x{item.Item2})")
        Next
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(NeverMindText)
        If character.CanUseItem Then
            prompt.AddChoice(UseText)
        End If
        If character.CanEquipItem Then
            prompt.AddChoice(EquipText)
        End If
        Select Case AnsiConsole.Prompt(prompt)
            Case NeverMindText
                'do nothing
            Case UseText
                UseItem(random, world)
            Case EquipText
                EquipItem(world)
        End Select
    End Sub
    Private Sub EquipItem(world As IWorld)
        Dim character = world.PlayerCharacter
        Dim equippableItems As IEnumerable(Of ItemType) = character.EquippableItems
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Equip What?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table = equippableItems.ToDictionary(Of String, ItemType)(Function(x) $"{x.ToDescriptor.Name}", Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                'do nothing
            Case Else
                ShowMessages(world.EquipItem(table(answer)))
        End Select
    End Sub
End Module
