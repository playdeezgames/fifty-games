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

    Public ReadOnly Property AttackStrength As Integer Implements ICharacter.AttackStrength
        Get
            Return _data.CharacterType.ToDescriptor().Attack + _data.AttackStrength
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

    Private Sub AddItems(itemType As ItemType, quantity As Integer)
        quantity += If(_data.Inventory.ContainsKey(itemType), _data.Inventory(itemType), 0)
        _data.Inventory(itemType) = quantity
    End Sub
End Class
