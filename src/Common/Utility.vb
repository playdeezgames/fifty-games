Imports System.Runtime.CompilerServices

Public Module Utility
    <Extension>
    Public Function Generate(Of TGenerated)(table As IReadOnlyDictionary(Of TGenerated, Integer), random As Random) As TGenerated
        Dim generated = random.Next(table.Values.Sum())
        For Each entry In table
            generated -= entry.Value
            If generated < 0 Then
                Return entry.Key
            End If
        Next
        Throw New NotImplementedException
    End Function
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
    Public Sub MainMenu(Of TData)(title As String, data As TData, playGameAction As Action(Of TData), instructionAction As Action, statsAction As Action(Of TData))
        Do
            AnsiConsole.Clear()
            statsAction(data)
            Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]{title}[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                    playGameAction(data)
                Case HowToPlayText
                    instructionAction()
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
End Module
