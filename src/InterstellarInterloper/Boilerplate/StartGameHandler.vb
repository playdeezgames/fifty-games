Friend Module StartGameHandler
    Private Const StarCount = 20
    Private Const TotalTurns = 20
    Private Const InitialShips = 10
    Friend Sub Run(data As InterstellarInterloperData, random As Random)
        'TODO: allow for customization of number of worlds, players, etc
        data.TurnsRemaining = TotalTurns
        data.OwnersTurn = 0
        data.Stars = New List(Of StarData)
        data.Fleets = New List(Of FleetData)
        data.Owners = New List(Of OwnerData) From {
            New OwnerData With {.IsHuman = True}
        }
        While data.Stars.Count < StarCount
            Dim candidate As New StarData With
                {
                    .Owner = Nothing,
                    .Ships = InitialShips,
                    .X = random.Next(MapColumns),
                    .Y = random.Next(MapRows)
                }
            If Not data.Stars.Any(Function(star) star.X = candidate.X AndAlso star.Y = candidate.Y) Then
                data.Stars.Add(candidate)
            End If
        End While
        data.Owners = New List(Of OwnerData) From {
            New OwnerData With {.IsHuman = True},
            New OwnerData With {.IsHuman = False}
        }
        SpawnOwners(data, random)
    End Sub

    Private Sub SpawnOwners(data As InterstellarInterloperData, random As Random)
        Dim ownerIndex = 0
        For Each owner In data.Owners
            Dim available = data.Stars.Where(Function(star) star.Owner Is Nothing).ToList
            available(random.Next(available.Count)).Owner = ownerIndex
            ownerIndex += 1
        Next
    End Sub
End Module
