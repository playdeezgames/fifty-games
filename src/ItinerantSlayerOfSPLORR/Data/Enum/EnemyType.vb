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
                    .HitPoints = 5,
                    .Attack = 5,
                    .Defend = 5,
                    .Agility = 5,
                    .Morale = 5,
                    .Name = "Blob"
                }
            }
        }
    <Extension>
    Friend Function ToDescriptor(enemyType As EnemyType) As EnemyTypeDescriptor
        Return table(enemyType)
    End Function
End Module
