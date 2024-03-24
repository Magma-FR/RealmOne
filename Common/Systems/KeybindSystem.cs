using Terraria.ModLoader;

namespace RealmOne.Common.Systems
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind RandomBuffKeybind { get; private set; }

        public static ModKeybind BrassActivation { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            BrassActivation = KeybindLoader.RegisterKeybind(Mod, "Brass Set Bonus", "X");
            RandomBuffKeybind = KeybindLoader.RegisterKeybind(Mod, "Technicians Call", "T");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            RandomBuffKeybind = null;
            BrassActivation = null;
        }
    }
}