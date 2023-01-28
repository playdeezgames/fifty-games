Public Interface ICharacter
    ReadOnly Property CharacterType As CharacterType
    Function RollAttack(random As Random) As Integer
    Sub AddXP(amount As Integer)
    ReadOnly Property Name As String
    ReadOnly Property XP As Integer
    ReadOnly Property XPGoal As Integer
    ReadOnly Property Level As Integer
    ReadOnly Property IsDead As Boolean
    ReadOnly Property HitPoints As Integer
    ReadOnly Property MaximumHitPoints As Integer
    Property IsInInn As Boolean
    Function Attack(enemy As IEnemy, random As Random) As IEnumerable(Of String)
    Function RollDefend(random As Random) As Integer
    Sub TakeDamage(damage As Integer)
    ReadOnly Property Jools As Integer
    Sub RestAtInn(inn As IInn)
    Sub LevelUpHitPoints()
    Sub LevelUpDefendStrength()
    Sub LevelUpAttackStrength()
    Sub BuyItems(shoppe As IShoppe, itemType As ItemType, quantity As Integer)
    Function HasItem(itemType As ItemType) As Boolean
    Function UseItem(itemType As ItemType) As IEnumerable(Of String)
    Sub Heal(healingAmount As Integer)
    Sub AddItems(emptyBottle As ItemType, quantity As Integer)
    Sub RemoveItems(itemType As ItemType, quantity As Integer)
    Sub SellItems(shoppe As IShoppe, itemType As ItemType, quantity As Integer)
    Function EquipItem(itemType As ItemType) As IEnumerable(Of String)
    ReadOnly Property Equipment As IEnumerable(Of (EquipSlotType, ItemType))
    ReadOnly Property HasEquipment As Boolean
    ReadOnly Property CanEquipItem As Boolean
    ReadOnly Property Items As IEnumerable(Of (ItemType, Integer))
    ReadOnly Property HasItems As Boolean
    ReadOnly Property UsableItems As IEnumerable(Of ItemType)
    ReadOnly Property CanUseItem As Boolean
    ReadOnly Property HasLeveledUp As Boolean
    ReadOnly Property AttackStrength As Integer
    ReadOnly Property DefendStrength As Integer
    ReadOnly Property HitPointIncrease As Integer
    ReadOnly Property AttackStrengthIncrease As Integer
    ReadOnly Property DefendStrengthIncrease As Integer
    Property IsInShoppe As Boolean
    ReadOnly Property ItemCount(itemType As ItemType) As Integer
    ReadOnly Property EquippableItems As IEnumerable(Of ItemType)
    Property IsInMessage As Boolean
End Interface
