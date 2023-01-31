# Itinerant Slayer of SPLORR!!

## TODOs

* trophy items dropped by slain creatures? 
* trigger conditions
    * all the time
    * if a flag is set
    * if a flag is clear
* move to next trigger when the first trigger has been completed
* sell item to shoppe optionally restocks

## Combat

* Equipment
    * unequip?
    * equip slots
        * shield
        * boots
        * helmet
* Do an agility v. agility check before fleeing
* Do morale check for enemy at the outset of the combat?
* Spells

## Core Functionality TODO

* Main Quest: acquire the macguffin and defeat the big bad
    * Quest Giver
    * Big Bad
    * Macguffin
    * Initially inaccessible resting place where the macguffin is
    * Quest Giver gives access to where macguffin is
    * Having macguffin gives access to where big bad is
    * After defeating big bad, return to quest giver for end game

## Feature Creep

* equipment breakage/durability?

## Marten's Shoppe

Flag: BlobGizzardFetchQuest

1. When BG Count >= 5, set temp flag
1. When temp flag set and BGFG clear, BG Count -= 5
1. When temp flag set and BGFG clear, set BGFG
1. Clear temp flag
1. When BGFG clear, show message
1. When BGDF clear, stop
1. When BGFG set, do shoppe

No Flags Set: "Closed due to shortage of blob gizzards!"

When Blob Gizzard Count >= 5: "Now that the blob gizzard shortage is over, we can reopen!"