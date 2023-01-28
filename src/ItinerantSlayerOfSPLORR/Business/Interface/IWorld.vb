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
    Sub FleeEncounter()
    Function Attack(enemy As IEnemy, random As Random) As IEnumerable(Of String)
    Function UseItem(itemType As ItemType, random As Random) As IEnumerable(Of String)
    Function EquipItem(itemType As ItemType) As IEnumerable(Of String)
    Sub ProceedToNextTrigger()
    ReadOnly Property HasMoreTriggers As Boolean
    ReadOnly Property Message As IMessage
    ReadOnly Property Inn As IInn
    ReadOnly Property PlayerCharacter As ICharacter
    ReadOnly Property IsInAnEncounter As Boolean
    ReadOnly Property Shoppe As IShoppe
End Interface
