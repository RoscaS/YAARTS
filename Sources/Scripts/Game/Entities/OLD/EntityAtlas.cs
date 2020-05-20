using System.Collections.Generic;
using static Game.Enums.EntityClass;
using static Game.Enums.EntityType;

public static class EntitiesAtlas
{

    /*------------------------------------------------------------------*\
    |*							UNITS
    \*------------------------------------------------------------------*/

    public static readonly EntityData knight = new EntityData {
            Class = Knight,
            Type = Character,
            Health = 200f,
            Acceleration = 10f,
            MaxSpeed = 5f,
            StoppingDistance = 2f,
            LigneOfSight = 16f,
            Range = 1.7f,
            Damages = 20f,
            CoolDown = 1.5f,
            // InteractionStrategy = Interaction.Melee,
            // ResourceStrategy = Resource.Health
    };

    public static readonly EntityData dummy = new EntityData {
            Class = Champion,
            Type = Character,
            Health = 200000f,
            Acceleration = 10f,
            MaxSpeed = 5f,
            StoppingDistance = 2f,
            LigneOfSight = 1f,
            Range = 1f,
            Damages = 0.1f,
            CoolDown = 2.5f,
            // InteractionStrategy = Interaction.Melee,
            // ResourceStrategy = Resource.Health
    };

    public static readonly EntityData archer = new EntityData {
            Class = Archer,
            Type = Character,
            Health = 150f,
            Acceleration = 10f,
            MaxSpeed = 6f,
            StoppingDistance = 2f,
            LigneOfSight = 18f,
            Range = 16f,
            Damages = 30f,
            CoolDown = 2f,
            // InteractionStrategy = Interaction.Arrow,
            // ResourceStrategy = Resource.Health
    };

    public static readonly EntityData wizard = new EntityData {
            Class = Wizard,
            Type = Character,
            Health = 110f,
            Acceleration = 10f,
            MaxSpeed = 4f,
            StoppingDistance = 0.5f,
            LigneOfSight = 20f,
            Range = 18f,
            Damages = 120f,
            CoolDown = 5f,
            // InteractionStrategy = Interaction.FireBall,
            // ResourceStrategy = Resource.Health
    };

    public static readonly EntityData worker = new EntityData {
            Class = Worker,
            Type = Character,
            Health = 300f,
            Acceleration = 10f,
            MaxSpeed = 5f,
            StoppingDistance = 2f,
            Range = 1.25f,
            Damages = 20f,
            CoolDown = 2f,
            LigneOfSight = 15f,
            // InteractionStrategy = Interaction.Melee,
            // ResourceStrategy = Resource.Health
    };

    /*------------------------------------------------------------------*\
    |*							COLLECTABLES
    \*------------------------------------------------------------------*/

    public static readonly EntityData gold = new EntityData {
            Class = Gold,
            Type = Collectible,
            Health = 30000f,
            // ResourceStrategy = Resource.Gold
    };

    public static readonly EntityData tree = new EntityData {
            Class = Tree,
            Type = Collectible,
            Health = 600f,
            // ResourceStrategy = Resource.Wood
    };


    /*------------------------------------------------------------------*\
    |*							STRUCTURES
    \*------------------------------------------------------------------*/

    public static readonly EntityData townCenter = new EntityData {
            Class = TownCenter,
            Type = Structure,
            Health = 1500f,
            LigneOfSight = 15f,
            // ResourceStrategy = Resource.Strucure
    };

    /*------------------------------------------------------------------*\
    |*							GROUPS
    \*------------------------------------------------------------------*/

    public static readonly List<EntityData> Units = new List<EntityData>()
            { knight, archer, wizard, dummy };
}
