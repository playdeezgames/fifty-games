Friend Interface IWorld
    ReadOnly Property CanContinue As Boolean
    ReadOnly Property CanAbandon As Boolean
    ReadOnly Property CanStart As Boolean
    ReadOnly Property PlayerBoard As IBoard
    ReadOnly Property Player As IPlayer
    Property Encounter As IEncounter
    Sub StartGame()
    Sub AbandonGame()
    Sub MoveNorth(random As Random)
    Sub MoveSouth(random As Random)
    Sub MoveWest(random As Random)
    Sub MoveEast(random As Random)
    ReadOnly Property PlayerCharacter As ICharacter
End Interface
