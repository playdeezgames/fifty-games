Public Module LandyMcLanderface
    Public Sub Run(data As LandyMcLanderfaceData)
        Do
            AnsiConsole.Clear()
            If data.AttemptCount > 0 Then
                AnsiConsole.MarkupLine($"Attempts Made: {data.AttemptCount}")
                AnsiConsole.MarkupLine($"Successful Landings: {data.SuccessCount}")
                AnsiConsole.MarkupLine($"Success Percentage: {100.0 * data.SuccessCount / data.AttemptCount:f}%")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Landy McLanderface[/]"}
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
    Private Const ChangeThrustText = "Change Thrust"
    Private Const NextTurnText = "Next Turn"

    Private Sub PlayGame(data As LandyMcLanderfaceData)
        Dim altitude = 1000.0
        Dim velocity = -10.0
        Dim thruster = 0.0
        Dim fuel = 20.0
        Dim turns = 0
        Const gravity = -1.62
        Const thrusterAcceleration = 5.0
        data.AttemptCount += 1
        Do While altitude > 0.0
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Turn: {turns}")
            AnsiConsole.MarkupLine($"Altitude: {altitude:f}")
            AnsiConsole.MarkupLine($"Velocity: {velocity:f}")
            AnsiConsole.MarkupLine($"Fuel: {fuel:f}")
            AnsiConsole.MarkupLine($"Thrust: {thruster * 100.0:f}%")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoice(NextTurnText)
            prompt.AddChoices(ChangeThrustText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChangeThrustText
                    thruster = Math.Clamp(AnsiConsole.Ask("[olive]New Thrust Percentage?[/]", 0.0), 0.0, 100.0) / 100.0
                Case NextTurnText
                    thruster = Math.Clamp(thruster, 0.0, fuel)
                    fuel -= thruster
                    Dim acceleration = thrusterAcceleration * thruster + gravity
                    altitude += (acceleration / 2 + velocity)
                    velocity += acceleration
                    turns += 1
            End Select
        Loop
        Select Case Math.Abs(velocity)
            Case Is < 1.0
                data.SuccessCount += 1
                data.PerfectCount += 1
                AnsiConsole.MarkupLine("Perfect landing!")
            Case Is < 10.0
                data.SuccessCount += 1
                AnsiConsole.MarkupLine("Any landing you can walk away from is a good landing!")
            Case Else
                AnsiConsole.MarkupLine("You and the lander are now a greasy spot.")
        End Select
        OkPrompt()
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Landy Mc Landerface"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You are attempting to land yer landing craft, named ""Landy McLanderface"" on the moon.

You start at a particular velocity.

You start at a particular altitude.

You have thrusters that you can engage to slow yer descent.

Yer goal is to reach the ground as close to zero velocity as you can to minimize the damage to the lander.

Good luck!")
        Common.OkPrompt()
    End Sub
End Module
