Public Module GuessMyNumber
    Private Const PlayText = "Play!"
    Private Const HowToPlayText = "How to play?"
    Private Const QuitText = "Quit"
    Public Sub Run()
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Guess My Number(1-100)[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                Case HowToPlayText
                Case QuitText
                    If Common.Confirm("[red]Are you sure you want to quit?[/]") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
