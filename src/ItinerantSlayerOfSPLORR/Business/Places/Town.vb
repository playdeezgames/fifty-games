﻿Module Town
    Friend ReadOnly map As IReadOnlyList(Of String) = New List(Of String) From
        {
            "##################################################",
            "#................................................#",
            "#....................###.........................#",
            "#....................# #.........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,,,........................#",
            "#.......................,........................#",
            "#.......................,........................#",
            "#.......................,........................#",
            "#.......................,........................#",
            "#.......................,........................#",
            "#.......................,........................#",
            "#...###.................,........................#",
            "#...# #.................,........................#",
            "#....,..................,........................#",
            "#....,,,,,,,,,,,,,,,,,,,,........................#",
            "#.......................,........................#",
            "#.......................,........................#",
            "########################,#########################"
        }
    Friend characters As IReadOnlyList(Of (CharacterType, Integer, Integer)) =
        New List(Of (CharacterType, Integer, Integer))
    Friend triggers As IReadOnlyList(Of (TriggerData, Integer, Integer)) =
        New List(Of (TriggerData, Integer, Integer)) From
        {
            (New TriggerData With
                {
                    .TriggerType = TriggerType.Teleport,
                    .Teleport = New TeleportData With
                    {
                        .DestinationX = 6,
                        .DestinationY = 3,
                        .DestinationBoard = 0
                    }
                },
                24, 24),
            (New TriggerData With
                {
                    .TriggerType = TriggerType.Inn,
                    .Inn = New InnData With
                    {
                        .Name = "Graham's Inn (Sometimes)",
                        .Price = 3
                    }
                },
                22, 3),
            (New TriggerData With
                {
                    .TriggerType = TriggerType.Shoppe,
                    .Shoppe = New ShoppeData With
                    {
                        .Name = "Märten's Nihilist Healing Supplies"
                    }
                },
                5, 19)
        }
    Friend ReadOnly defaultTerrain As TerrainType = TerrainType.Grass
    Friend ReadOnly encounterZones As IReadOnlyList(Of EncounterZoneData) =
        New List(Of EncounterZoneData)
End Module
