using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using RealmOne.Items.Others;
using RealmOne.Projectiles.Other;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Common.Global
{

    public class GlobalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool HealingPetal = false;
        public bool DefensivePetal = false;


    }
}