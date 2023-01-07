Public Module GuessMyNumber
    Private Const OkText = "Ok"
    Public Sub Run()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[red]This Guess My Number Game Is A Stub! Come back later![/]")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
