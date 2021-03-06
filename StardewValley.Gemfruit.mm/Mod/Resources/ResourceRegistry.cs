using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Gemfruit.Mod.API;
using Gemfruit.Mod.API.Utility;
using Gemfruit.Mod.Internal;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace Gemfruit.Mod.Resources
{
    public class ResourceRegistry
    {
        private readonly Dictionary<ResourceKey, object> _dictionary =
            new Dictionary<ResourceKey, object>();

        public void Initialize()
        {
            GemfruitMod.Logger.Log(LogLevel.Info, GetType().Name, "Initializing ResourceRegistry");
        }
        
        public Optional<T> Get<T>(ResourceKey key)
        {
            //GemfruitMod.Logger.Log(LogLevel.DEBUG, "ResourceRegistry", $"Trying to fetch {key}");
            if (!_dictionary.ContainsKey(key))
            {
                //GemfruitMod.Logger.Log(LogLevel.DEBUG, "ResourceRegistry", "Key not found");
                return Optional<T>.None();
            }

            if(!(_dictionary[key] is T))
            {
                throw new Exception($"bad type '{typeof(T)}' for asset '{key}'");
            }

            //GemfruitMod.Logger.Log(LogLevel.DEBUG, "ResourceRegistry", "Key found!!");
            return new Optional<T>((T)_dictionary[key]);
        }

        public void LoadFromDirectory<T>(string namspac, string parent, string dir)
        {
            GemfruitMod.Logger.Log(LogLevel.Debug, "ResourceRegistry", $"Loading from {dir}");
            var tree = (parent == dir) ? "" : 
                dir.Replace(parent + Path.DirectorySeparatorChar, "")
                    .Replace(Path.DirectorySeparatorChar, '\\');
            var files = Directory.GetFiles(dir);
            foreach (var f in files)
            {
                if(File.GetAttributes(f).HasFlag(FileAttributes.Directory)) continue;
                GemfruitMod.Logger.Log(LogLevel.Debug, "ResourceRegistry", $"Trying to load {f}");
                var name = f.Replace(dir + Path.DirectorySeparatorChar, "");
                name = Regex.Replace(name, "(.+)\\..*", "$1");
                var fs = new FileStream(f, FileMode.Open);
                if (typeof(T) == typeof(Texture2D))
                {
                    Register(new ResourceKey(namspac, tree + "\\" + name),
                        Texture2D.FromStream(Game1.graphics.GraphicsDevice, fs));
                    GemfruitMod.Logger.Log(LogLevel.Debug, "ResourceRegistry", $"Loaded texture {tree}\\{name}");
                }
                fs.Close();
            }
        }

        public void Register<T>(ResourceKey key, T asset)
        {
            GemfruitMod.Logger.Log(LogLevel.Debug, GetType().Name,
                $"Registering resource '{key}");
            _dictionary[key] = asset;
        }
    }
}