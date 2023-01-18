Friend Module InPlayHandler
    Friend Sub Run(world As World)
        AnsiConsole.Clear()
        AnsiConsole.Cursor.Hide
        Do
            ShowPlayerBoard(world)
            Dim key = WaitForKey()
            Select Case key
                Case ConsoleKey.Escape
                    Exit Do
            End Select
        Loop
        AnsiConsole.Cursor.Show
    End Sub

    Private Sub ShowPlayerBoard(world As World)
        Dim board = world.PlayerBoard

    End Sub

    Friend Function WaitForKey() As ConsoleKey
        Dim key As ConsoleKey?
        Do
            key = AnsiConsole.Console.Input.ReadKey(True)?.Key
        Loop Until key.HasValue
        Return key.Value
    End Function
End Module
