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
            Return _data.CharacterType.ToDescriptor.HitPoints
        End Get
    End Property

    Public Sub AddXP(amount As Integer) Implements ICharacter.AddXP
        _data.XP += amount
    End Sub

    Public Sub TakeDamage(damage As Integer) Implements ICharacter.TakeDamage
        _data.Wounds += damage
    End Sub

    Public Function RollAttack(random As Random) As Integer Implements ICharacter.RollAttack
        Return random.Next(_data.CharacterType.ToDescriptor().Attack) + 1
    End Function

    Public Function Attack(enemy As IEnemy, random As Random) As IEnumerable(Of String) Implements ICharacter.Attack
        Dim messages As New List(Of String)
        Dim attackRoll = RollAttack(random)
        messages.Add($"{Name} rolls an attack of {attackRoll}.")
        Dim defendRoll = enemy.RollDefend(random)
        messages.Add($"{enemy.Name} rolls a defend of {defendRoll}")
        If attackRoll > defendRoll Then
            messages.Add($"{Name} hits!")
            enemy.TakeDamage(attackRoll - defendRoll)
            If enemy.IsDead Then
                messages.Add($"{Name} kills {enemy.Name}!")
                If enemy.XP > 0 Then
                    messages.Add($"{Name} gets {enemy.XP} XP!")
                    AddXP(enemy.XP)
                End If
                'TODO: loot drop
            End If
        Else
            messages.Add($"{Name} misses!")
        End If
        Return messages
    End Function

    Public Function RollDefend(random As Random) As Integer Implements ICharacter.RollDefend
        Return random.Next(_data.CharacterType.ToDescriptor().Defend) + 1
    End Function
End Class
