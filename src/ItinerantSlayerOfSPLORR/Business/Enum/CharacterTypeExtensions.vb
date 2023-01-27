Imports System.Runtime.CompilerServices

Friend Module CharacterTypeExtensions
    Private ReadOnly table As IReadOnlyDictionary(Of CharacterType, CharacterTypeDescriptor) =
        New Dictionary(Of CharacterType, CharacterTypeDescriptor) From
        {
            {
                CharacterType.Dude,
                New CharacterTypeDescriptor With
                {
                    .Name = "yer character",
                    .Attack = 0,
                    .Defend = 0,
                    .HitPoints = 5,
                    .XPGoal = Function(level)
                                  Dim result = 10
                                  While level > 1
                                      result *= 2
                                      level -= 1
                                  End While
                                  Return result
                              End Function,
                    .HitPointIncrease = 5,
                    .AttackStrengthIncrease = 1,
                    .DefendStrengthIncrease = 1
                }
            }
        }
    <Extension>
    Friend Function ToDescriptor(characterType As CharacterType) As CharacterTypeDescriptor
        Return table(characterType)
    End Function
End Module