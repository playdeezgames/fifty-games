Public Module Utility
    Public Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
    Public Function Confirm(text As String) As Boolean
        Dim prompt As New SelectionPrompt(Of String) With {.Title = text}
        prompt.AddChoices(NoText, YesText)
        Return AnsiConsole.Prompt(prompt) = YesText
    End Function
    Public Function ConfirmQuit() As Boolean
        Return Common.Confirm("[red]Are you sure you want to quit?[/]")
    End Function
End Module
