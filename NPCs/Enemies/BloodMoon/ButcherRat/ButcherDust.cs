using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

public class ButcherDust : ModDust
{
    public override void OnSpawn(Dust dust) {
        dust.frame = new Rectangle(0, Main.rand.Next(3) * 3, 8, 8);

        dust.noLight = true;

        dust.scale *= Main.rand.NextFloat(0.8f, 1.2f);
    }

    public override bool Update(Dust dust) {
        dust.position += dust.velocity;
        
        dust.velocity.X *= 0.98f;
        dust.velocity.Y += 0.1f;

        dust.alpha += 3;

        if (dust.alpha >= 255) {
            dust.active = false;
        }
        
        return false;
    }
}
