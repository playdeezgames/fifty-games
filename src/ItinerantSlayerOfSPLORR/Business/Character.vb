Friend Class Character
    Implements ICharacter
    Friend _data As CharacterData
    Sub New(data As CharacterData)
        _data = data
    End Sub

    Public ReadOnly Property CharacterType As CharacterType Implements ICharacter.CharacterType
        Get
            Return _data.CharacterType
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return _data.CharacterType.ToDescriptor().Name
        End Get
    End Property

    Public ReadOnly Property XP As Integer Implements ICharacter.XP
        Get
            Return _data.XP
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements ICharacter.IsDead
        Get
            Return HitPoints = 0
        End Get
    End Property

    Public ReadOnly Property HitPoints As Integer Implements ICharacter.HitPoints
        Get
            Return Math.Clamp(MaximumHitPoints - _data.Wounds, 0, MaximumHitPoints)
        End Get
    End Property

    Public ReadOnly Property MaximumHitPoints As Integer Implements ICharacter.MaximumHitPoints
        Get
            Return _data.CharacterType.ToDescriptor.HitPoints + _data.MaximumHitPoints
        End Get
    End Property

    Public Property IsInShoppe As Boolean Implements ICharacter.IsInShoppe
        Get
            Return _data.IsInShoppe
        End Get
        Set(value As Boolean)
            _data.IsInShoppe = value
        End Set
    End Property

    Public Property IsInInn As Boolean Implements ICharacter.IsInInn
        Get
            Return _data.IsInInn
        End Get
        Set(value As Boolean)
            _data.IsInInn = value
        End Set
    End Property

    Public ReadOnly Property Jools As Integer Implements ICharacter.Jools
        Get
            Return _data.Jools
        End Get
    End Property

    Private ReadOnly Property EquippedItemAttackStrength As Integer
        Get
            Return Equipment.Sum(Function(x) x.Item2.ToDescriptor.AttackStrength)
        End Get
    End Property

    Public ReadOnly Property AttackStrength As Integer Implements ICharacter.AttackStrength
        Get
            Return _data.CharacterType.ToDescriptor().Attack + _data.AttackStrength + EquippedItemAttackStrength
        End Get
    End Property

    Public ReadOnly Property DefendStrength As Integer Implements ICharacter.DefendStrength
        Get
            Return _data.CharacterType.ToDescriptor().Defend + _data.DefendStrength
        End Get
    End Property

    Public ReadOnly Property Level As Integer Implements ICharacter.Level
        Get
            Return _data.Level
        End Get
    End Property

    Public ReadOnly Property XPGoal As Integer Implements ICharacter.XPGoal
        Get
            Return _data.CharacterType.ToDescriptor.XPGoal(Level)
        End Get
    End Property

    Public ReadOnly Property HasLeveledUp As Boolean Implements ICharacter.HasLeveledUp
        Get
            Return XP >= XPGoal
        End Get
    End Property

    Public ReadOnly Property HitPointIncrease As Integer Implements ICharacter.HitPointIncrease
        Get
            Return _data.CharacterType.ToDescriptor.HitPointIncrease
        End Get
    End Property

    Public ReadOnly Property AttackStrengthIncrease As Integer Implements ICharacter.AttackStrengthIncrease
        Get
            Return _data.CharacterType.ToDescriptor.AttackStrengthIncrease
        End Get
    End Property

    Public ReadOnly Property DefendStrengthIncrease As Integer Implements ICharacter.DefendStrengthIncrease
        Get
            Return _data.CharacterType.ToDescriptor.DefendStrengthIncrease
        End Get
    End Property

    Public ReadOnly Property CanUseItem As Boolean Implements ICharacter.CanUseItem
        Get
            Return _data.Inventory.Keys.Any(Function(x) x.ToDescriptor.CanUse)
        End Get
    End Property

    Public ReadOnly Property UsableItems As IEnumerable(Of ItemType) Implements ICharacter.UsableItems
        Get
            Return _data.Inventory.Keys.Where(Function(x) x.ToDescriptor.CanUse)
        End Get
    End Property

    Public ReadOnly Property ItemCount(itemType As ItemType) As Integer Implements ICharacter.ItemCount
        Get
            If Not _data.Inventory.ContainsKey(itemType) Then
                Return 0
            End If
            Return _data.Inventory(itemType)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements ICharacter.HasItems
        Get
            Return _data.Inventory.Any
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of (ItemType, Integer)) Implements ICharacter.Items
        Get
            Return _data.Inventory.Select(Function(x) (x.Key, x.Value))
        End Get
    End Property

    Public ReadOnly Property CanEquipItem As Boolean Implements ICharacter.CanEquipItem
        Get
            Return _data.Inventory.Any(Function(x) x.Key.ToDescriptor.CanEquip)
        End Get
    End Property

    Public ReadOnly Property EquippableItems As IEnumerable(Of ItemType) Implements ICharacter.EquippableItems
        Get
            Return _data.Inventory.Keys.Where(Function(x) x.ToDescriptor.CanEquip)
        End Get
    End Property

    Public ReadOnly Property HasEquipment As Boolean Implements ICharacter.HasEquipment
        Get
            Return _data.Equipment.Any
        End Get
    End Property

    Public ReadOnly Property Equipment As IEnumerable(Of (EquipSlotType, ItemType)) Implements ICharacter.Equipment
        Get
            Return _data.Equipment.Select(Function(x) (x.Key, x.Value))
        End Get
    End Property

    Public Sub AddXP(amount As Integer) Implements ICharacter.AddXP
        _data.XP += amount
    End Sub

    Public Sub TakeDamage(damage As Integer) Implements ICharacter.TakeDamage
        _data.Wounds += damage
    End Sub

    Public Sub RestAtInn(inn As IInn) Implements ICharacter.RestAtInn
        _data.Jools -= inn.Price
        _data.Wounds = 0
    End Sub

    Public Function RollAttack(random As Random) As Integer Implements ICharacter.RollAttack
        Return random.Next(AttackStrength) + 1
    End Function

    Public Function Attack(enemy As IEnemy, random As Random) As IEnumerable(Of String) Implements ICharacter.Attack
        Dim messages As New List(Of String)
        Dim attackRoll = RollAttack(random)
        messages.Add($"{Name} rolls an attack of {attackRoll}.")
        Dim defendRoll = enemy.RollDefend(random)
        messages.Add($"{enemy.Name} rolls a defend of {defendRoll}")
        If attackRoll > defendRoll Then
            Dim damage = attackRoll - defendRoll
            messages.Add($"{Name} hits for {damage} damage!")
            enemy.TakeDamage(damage)
            If enemy.IsDead Then
                messages.Add($"{Name} kills {enemy.Name}!")
                If enemy.XP > 0 Then
                    messages.Add($"{Name} gets {enemy.XP} XP!")
                    AddXP(enemy.XP)
                End If
                Dim jools = enemy.RollJools(random)
                If jools > 0 Then
                    AddJools(jools)
                    messages.Add($"{Name} gets {jools} jools!")
                End If
            End If
        Else
            messages.Add($"{Name} misses!")
        End If
        Return messages
    End Function

    Private Sub AddJools(jools As Integer)
        _data.Jools += jools
    End Sub

    Public Function RollDefend(random As Random) As Integer Implements ICharacter.RollDefend
        Return random.Next(DefendStrength) + 1
    End Function

    Public Sub LevelUpHitPoints() Implements ICharacter.LevelUpHitPoints
        If Not HasLeveledUp Then
            Return
        End If
        _data.MaximumHitPoints += HitPointIncrease
        IncreaseXPLevel()
    End Sub

    Private Sub IncreaseXPLevel()
        _data.XP -= XPGoal
        _data.Level += 1
    End Sub

    Public Sub LevelUpDefendStrength() Implements ICharacter.LevelUpDefendStrength
        If Not HasLeveledUp Then
            Return
        End If
        _data.DefendStrength += DefendStrengthIncrease
        IncreaseXPLevel()
    End Sub

    Public Sub LevelUpAttackStrength() Implements ICharacter.LevelUpAttackStrength
        If Not HasLeveledUp Then
            Return
        End If
        _data.AttackStrength += AttackStrengthIncrease
        IncreaseXPLevel()
    End Sub

    Public Sub BuyItems(shoppe As IShoppe, itemType As ItemType, quantity As Integer) Implements ICharacter.BuyItems
        _data.Jools -= shoppe.Prices(itemType) * quantity
        AddItems(itemType, quantity)
    End Sub

    Public Sub AddItems(itemType As ItemType, quantity As Integer) Implements ICharacter.AddItems
        quantity += If(_data.Inventory.ContainsKey(itemType), _data.Inventory(itemType), 0)
        _data.Inventory(itemType) = quantity
    End Sub

    Public Function HasItem(itemType As ItemType) As Boolean Implements ICharacter.HasItem
        Return _data.Inventory.ContainsKey(itemType)
    End Function

    Public Function UseItem(itemType As ItemType) As IEnumerable(Of String) Implements ICharacter.UseItem
        Dim messages As New List(Of String)
        If Not HasItem(itemType) Then
            messages.Add($"{Name} has no {itemType.ToDescriptor.Name} to use!")
            Return messages
        End If
        If Not itemType.ToDescriptor.CanUse Then
            messages.Add($"{Name} cannot use {itemType.ToDescriptor.Name}!")
            Return messages
        End If
        messages.AddRange(itemType.ToDescriptor.UseBy(Me))
        Return messages
    End Function

    Public Sub Heal(healingAmount As Integer) Implements ICharacter.Heal
        TakeDamage(-healingAmount)
    End Sub

    Public Sub RemoveItems(itemType As ItemType, quantity As Integer) Implements ICharacter.RemoveItems
        If Not _data.Inventory.ContainsKey(itemType) Then
            Return
        End If
        Dim currentQuantity = _data.Inventory(itemType)
        If quantity >= currentQuantity Then
            _data.Inventory.Remove(itemType)
            Return
        End If
        _data.Inventory(itemType) = currentQuantity - quantity
    End Sub

    Public Sub SellItems(shoppe As IShoppe, itemType As ItemType, quantity As Integer) Implements ICharacter.SellItems
        _data.Jools += shoppe.Offers(itemType) * quantity
        RemoveItems(itemType, quantity)
    End Sub

    Public Function EquipItem(itemType As ItemType) As IEnumerable(Of String) Implements ICharacter.EquipItem
        Dim messages As New List(Of String)
        If Not itemType.ToDescriptor.CanEquip Then
            messages.Add($"{Name} cannot equip {itemType.ToDescriptor.Name}.")
            Return messages
        End If
        If Not HasItem(itemType) Then
            messages.Add($"{Name} has no {itemType.ToDescriptor.Name} to equip.")
            Return messages
        End If
        Dim equipSlot As EquipSlotType = itemType.ToDescriptor.EquipSlot.Value
        Equip(equipSlot, itemType)
        messages.Add($"{Name} equips {itemType.ToDescriptor.Name}.")
        Return messages
    End Function

    Private Sub Equip(equipSlot As EquipSlotType, itemType As ItemType)
        Unequip(equipSlot)
        RemoveItems(itemType, 1)
        _data.Equipment.Add(equipSlot, itemType)
    End Sub

    Private Sub Unequip(equipSlot As EquipSlotType)
        If _data.Equipment.ContainsKey(equipSlot) Then
            AddItems(_data.Equipment(equipSlot), 1)
            _data.Equipment.Remove(equipSlot)
        End If
    End Sub
End Class
