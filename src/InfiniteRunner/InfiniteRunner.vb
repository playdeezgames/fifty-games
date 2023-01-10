Imports System.Net.Http

Public Module InfiniteRunner
    Public Sub Run(data As InfiniteRunnerData)
        Do
            AnsiConsole.Clear()
            If data.GamesPlayed > 0 Then
                AnsiConsole.MarkupLine($"Games Played: {data.GamesPlayed}")
                AnsiConsole.MarkupLine($"High Score: {data.HighScore}")
                AnsiConsole.MarkupLine($"Average Score: {data.TotalScore / data.GamesPlayed:f}")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Infinite Runner:[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                    PlayGame(data)
                Case HowToPlayText
                    ShowInstructions()
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Infinite Runner"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("In infinite runner, you are traveling at a constant speed down a three lane corridor.

There are things to run into that give you points.

There are things to run into that end the game.

You can move between the lanes with the arrow keys.

The goal is to get a high score.")
        Common.OkPrompt()
    End Sub

    Private Sub PlayGame(data As InfiniteRunnerData)
        Const LaneRows = 25
        Const FrameSeconds = 0.25
        Dim random As New Random
        AnsiConsole.Clear()
        AnsiConsole.Cursor.Hide
        Dim items(LaneRows) As (Integer, Char)
        For row = 1 To LaneRows
            items(row - 1) = (3, " "c)
            AnsiConsole.Cursor.SetPosition(1, row)
            AnsiConsole.Markup("#   |   |   #")
        Next
        Dim currentPosition = 7
        AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
        AnsiConsole.Markup("@")
        Dim nextTime = DateTimeOffset.Now.AddSeconds(FrameSeconds)
        Dim score = 0
        Dim alive = True
        Do
            If AnsiConsole.Console.Input.IsKeyAvailable Then
                Select Case AnsiConsole.Console.Input.ReadKey(True).Value.Key
                    Case ConsoleKey.Escape
                        Exit Do
                    Case ConsoleKey.LeftArrow
                        If currentPosition > 3 Then
                            AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
                            AnsiConsole.Markup(" ")
                            currentPosition -= 4
                        End If
                    Case ConsoleKey.RightArrow
                        If currentPosition < 11 Then
                            AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
                            AnsiConsole.Markup(" ")
                            currentPosition += 4
                        End If
                End Select
            End If
            AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
            AnsiConsole.Markup("@")
            If DateTimeOffset.Now >= nextTime Then
                nextTime = DateTimeOffset.Now.AddSeconds(FrameSeconds)
                For row = LaneRows To 1 Step -1
                    AnsiConsole.Cursor.SetPosition(items(row - 1).Item1, row)
                    AnsiConsole.Markup(" ")

                    If row > 1 Then
                        items(row - 1) = items(row - 2)
                        If row = LaneRows - 1 AndAlso currentPosition = items(row - 1).Item1 Then
                            Select Case items(row - 1).Item2
                                Case "$"c
                                    AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
                                    AnsiConsole.Markup("@")
                                    score += 1
                                Case "X"c
                                    AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
                                    AnsiConsole.Markup("*")
                                    alive = False
                                Case Else
                                    AnsiConsole.Cursor.SetPosition(currentPosition, LaneRows)
                                    AnsiConsole.Markup("@")
                            End Select
                        End If
                    Else
                        Dim c As Char = " "c
                        Select Case random.Next(100)
                            Case Is < 50
                                c = "$"c
                            Case Is < 60
                                c = "X"c
                        End Select
                        items(row - 1) = (random.Next(3) * 4 + 3, c)
                    End If
                    AnsiConsole.Cursor.SetPosition(items(row - 1).Item1, row)
                    AnsiConsole.Markup(items(row - 1).Item2)
                Next
            End If
            AnsiConsole.Cursor.SetPosition(1, LaneRows + 1)
            AnsiConsole.Markup($"Score: {score}  ")
            If Not alive Then
                Exit Do
            End If
        Loop
        AnsiConsole.Cursor.SetPosition(1, LaneRows + 2)
        AnsiConsole.MarkupLine("You died!")
        OkPrompt()
        AnsiConsole.Cursor.Show
        data.GamesPlayed += 1
        data.TotalScore += score
        data.HighScore = Math.Max(score, data.HighScore)
    End Sub
End Module
