using System.Collections.Generic;
using System.Diagnostics;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Events;
using Gemfruit.Mod.API.Events.Monsters;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.API.Utility.Registry;
using Gemfruit.Mod.Internal;
using StardewValley.Locations;
using StardewValley.Monsters;

namespace Gemfruit.Mod.Monsters
{
    public class MonsterRegistry : KeyedRegistry<MonsterType>
    {
        private readonly Dictionary<ResourceKey, MonsterType> _types =
            new Dictionary<ResourceKey, MonsterType>();

        public Optional<MonsterType> Get(ResourceKey key)
        {
            if (CurrentPhase == RegistryPhase.Frozen)
            {
                if (_types.ContainsKey(key))
                    return new Optional<MonsterType>(_types[key]);
                
                GemfruitMod.Logger.Log(LogLevel.Error, "MonsterRegistry",
                    $"Attempted to get non-existent monster '{key}'!");
            }
            else
            {
                GemfruitMod.Logger.Log(LogLevel.Error, "MonsterRegistry",
                    $"Attempted to get monster '{key}' before registration was done!");
            }

            return Optional<MonsterType>.None();
        }

        protected override void InitializeRecords()
        {
            GemfruitMod.InitBus.FireEvent(new MonsterRegistrationEvent(this, EventPhase.Before));
            Register(new ResourceKey("green_slime"), new MonsterType(new ResourceKey("green_slime"))
                .SetMineshaftConstructor(data => new GreenSlime(data.Position, data.Level)));
            Register(new ResourceKey("big_slime"), new MonsterType(new ResourceKey("big_slime"))
                .SetMineshaftConstructor(data => new BigSlime(data.Position, data.Area)));
            Register(new ResourceKey("bug"), new MonsterType(new ResourceKey("bug"))
                .SetMineshaftConstructor(data => new Bug(data.Position,
                    data.Rand.Next(4), data.Shaft)));
            Register(new ResourceKey("duggy"), new MonsterType(new ResourceKey("duggy"))
                .SetMineshaftConstructor(data => new Duggy(data.Position)));
            Register(new ResourceKey("rock_crab"), new MonsterType(new ResourceKey("rock_crab"))
                .SetMineshaftConstructor(data => new RockCrab(data.Position)));
            Register(new ResourceKey("fly"), new MonsterType(new ResourceKey("fly"))
                .SetMineshaftConstructor(data => new Fly(data.Position)));
            Register(new ResourceKey("grub"), new MonsterType(new ResourceKey("grub"))
                .SetMineshaftConstructor(data => new Grub(data.Position)));
            Register(new ResourceKey("bat"), new MonsterType(new ResourceKey("bat"))
                .SetMineshaftConstructor(data => new Bat(data.Position, data.Level)));
            Register(new ResourceKey("rock_golem"), new MonsterType(new ResourceKey("rock_golem"))
                .SetMineshaftConstructor(data => new RockGolem(data.Position, data.Shaft)));
            Register(new ResourceKey("skeleton"), new MonsterType(new ResourceKey("skeleton"))
                .SetMineshaftConstructor(data => new Skeleton(data.Position)));
            Register(new ResourceKey("dust_spirit"), new MonsterType(new ResourceKey("dust_spirit"))
                .SetMineshaftConstructor(data => new DustSpirit(data.Position, 
                    data.Rand.NextDouble() < 0.8)));
            Register(new ResourceKey("ghost"), new MonsterType(new ResourceKey("ghost"))
                .SetMineshaftConstructor(
                    data =>
                    {
                        data.Shaft.setGhostAdded(true);
                        return new Ghost(data.Position);
                    }));
            Register(new ResourceKey("metal_head"), new MonsterType(new ResourceKey("metal_head"))
                .SetMineshaftConstructor(data => new MetalHead(data.Position, data.Area)));
            Register(new ResourceKey("shadow_brute"), new MonsterType(new ResourceKey("shadow_brute"))
                .SetMineshaftConstructor(data => new ShadowBrute(data.Position)));
            Register(new ResourceKey("shadow_shaman"), new MonsterType(new ResourceKey("shadow_shaman"))
                .SetMineshaftConstructor(data => new ShadowShaman(data.Position)));
            Register(new ResourceKey("lava_crab"), new MonsterType(new ResourceKey("lava_crab"))
                .SetMineshaftConstructor(data => new RockCrab(data.Position, "Lava Crab")));
            Register(new ResourceKey("squid_kid"), new MonsterType(new ResourceKey("squid_kid"))
                .SetMineshaftConstructor(data => new SquidKid(data.Position)));
            
            Register(new ResourceKey("wild_shadow_brute"), new MonsterType(new ResourceKey("wild_shadow_brute"))
                .SetWildernessConstructor(
                    data => new ShadowBrute(data.Position)
                    {
                        focusedOnFarmers = true, wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_golem"), new MonsterType(new ResourceKey("wild_golem"))
                .SetWildernessConstructor(
                    data => new RockGolem(data.Position, data.Player.combatLevel)
                    {
                        focusedOnFarmers = true, wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_slime"), new MonsterType(new ResourceKey("wild_slime"))
                .SetWildernessConstructor(
                    data =>
                    {
                        var mineLevel = 1;
                        if (data.Player.combatLevel >= 10) mineLevel = 140;
                        else if (data.Player.combatLevel >= 8) mineLevel = 100;
                        else if (data.Player.combatLevel >= 4) mineLevel = 41;
                        return new GreenSlime(data.Position, mineLevel)
                        {
                            wildernessFarmMonster = true
                        };
                    }));
            Register(new ResourceKey("wild_galaxy_bat"), new MonsterType(new ResourceKey("wild_galaxy_bat"))
                .SetWildernessConstructor(
                    data => new Bat(data.Position, 9999)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_iridium_bat"), new MonsterType(new ResourceKey("wild_iridium_bat"))
                .SetWildernessConstructor(
                    data => new Bat(data.Position, 172)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_serpent"), new MonsterType(new ResourceKey("wild_serpent"))
                .SetWildernessConstructor(
                    data => new Serpent(data.Position)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_lava_bat"), new MonsterType(new ResourceKey("wild_lava_bat"))
                .SetWildernessConstructor(
                    data => new Bat(data.Position, 81)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_frost_bat"), new MonsterType(new ResourceKey("wild_frost_bat"))
                .SetWildernessConstructor(
                    data => new Bat(data.Position, 41)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            Register(new ResourceKey("wild_flying_bat"), new MonsterType(new ResourceKey("wild_flying_bat"))
                .SetWildernessConstructor(
                    data => new Bat(data.Position, 1)
                    {
                        focusedOnFarmers = true,
                        wildernessFarmMonster = true
                    }));
            
            Register(new ResourceKey("flying_fly"), new MonsterType(new ResourceKey("flying_fly"))
                .SetWildernessConstructor(
                    data => new Fly(data.Position)
                    {
                        focusedOnFarmers = true
                    }));
            
            // Farm Constructors
            Register(new ResourceKey("hutch_green_slime"), new MonsterType(new ResourceKey("hutch_green_slime"))
                .SetHutchConstructor(data => new GreenSlime(data.Position, 0)));
            Register(new ResourceKey("hutch_frost_jelly"), new MonsterType(new ResourceKey("hutch_frost_jelly"))
                .SetHutchConstructor(data => new GreenSlime(data.Position, 40)));
            Register(new ResourceKey("hutch_red_sludge"), new MonsterType(new ResourceKey("hutch_red_sludge"))
                .SetHutchConstructor(data => new GreenSlime(data.Position, 80)));
            Register(new ResourceKey("hutch_purple_sludge"), new MonsterType(new ResourceKey("hutch_purple_sludge"))
                .SetHutchConstructor(data => new GreenSlime(data.Position, 121)));
            
            GemfruitMod.InitBus.FireEvent(new MonsterRegistrationEvent(this, EventPhase.During));
            GemfruitMod.InitBus.FireEvent(new MonsterRegistrationEvent(this, EventPhase.After));
        }

        protected override void AddItem(ResourceKey key, MonsterType value)
        {
            _types.Add(key, value);
        }

        protected override bool HasKey(ResourceKey key)
        {
            return _types.ContainsKey(key);
        }
    }
}