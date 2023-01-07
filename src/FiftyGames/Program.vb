Module Program
    Private Const QuitText = "Quit"
    Private Const OkText = "Ok"
    Private ReadOnly gameTable As IReadOnlyDictionary(Of String, Action(Of FiftyGamesData)) =
        New Dictionary(Of String, Action(Of FiftyGamesData)) From
        {
            {"Guess My Number", Sub(data) GuessMyNumber.Run(data.GuessMyNumber)},
            {"Game02", AddressOf ThisGameIsAStub},
            {"Game03", AddressOf ThisGameIsAStub},
            {"Game04", AddressOf ThisGameIsAStub},
            {"Game05", AddressOf ThisGameIsAStub},
            {"Game06", AddressOf ThisGameIsAStub},
            {"Game07", AddressOf ThisGameIsAStub},
            {"Game08", AddressOf ThisGameIsAStub},
            {"Game09", AddressOf ThisGameIsAStub},
            {"Game10", AddressOf ThisGameIsAStub},
            {"Game11", AddressOf ThisGameIsAStub},
            {"Game12", AddressOf ThisGameIsAStub},
            {"Game13", AddressOf ThisGameIsAStub},
            {"Game14", AddressOf ThisGameIsAStub},
            {"Game15", AddressOf ThisGameIsAStub},
            {"Game16", AddressOf ThisGameIsAStub},
            {"Game17", AddressOf ThisGameIsAStub},
            {"Game18", AddressOf ThisGameIsAStub},
            {"Game19", AddressOf ThisGameIsAStub},
            {"Game20", AddressOf ThisGameIsAStub},
            {"Game21", AddressOf ThisGameIsAStub},
            {"Game22", AddressOf ThisGameIsAStub},
            {"Game23", AddressOf ThisGameIsAStub},
            {"Game24", AddressOf ThisGameIsAStub},
            {"Game25", AddressOf ThisGameIsAStub},
            {"Game26", AddressOf ThisGameIsAStub},
            {"Game27", AddressOf ThisGameIsAStub},
            {"Game28", AddressOf ThisGameIsAStub},
            {"Game29", AddressOf ThisGameIsAStub},
            {"Game30", AddressOf ThisGameIsAStub},
            {"Game31", AddressOf ThisGameIsAStub},
            {"Game32", AddressOf ThisGameIsAStub},
            {"Game33", AddressOf ThisGameIsAStub},
            {"Game34", AddressOf ThisGameIsAStub},
            {"Game35", AddressOf ThisGameIsAStub},
            {"Game36", AddressOf ThisGameIsAStub},
            {"Game37", AddressOf ThisGameIsAStub},
            {"Game38", AddressOf ThisGameIsAStub},
            {"Game39", AddressOf ThisGameIsAStub},
            {"Game40", AddressOf ThisGameIsAStub},
            {"Game41", AddressOf ThisGameIsAStub},
            {"Game42", AddressOf ThisGameIsAStub},
            {"Game43", AddressOf ThisGameIsAStub},
            {"Game44", AddressOf ThisGameIsAStub},
            {"Game45", AddressOf ThisGameIsAStub},
            {"Game46", AddressOf ThisGameIsAStub},
            {"Game47", AddressOf ThisGameIsAStub},
            {"Game48", AddressOf ThisGameIsAStub},
            {"Game49", AddressOf ThisGameIsAStub},
            {"Game50", AddressOf ThisGameIsAStub}
        }
    Sub Main(args As String())
        Console.Title = "Fifty Games!"
        Dim data As New FiftyGamesData
        'TODO: load data from file if it exists, otherwise blank it out
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Fifty Games![/]"}
            prompt.AddChoice(QuitText)
            prompt.AddChoices(gameTable.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case QuitText
                    If Common.Confirm("[red]Are you sure you want to quit?[/]") Then
                        Exit Do
                    End If
                Case Else
                    gameTable(answer)(data)
                    'TODO: save data
            End Select

        Loop
    End Sub
    Private Sub ThisGameIsAStub(data As FiftyGamesData)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[red]This Game Is A Stub! Come back later![/]")
        Common.OkPrompt()
    End Sub
End Module
