Public Interface IEnemy
    ReadOnly Property Name As String
    ReadOnly Property HitPoints As Integer
    ReadOnly Property MaximumHitPoints As Integer
    ReadOnly Property IsDead As Boolean
    Sub TakeDamage(damage As Integer)
    Function RollDefend(random As Random) As Integer
    ReadOnly Property XP As Integer
End Interface
