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
                'TODO: equip handler
        End Select
    End Sub

End Module
