Imports System.Security

Friend Module InPlayHandler
    Friend Sub Run(world As IWorld)
        AnsiConsole.Clear()
        AnsiConsole.Cursor.Hide
        Do
            ShowPlayerBoard(world)
            Dim key = WaitForKey()
            Select Case key
                Case ConsoleKey.UpArrow
                    world.MoveNorth()
                Case ConsoleKey.DownArrow
                    world.MoveSouth()
                Case ConsoleKey.LeftArrow
                    world.MoveWest()
                Case ConsoleKey.RightArrow
                    world.MoveEast()
                Case ConsoleKey.Escape
                    Exit Do
            End Select
        Loop
        AnsiConsole.Cursor.Show
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
                AnsiConsole.Markup("[white on black]?[/]")
        End Select
    End Sub

    Private Sub RenderTerrain(terrain As TerrainType)
        Select Case terrain
            Case TerrainType.Grass
                AnsiConsole.Markup("[green on black].[/]")
            Case TerrainType.Water
                AnsiConsole.Markup("[navy on blue]≈[/]")
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub

    Friend Function WaitForKey() As ConsoleKey
        Dim key As ConsoleKey?
        Do
            key = AnsiConsole.Console.Input.ReadKey(True)?.Key
        Loop Until key.HasValue
        Return key.Value
    End Function
End Module
