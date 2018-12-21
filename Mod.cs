﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace Bow
{
    public class Mod : StardewModdingAPI.Mod
    {
        public static Mod instance;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            instance = this;

            helper.Events.Display.MenuChanged += onMenuChanged;

            BowTool.Texture = helper.Content.Load<Texture2D>("bow.png");
        }

        /// <summary>Raised after a game menu is opened, closed, or replaced.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void onMenuChanged(object sender, MenuChangedEventArgs e)
        {
            if (!(e.NewMenu is ShopMenu menu) || menu.portraitPerson?.Name != "Marlon")
                return;

            Log.trace($"Adding bow to Marlon's shop.");

            var forSale = Helper.Reflection.GetField<List<Item>>(menu, "forSale").GetValue();
            var itemPriceAndStock = Helper.Reflection.GetField<Dictionary<Item, int[]>>(menu, "itemPriceAndStock").GetValue();

            var bow = new BowTool();
            forSale.Add(bow);
            itemPriceAndStock.Add(bow, new int[] { 2500, 1 });
        }
    }
}
