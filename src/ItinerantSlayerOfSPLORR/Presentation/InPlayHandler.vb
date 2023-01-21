﻿Friend Module InPlayHandler
    Friend Sub Run(world As IWorld)
        Dim random As New Random
        AnsiConsole.Clear()
        Do
            If world.IsInAnEncounter Then
                ShowEncounter(world)
            Else
                If ShowBoard(random, world) Then
                    Exit Do
                End If
            End If
        Loop
    End Sub

    Private Sub ShowEncounter(world As IWorld)
        'run
        'fight
        'use item
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Encounter Type: {world.Encounter.EncounterType}")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(FleeText)
        Select Case AnsiConsole.Prompt(prompt)
            Case FleeText
                world.FleeEncounter()
        End Select
        AnsiConsole.Clear()
    End Sub

    Private Function ShowBoard(random As Random, world As IWorld) As Boolean
        AnsiConsole.Cursor.Hide
        AnsiConsole.Cursor.SetPosition(1, 1)
        ShowPlayerBoard(world)
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
                Return True
        End Select
        Return False
    End Function

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
