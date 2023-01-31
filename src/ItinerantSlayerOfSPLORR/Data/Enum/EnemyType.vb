Imports System.Runtime.CompilerServices

Public Enum EnemyType
    Blob
End Enum
Friend Module EnemyTypeExtensions
    Private table As IReadOnlyDictionary(Of EnemyType, EnemyTypeDescriptor) =
        New Dictionary(Of EnemyType, EnemyTypeDescriptor) From
        {
            {
                EnemyType.Blob,
                New EnemyTypeDescriptor With
                {
                    .HitPoints = 2,
                    .Attack = 3,
                    .Defend = 5,
                    .Agility = 5,
                    .Morale = 5,
                    .Name = "Blob",
                    .XP = 1,
                    .MinimumJools = 1,
                    .MaximumJools = 3,
                    .LootTable =
                    {
                        (1, Array.Empty(Of ItemType)),
                        (1, {ItemType.BlobGizzard})
                    }
                }
            }
        }
    <Extension>
    Friend Function ToDescriptor(enemyType As EnemyType) As EnemyTypeDescriptor
        Return table(enemyType)
    End Function
End Module
