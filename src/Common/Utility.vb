Public Module Utility
    Private Const OkText = "Ok"
    Public Sub OkPrompt()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(OkText)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
