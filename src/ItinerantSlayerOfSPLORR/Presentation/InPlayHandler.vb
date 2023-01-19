Imports System.Security

Friend Module InPlayHandler
    Friend Sub Run(world As IWorld)
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
    Private Const HorizontalFieldOfView = 12
    Private Const VerticalFieldOfView = 12
    Private Sub ShowPlayerBoard(world As IWorld)
        Dim board As IBoard = world.PlayerBoard
        Dim player As IPlayer = world.Player
        Dim leftX As Integer = player.X - HorizontalFieldOfView
        Dim rightX As Integer = player.X + HorizontalFieldOfView
        Dim topY As Integer = player.Y - VerticalFieldOfView
        Dim bottomY As Integer = player.Y + VerticalFieldOfView
        For y = topY To bottomY
            For x = leftX To rightX
                Dim plotX = x - leftX + 1
                Dim plotY = y - topY + 1
                AnsiConsole.Cursor.SetPosition(plotX, plotY)
                Dim cell = board.GetCell(x, y)
                If cell Is Nothing Then
                    Continue For
                End If
                Dim character = cell.Character
                If character IsNot Nothing Then
                    Select Case character.CharacterType
                        Case CharacterType.Dude
                            AnsiConsole.Markup("[white on black]☺[/]")
                    End Select
                    Continue For
                End If
                Dim terrain As TerrainType = cell.Terrain
                Select Case terrain
                    Case TerrainType.Grass
                        AnsiConsole.Markup("[green on black].[/]")
                    Case TerrainType.Water
                        AnsiConsole.Markup("[navy on blue]≈[/]")
                End Select
            Next
        Next
    End Sub

    Friend Function WaitForKey() As ConsoleKey
        Dim key As ConsoleKey?
        Do
            key = AnsiConsole.Console.Input.ReadKey(True)?.Key
        Loop Until key.HasValue
        Return key.Value
    End Function
End Module
