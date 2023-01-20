﻿Friend Module Overworld

    Friend ReadOnly map As IReadOnlyList(Of String) = New List(Of String) From
        {
            "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
            "~~~~~~....................................................................................................................................................................................................................................~~~~~~",
            "~~~~........................................................................................................................................................................................................................................~~~~",
            "~~....!.......................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~............................................................................................................................................................................................................................................~~",
            "~~~~........................................................................................................................................................................................................................................~~~~",
            "~~~~~~....................................................................................................................................................................................................................................~~~~~~",
            "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
        }
    Friend characters As IReadOnlyList(Of (CharacterType, Integer, Integer)) =
        New List(Of (CharacterType, Integer, Integer)) From
        {
            (CharacterType.Dude, 6, 3)
        }
    Friend triggers As IReadOnlyList(Of (TriggerData, Integer, Integer)) =
        New List(Of (TriggerData, Integer, Integer)) From
        {
            (New TriggerData With
                {
                    .TriggerType = TriggerType.Teleport,
                    .Teleport = New TeleportData With
                    {
                        .DestinationX = 24,
                        .DestinationY = 23,
                        .DestinationBoard = 1
                    }
                },
                6, 3)
        }
    Friend ReadOnly defaultTerrain As TerrainType = TerrainType.Water
End Module