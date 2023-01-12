Friend Module MapHandler
    Friend Sub Run(data As InterstellarInterloperData)
        AnsiConsole.Clear()
        Dim index = 1
        For Each star In data.Stars
            AnsiConsole.Cursor.SetPosition(star.X * 4 + 1, star.Y + 1)
            Dim color = If(data.OwnersTurn = star.Owner, "olive", "silver")
            AnsiConsole.Markup($"[{color}]({index:d2})[/]")
            index += 1
        Next
        AnsiConsole.Cursor.SetPosition(1, MapRows + 1)
        OkPrompt()
    End Sub
End Module
