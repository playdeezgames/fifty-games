Friend Interface IWorld
    ReadOnly Property CanContinue As Boolean
    ReadOnly Property CanAbandon As Boolean
    ReadOnly Property CanStart As Boolean
    ReadOnly Property PlayerBoard As IBoard
    ReadOnly Property Player As IPlayer
    Sub StartGame()
    Sub AbandonGame()
    Sub MoveNorth()
    Sub MoveSouth()
    Sub MoveWest()
    Sub MoveEast()
End Interface
