Imports System.Runtime.CompilerServices

Public Enum CharacterType
    Dude
End Enum
Friend Module CharacterTypeExtensions
    Private table As IReadOnlyDictionary(Of CharacterType, CharacterTypeDescriptor) =
        New Dictionary(Of CharacterType, CharacterTypeDescriptor) From
        {
            {
                CharacterType.Dude,
                New CharacterTypeDescriptor With
                {
                    .Name = "yer character",
                    .Attack = 10
                }
            }
        }
    <Extension>
    Friend Function ToDescriptor(characterType As CharacterType) As CharacterTypeDescriptor
        Return table(characterType)
    End Function
End Module