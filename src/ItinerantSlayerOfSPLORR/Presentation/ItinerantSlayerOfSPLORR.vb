Public Module ItinerantSlayerOfSPLORR
    Public Sub Run(data As WorldData)
        Dim world As IWorld = New World(data)
        TitleHandler.Run(world)
    End Sub
End Module
