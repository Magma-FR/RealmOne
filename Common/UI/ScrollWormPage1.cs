using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace RealmOne.Common.UI
{
    internal class ScrollWormPage1 : UIState
    {


        private UIElement area;
        private UIImage barFrame;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 1200, 1f);

            area.Top.Set(250, 0f);
            area.Width.Set(30, 0f);
            area.Height.Set(30, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("RealmOne/Assets/Textures/SquirmoLore1"));
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(0, 0f);
            barFrame.ImageScale = 1.4f;
            area.Append(barFrame);
            Append(area);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;
        }
    }

    internal class UISystemWorm1 : ModSystem
    {
        public int UIContext = 3;

        public UserInterface ScrollInterface;

        internal ScrollWormPage1 Scroll;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                Scroll = new();
                ScrollInterface = new();
                ScrollInterface.SetState(Scroll);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            ScrollInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (resourceBarIndex != -1)
            {
                var player = Main.LocalPlayer;

                if (player.GetModPlayer<Scroll>().ScrollContext == UIContext)
                {
                    layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                        "Realm Of R'lyeh: ScrollWormPage1",
                        delegate
                        {
                            ScrollInterface.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
    }
}