Imports System.Runtime.CompilerServices

Public Enum EncounterType
    Blob
End Enum
Friend Module EncounterTypeExtensions
    Private table As IReadOnlyDictionary(Of EncounterType, EncounterTypeDescriptor) =
        New Dictionary(Of EncounterType, EncounterTypeDescriptor) From
        {
            {
                EncounterType.Blob,
                New EncounterTypeDescriptor With
                {
                    .EnemyType = EnemyType.Blob,
                    .Generator = New Dictionary(Of Integer, Integer) From {{1, 1}}
                }
            }
        }
    <Extension>
    Function ToDescriptor(encounterType As EncounterType) As EncounterTypeDescriptor
        Return table(encounterType)
    End Function
End Module
