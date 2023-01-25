Friend Module GameOverHandler
    Friend Sub ShowGameOver(world As IWorld)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Game Over!")
        OkPrompt()
    End Sub
End Module
