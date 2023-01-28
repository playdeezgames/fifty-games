Friend Module BoardHandler
    Friend Function ShowBoard(random As Random, world As IWorld) As Boolean
        AnsiConsole.Cursor.Hide
        AnsiConsole.Cursor.SetPosition(1, 1)
        ShowPlayerBoard(world)
        ShowPlayerStatistics(world)
        Dim key = WaitForKey()
        Select Case key
            Case ConsoleKey.UpArrow
                world.MoveNorth(random)
            Case ConsoleKey.DownArrow
                world.MoveSouth(random)
            Case ConsoleKey.LeftArrow
                world.MoveWest(random)
            Case ConsoleKey.RightArrow
                world.MoveEast(random)
            Case ConsoleKey.Escape
                Return InGameMenuHandler.Run(world, random)
        End Select
        Return False
    End Function

    Private Sub ShowPlayerStatistics(world As IWorld)
        Dim character = world.PlayerCharacter
        AnsiConsole.Cursor.SetPosition(50, 1)
        AnsiConsole.Markup($"Level: {character.Level}")
        AnsiConsole.Cursor.SetPosition(50, 2)
        AnsiConsole.Markup($"HP: {character.HitPoints}/{character.MaximumHitPoints}")
        AnsiConsole.Cursor.SetPosition(50, 3)
        If character.HasLeveledUp Then
            AnsiConsole.Markup($"XP: LEVEL UP!")
        Else
            AnsiConsole.Markup($"XP: {character.XP}/{character.XPGoal}")
        End If
        AnsiConsole.Cursor.SetPosition(50, 4)
        AnsiConsole.Markup($"Jools: {character.Jools}")
    End Sub

    Private Const HorizontalFieldOfView = 24
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
                    RenderTerrain(board.DefaultTerrain)
                    Continue For
                End If
                Dim character = cell.Character
                If character IsNot Nothing Then
                    RenderCharacter(character)
                    Continue For
                End If
                RenderTerrain(cell.Terrain)
            Next
        Next
    End Sub

    Private Sub RenderCharacter(character As ICharacter)
        Select Case character.CharacterType
            Case CharacterType.Dude
                AnsiConsole.Markup("[white on black]☺[/]")
            Case Else
                AnsiConsole.Markup("[white on black]☻[/]")
        End Select
    End Sub

    Private Sub RenderTerrain(terrain As TerrainType)
        Select Case terrain
            Case TerrainType.Grass
                AnsiConsole.Markup("[green on black].[/]")
            Case TerrainType.Water
                AnsiConsole.Markup("[navy on blue]≈[/]")
            Case TerrainType.Home
                AnsiConsole.Markup("[white on black]■[/]")
            Case TerrainType.Wall
                AnsiConsole.Markup("[black on grey]#[/]")
            Case TerrainType.Empty
                AnsiConsole.Markup("[black on black] [/]")
            Case TerrainType.Road
                AnsiConsole.Markup("[grey on black]░[/]")
            Case TerrainType.Sign
                AnsiConsole.Markup("[olive on black]?[/]")
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub

    Private Function WaitForKey() As ConsoleKey
        Dim key As ConsoleKey?
        Do
            key = AnsiConsole.Console.Input.ReadKey(True)?.Key
        Loop Until key.HasValue
        Return key.Value
    End Function

End Module
