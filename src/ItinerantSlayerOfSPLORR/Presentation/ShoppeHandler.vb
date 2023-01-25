Friend Module ShoppeHandler
    Friend Sub ShowShoppe(world As IWorld)
        Dim character = world.PlayerCharacter
        Do
            AnsiConsole.Clear()
            Dim shoppe As IShoppe = world.Shoppe
            AnsiConsole.MarkupLine($"{character.Name} enters {shoppe.Name}.")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now what?[/]"}
            If shoppe.SellsThings Then
                prompt.AddChoice(BuyText)
            End If
            prompt.AddChoice(LeaveText)
            Select Case AnsiConsole.Prompt(prompt)
                Case BuyText
                    BuyItems(shoppe, world.PlayerCharacter)
                Case LeaveText
                    Exit Do
            End Select
        Loop
        AnsiConsole.MarkupLine("Thanks fer stoppin'!")
        OkPrompt()
        character.IsInShoppe = False
    End Sub
    Private Sub BuyItems(shoppe As IShoppe, character As ICharacter)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"{character.Name} currently has {character.Jools} jools.")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to buy?[/]"}
            Dim table = shoppe.Prices.ToDictionary(Function(x) $"{x.Key.Name}(cost {x.Value}, have {character.ItemCount(x.Key)})", Function(x) x.Key)
            prompt.AddChoices(table.Keys)
            prompt.AddChoice(NeverMindText)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case NeverMindText
                    Exit Do
                Case Else
                    BuyItem(shoppe, character, table(answer))
            End Select
        Loop
    End Sub

    Private Sub BuyItem(shoppe As IShoppe, character As ICharacter, itemType As ItemType)
        Dim maximumQuantity = character.Jools \ shoppe.Prices(itemType)
        Select Case maximumQuantity
            Case 0
                AnsiConsole.MarkupLine("You don't have enough!")
                OkPrompt()
                Return
            Case 1
                character.BuyItems(shoppe, itemType, 1)
            Case Else
                Dim quantity = Math.Clamp(AnsiConsole.Ask(Of Integer)($"[olive]How many(0-{maximumQuantity})?[/]"), 0, maximumQuantity)
                If quantity > 0 Then
                    character.BuyItems(shoppe, itemType, quantity)
                End If
        End Select
    End Sub

End Module
