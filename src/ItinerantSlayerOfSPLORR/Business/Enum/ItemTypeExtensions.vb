Imports System.Runtime.CompilerServices

Friend Module ItemTypeExtensions
    Private ReadOnly descriptors As IReadOnlyDictionary(Of ItemType, ItemTypeDescriptor) =
        New Dictionary(Of ItemType, ItemTypeDescriptor) From
        {
            {
                ItemType.Potion,
                New ItemTypeDescriptor With
                {
                    .Name = "Potion",
                    .CanUse = True,
                    .UseBy = AddressOf UsePotion
                }
            },
            {
                ItemType.EmptyBottle,
                New ItemTypeDescriptor With
                {
                    .Name = "Empty Bottle"
                }
            },
            {
                ItemType.Sword,
                New ItemTypeDescriptor With
                {
                    .Name = "Sword"
                }
            }
        }
    <Extension>
    Friend Function ToDescriptor(itemType As ItemType) As ItemTypeDescriptor
        Return descriptors(itemType)
    End Function
    Private Function UsePotion(character As ICharacter) As IEnumerable(Of String)
        Dim messages As New List(Of String)
        Const HealingAmount = 5
        Dim hpBefore = character.HitPoints
        character.Heal(HealingAmount)
        Dim actualHealing = character.HitPoints - hpBefore
        messages.Add($"{character.Name} drinks the potion.")
        messages.Add($"{character.Name} gains {actualHealing} HP.")
        character.AddItems(ItemType.EmptyBottle, 1)
        Return messages
    End Function
End Module
