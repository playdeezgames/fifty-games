Public Module Utility
    Private Const OkText = "Ok"
    Private Const NoText = "No"
    Private Const YesText = "Yes"
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
End Module
