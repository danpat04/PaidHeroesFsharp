namespace PaidHeroes

open System

type Stats =
    { strength: int
      agility: int
      intelligence: int }
    static member randomGenerate(seed) =
        let rnd = System.Random(seed)
        let gen() = rnd.Next() % 5 + 1
        {strength=gen(); agility=gen(); intelligence=gen()}

type EquipSlot =
| Head = 1
| Body = 2
| MainHand = 3
| SubHand = 4

type Equipment =
    { uuid: Guid
      id: string 
      level: int }

type Hero(guid, name, level, stats, abilities)=
    let guid: Guid = guid
    let name: string = name
    let mutable level: int = level
    let stats: Stats = stats
    let mutable abilities: Map<string, int> = abilities
    let mutable equipments: Map<EquipSlot, Equipment> = Map.empty
    new(name, abilities) = 
        let guid = Guid.NewGuid()
        Hero(guid, name, 1, Stats.randomGenerate(guid.GetHashCode()), abilities)
    member Hero.Equip(slot, equipment) =
        if equipments.ContainsKey slot then
            false
        else
            equipments.Add (slot, equipment) |> ignore
            true
    member Hero.Unequip(slot) =
        if equipments.ContainsKey slot then
            equipments.Remove(slot) |> ignore
            true
        else
            false
