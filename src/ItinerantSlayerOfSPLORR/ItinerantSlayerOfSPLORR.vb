Public Module ItinerantSlayerOfSPLORR
    Public Sub Run(data As WorldData)
        Dim world As New World(data)
        TitleHandler.Run(world)
    End Sub
End Module
