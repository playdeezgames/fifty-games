Friend Class EnemyTypeDescriptor
    Public Property HitPoints As Integer
    Public Property Attack As Integer
    Public Property Defend As Integer
    Public Property Agility As Integer
    Public Property Morale As Integer
    Public Property Name As String
    Public Property XP As Integer
    Public Property MinimumJools As Integer
    Public Property MaximumJools As Integer
    Public Property LootTable As IEnumerable(Of (Integer, IEnumerable(Of ItemType)))
    Friend Function RollLoot(random As Random) As IEnumerable(Of ItemType)
        Dim generated = random.Next(LootTable.Sum(Function(x) x.Item1))
        For Each entry In LootTable
            generated -= entry.Item1
            If generated < 0 Then
                Return entry.Item2
            End If
        Next
        Throw New NotImplementedException
    End Function
End Class
