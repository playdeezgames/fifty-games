Public Interface ICharacter
    ReadOnly Property CharacterType As CharacterType
    Function RollAttack(random As Random) As Integer
    Sub AddXP(amount As Integer)
    ReadOnly Property Name As String
    ReadOnly Property XP As Integer
    ReadOnly Property IsDead As Boolean
    ReadOnly Property HitPoints As Integer
    ReadOnly Property MaximumHitPoints As Integer
    Function Attack(enemy As IEnemy, random As Random) As IEnumerable(Of String)
    Function RollDefend(random As Random) As Integer
    Sub TakeDamage(damage As Integer)
End Interface
