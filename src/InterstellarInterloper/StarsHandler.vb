Friend Module StarsHandler
    Friend Sub Run(data As InterstellarInterloperData)
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String)
        prompt.AddChoice(NeverMindText)
        Dim index = 1
        Dim table = New Dictionary(Of String, Integer)
        For Each star In data.Stars
            If star.Owner = data.OwnersTurn Then
                table.Add($"#{index:d2} - Ships: {star.Ships}", index - 1)
            End If
            index += 1
        Next
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                StarHandler.Run(data, table(answer))
        End Select
    End Sub
End Module
