Friend Module ShipProductionHandler
    Friend Sub Run(data As InterstellarInterloperData, random As Random)
        For Each star In data.Stars
            If random.Next(1, 7) + random.Next(1, 7) > star.Ships Then
                star.Ships += 1
            End If
        Next
    End Sub
End Module
