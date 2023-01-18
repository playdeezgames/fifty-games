Friend Module TitleHandler
    Friend Sub Run(world As World)
        AnsiConsole.Clear()
        Dim figlet As New FigletText("Itinerant Slayer of SPLORR!!") With {.Alignment = Justify.Center, .Color = Color.Lime}
        AnsiConsole.Write(figlet)
        OkPrompt()
        MainMenuHandler.Run(world)
    End Sub
End Module
