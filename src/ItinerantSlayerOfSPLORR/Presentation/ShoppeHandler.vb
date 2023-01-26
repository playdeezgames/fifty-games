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
            If shoppe.BuysThings Then
                prompt.AddChoice(SellText)
            End If
            prompt.AddChoice(LeaveText)
            Select Case AnsiConsole.Prompt(prompt)
                Case BuyText
                    BuyItems(shoppe, world.PlayerCharacter)
                Case SellText
                    SellItems(shoppe, world.PlayerCharacter)
                Case LeaveText
                    Exit Do
            End Select
        Loop
        AnsiConsole.MarkupLine("Thanks fer stoppin'!")
        OkPrompt()
        character.IsInShoppe = False
    End Sub

    Private Sub SellItems(shoppe As IShoppe, character As ICharacter)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to buy?[/]"}
            Dim table = shoppe.Offers.ToDictionary(Function(x) $"{x.Key.ToDescriptor.Name}(offers {x.Value}, have {character.ItemCount(x.Key)})", Function(x) x.Key)
            prompt.AddChoices(table.Keys)
            prompt.AddChoice(NeverMindText)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case NeverMindText
                    Exit Do
                Case Else
                    SellItem(shoppe, character, table(answer))
            End Select
        Loop
    End Sub

    Private Sub SellItem(shoppe As IShoppe, character As ICharacter, itemType As ItemType)
        Dim maximumQuantity = character.ItemCount(itemType)
        Select Case maximumQuantity
            Case Is <= 0
                AnsiConsole.MarkupLine($"{character.Name} does have any {itemType.ToDescriptor.Name} to sell.")
                OkPrompt()
            Case 1
                character.SellItems(shoppe, itemType, 1)
            Case Else
                Dim quantity = Math.Clamp(AnsiConsole.Ask(Of Integer)($"[olive]How many(0-{maximumQuantity})?[/]"), 0, maximumQuantity)
                If quantity > 0 Then
                    character.SellItems(shoppe, itemType, quantity)
                End If
        End Select
    End Sub

    Private Sub BuyItems(shoppe As IShoppe, character As ICharacter)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"{character.Name} currently has {character.Jools} jools.")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to buy?[/]"}
            Dim table = shoppe.Prices.ToDictionary(Function(x) $"{x.Key.ToDescriptor.Name}(cost {x.Value}, have {character.ItemCount(x.Key)})", Function(x) x.Key)
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
        Dim maximumQuantity = If(shoppe.Prices(itemType) > 0, character.Jools \ shoppe.Prices(itemType), 1)
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
