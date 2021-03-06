using System;
using Gemfruit.Mod;
using Gemfruit.Mod.Internal;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace StardewValley
{
    internal class patch_Game1 : Game1
    {
        protected extern void orig_Initialize();
        protected override void Initialize()
        {
            Console.WriteLine(  "\n\n"+
                                ".88888.                       .8888b                   oo   dP   \n"+
                                "d8'   `88                     88                            88   \n" +
                                "88        .d8888b. 88d8b.d8b. 88aaa  88d888b. dP    dP dP d8888P \n"+
                                "88   YP88 88ooood8 88'`88'`88 88     88'  `88 88    88 88   88   \n"+
                                "Y8.   .88 88.  ... 88  88  88 88     88       88.  .88 88   88   \n"+
                                " `88888'  `88888P' dP  dP  dP dP     dP       `88888P' dP   dP   \n");
            Console.WriteLine($"Loading Gemfruit v{GemfruitMod.GemfruitVersion}");
            GemfruitMod.Initialize(this);
            try
            {
                GemfruitMod.LoadMods();
                GemfruitMod.LoadInitHooks();
                GemfruitMod.ResourceRegistry.Initialize();
                GemfruitMod.MonsterRegistry.Initialize();
                GemfruitMod.MineshaftSpawnRegistry.Initialize();
                GemfruitMod.WildernessSpawnRegistry.Initialize();
                GemfruitMod.PlaceableRegistry.Initialize();
                GemfruitMod.ItemRegistry.Initialize();
                GemfruitMod.PreservesRecipeRegistry.Initialize();
                GemfruitMod.KegRecipeRegistry.Initialize();
                GemfruitMod.MayonnaiseRecipeRegistry.Initialize();
                GemfruitMod.CrystalariumRecipeRegistry.Initialize();
                GemfruitMod.ArtifactDropRegistry.Initialize();
                GemfruitMod.GeodeResultRegistry.Initialize();
                GemfruitMod.LoadGameHooks();
            }
            catch (Exception e)
            {
                GemfruitMod.Logger.Log(LogLevel.Fatal, "Game1", "Encountered an exception while initializing Gemfruit");
                GemfruitMod.Logger.Log(LogLevel.Fatal, "Game1", e.Message);
                GemfruitMod.Logger.Log(LogLevel.Fatal, "Game1", e.StackTrace);
                Environment.Exit(-1);
            }

            orig_Initialize();
        }

        protected extern void orig_LoadContent();
        protected override void LoadContent()
        {
            GemfruitMod.Logger.Log(LogLevel.Info, "Game1", "Loading mod assets...");
            try
            {
                GemfruitMod.LoadAssets();
            }           
            catch (Exception e)
            {
                GemfruitMod.Logger.Log(LogLevel.Fatal, "Game1", "Encountered an exception while loading Gemfruit content");
                GemfruitMod.Logger.Log(LogLevel.Fatal, "Game1", e.Message);
                GemfruitMod.Logger.Log(LogLevel.Fatal, "Game1", e.StackTrace);
                Environment.Exit(-1);
            }

            orig_LoadContent();
        }
    }
}