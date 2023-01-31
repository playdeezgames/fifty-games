Module Town
    Friend ReadOnly map As IReadOnlyList(Of String) = New List(Of String) From
        {
            "##################################################",
            "#................................................#",
            "#....................###.........................#",
            "#....................# #..................?......#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,..........................#",
            "#.....................,....###...................#",
            "#.....................,....# #...................#",
            "#.....................,.....,....................#",
            "#.....................,.....,....................#",
            "#.....................,,,,,,,....................#",
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
            "#.......................,.?......................#",
            "#.......................,........................#",
            "########################,#########################"
        }
    Friend characters As IReadOnlyList(Of (CharacterType, Integer, Integer)) =
        New List(Of (CharacterType, Integer, Integer))
    Friend triggers As IReadOnlyList(Of (IReadOnlyList(Of TriggerData), Integer, Integer)) =
        New List(Of (IReadOnlyList(Of TriggerData), Integer, Integer)) From
        {
            (
                New List(Of TriggerData) From
                {
                    New TriggerData With
                    {
                        .Condition = TriggerConditionType.WhenFlagClear,
                        .ConditionFlag = "Condition",
                        .TriggerType = TriggerType.Message,
                        .Message = New MessageData With
                        {
                            .Text = "First Time"
                        }
                    },
                    New TriggerData With
                    {
                        .Condition = TriggerConditionType.WhenFlagSet,
                        .ConditionFlag = "Condition",
                        .TriggerType = TriggerType.Message,
                        .Message = New MessageData With
                        {
                            .Text = "Subsequent Time"
                        }
                    },
                    New TriggerData With
                    {
                        .Condition = TriggerConditionType.WhenFlagClear,
                        .ConditionFlag = "Condition",
                        .TriggerType = TriggerType.SetFlag,
                        .Flag = "Condition"
                    }
                },
                42, 3),
            (
                New List(Of TriggerData) From
                {
                    New TriggerData With
                    {
                        .TriggerType = TriggerType.Message,
                        .Message = New MessageData With
                        {
                            .Text = "Welcome to the Town of Quotidian!"
                        }
                    },
                    New TriggerData With
                    {
                        .TriggerType = TriggerType.Message,
                        .Message = New MessageData With
                        {
                            .Text = "Please don't feed the bears."
                        }
                    }
                },
                26, 22),
            (
                New List(Of TriggerData) From
                {
                    New TriggerData With
                    {
                        .TriggerType = TriggerType.Teleport,
                        .Teleport = New TeleportData With
                        {
                            .DestinationX = 6,
                            .DestinationY = 3,
                            .DestinationBoard = 0
                        }
                    }
                },
                24, 24),
            (New List(Of TriggerData) From {New TriggerData With
                {
                    .TriggerType = TriggerType.Inn,
                    .Inn = New InnData With
                    {
                        .Name = "Graham's Inn (Sometimes)",
                        .Price = 3
                    }
                }},
                22, 3),
            (New List(Of TriggerData) From {
                New TriggerData With
                {
                    .TriggerType = TriggerType.Message,
                    .Condition = TriggerConditionType.WhenFlagClear,
                    .ConditionFlag = "Condition",
                    .Message = New MessageData With
                    {
                        .Text = "Go away!"
                    }
                },
                New TriggerData With
                {
                    .TriggerType = TriggerType.StopTriggers,
                    .Condition = TriggerConditionType.WhenFlagClear,
                    .ConditionFlag = "Condition"
                },
                New TriggerData With
                {
                    .TriggerType = TriggerType.Shoppe,
                    .Shoppe = New ShoppeData With
                    {
                        .Name = "Märten's Nihilist Healing Supplies",
                        .Prices = New Dictionary(Of ItemType, (Integer, Integer)) From
                        {
                            {ItemType.Potion, (6, Integer.MaxValue)}
                        },
                        .Offers = New Dictionary(Of ItemType, Integer) From
                        {
                            {ItemType.EmptyBottle, 1}
                        }
                    }
                }},
                5, 19),
            (New List(Of TriggerData) From {New TriggerData With
                {
                    .TriggerType = TriggerType.Shoppe,
                    .Shoppe = New ShoppeData With
                    {
                        .Name = "Samuli's Blacksmithery",
                        .Prices = New Dictionary(Of ItemType, (Integer, Integer)) From
                        {
                            {ItemType.RustyDagger, (0, 1)},
                            {ItemType.Dagger, (25, 1)},
                            {ItemType.LeatherArmor, (50, 1)}
                        },
                        .Offers = New Dictionary(Of ItemType, Integer) From
                        {
                            {ItemType.Dagger, 12},
                            {ItemType.LeatherArmor, 25}
                        }
                    }
                }},
                28, 8)
        }
    Friend ReadOnly defaultTerrain As TerrainType = TerrainType.Grass
    Friend ReadOnly encounterZones As IReadOnlyList(Of EncounterZoneData) =
        New List(Of EncounterZoneData)
End Module
