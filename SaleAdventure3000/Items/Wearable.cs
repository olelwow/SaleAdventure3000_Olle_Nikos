using SaleAdventure3000.Entities;

namespace SaleAdventure3000.Items
{
    public class Wearable : Item
    {
        // Arrays för Wearables som innehåller deras symboler, namn och typ.
        // Dictionary används för att avgöra vilken kombination har plockats
        // och assigna relevanta stats som Luck, Evasion och Armor.
        private readonly string[] symbols = [" H ", " B ", " L "];
        private readonly string[] names = ["Helmet", "Body", "Legs"];
        private readonly string[] types = ["Leather", "Plate", "Cloth"];
        private readonly Dictionary<string, int[]> partProperties = new()
        {
            {"ClothHelmet", [5, 3] },
            {"ClothBody",   [10, 8] },
            {"ClothLegs",   [8, 5] },
            {"PlateHelmet", [5, 10] },
            {"PlateBody",   [20, 10] },
            {"PlateLegs",   [12, 8] },
            {"LeatherHelmet", [5, 5] },
            {"LeatherBody", [10, 10] },
            {"LeatherLegs", [7, 7] },
        };

       // Kontrollerar om Wearable är på spelaren(eq = equiped)
        public Wearable(bool eq) 
        {
            this.Equipped = eq;
        }
        
        // Constructor som assigna symbolens färg.
        public Wearable(string name, string type, int posX, int posY)
        {
            EntitySelection(name, type);
            this.PosX = posX;
            this.PosY = posY;
            if (type == "Cloth")
            {
                this.SymbolColor = "#6699ff";
            }
            else if (type == "Leather")
            {
                this.SymbolColor = "#fc6f03";
            }
            else if (type == "Plate")
            {
                this.SymbolColor = "#d9d8d7";
            }
            this.Wear = true;
        }
        
        public override void EntitySelection(string name, string type)
        {
            string typeName = type + name;
            // Metod som väljer rätt egenskaper beroende på vilket namn man skrivit in i konstruktorn.
            // Val av typen och namn sker genom att kolla i Dictionary
            for (int i = 0; i < names.Length; i++)
            {
                if (name == names[i])
                {
                    this.Name = names[i];
                    this.Symbol = symbols[i];
                }
                if (type == types[i])
                {
                    this.ItemType = types[i];

                    for (int j = 0; j < partProperties.Count; j++)
                    {
                        if (partProperties.ContainsKey(typeName))
                        {
                            if (type == "Cloth")
                            {
                                this.Luck = partProperties[typeName].ElementAt(0);
                                this.HpBoost = partProperties[typeName].ElementAt(1);
                                break;
                            }
                            else if (type == "Leather")
                            {
                                this.Evasion = partProperties[typeName].ElementAt(0);
                                this.HpBoost = partProperties[typeName].ElementAt(1);
                                break;
                            }
                            else
                            {
                                this.Armor = partProperties[typeName].ElementAt(0);
                                this.HpBoost = partProperties[typeName].ElementAt(1);
                                break;
                            }
                        }
                    }
                }
            }
        }

        //Metoden som skriver ut vad spelaren har plockat upp och adderar Items stats till spelaren
        public override void OnPickup (Item item, Player player)
        {
            Console.WriteLine($"{player.Name} equips " +
                              $"{item.ItemType} {item.Name}, gaining {item.HpBoost} HP " +
                              $"and {item.PowerAdded} power."
                              );

            player.HP += item.HpBoost;
            player.BoostedHP += item.HpBoost;
            player.Power += item.PowerAdded;
            player.Luck += item.Luck;
            player.Evasion += item.Evasion;
            player.Armor += item.Armor;
            item.Equipped = true;
            AddToBag(item, player); // Item kommer i in inventory/bag
        }
        
    }
}
