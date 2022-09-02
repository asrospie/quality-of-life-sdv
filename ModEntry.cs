using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Network;
using System.Collections.Generic;

namespace quality_of_life_sdv
{
    public class ModEntry : Mod
    {
        private const float SPEED_BUFF = 2.5f;
        private const double STANDARD_SPEED = 5.0;

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.FlooringSpeedUp;
            helper.Events.Input.ButtonPressed += this.WaterAttached;
        }

        private void WaterAttached(object sender, ButtonPressedEventArgs e)
        {
            SButton buttonPressed = e.Button;
            Tool curTool = Game1.player.CurrentTool;
            if (buttonPressed == SButton.MouseLeft && curTool.Name.Contains("Watering Can"))
            {
                //Vector2 clickVec = Game1.player.GetToolLocation();
                Vector2 clickVec = new Vector2(Game1.getMouseX(), Game1.getMouseY());
                this.Monitor.Log($"Name: {curTool.Name}", LogLevel.Debug);

                Dictionary<Vector2, bool> adjTiles = this.AdjacentHoeTiles(clickVec);
            }
        }

        private Dictionary<Vector2, bool> AdjacentHoeTiles(Vector2 toolHit)
        {
            Dictionary<Vector2, bool> adjTiles = new Dictionary<Vector2, bool>();

            StardewValley.TerrainFeatures.TerrainFeature feature = Game1.currentLocation.terrainFeatures[toolHit];

            this.Monitor.Log($"Name: {feature.GetType().Name}", LogLevel.Debug);

            return adjTiles;
        }

        private bool MoveButtonDown()
        {
            return this.Helper.Input.IsDown(SButton.W) ||
                this.Helper.Input.IsDown(SButton.A) ||
                this.Helper.Input.IsDown(SButton.S) ||
                this.Helper.Input.IsDown(SButton.D);
        }

        private void FlooringSpeedUp(object sender, UpdateTickedEventArgs e)
        {
            bool isMoving = this.MoveButtonDown();
            if (isMoving)
            {
                Vector2 curVec = Game1.player.getTileLocation();
                StardewValley.TerrainFeatures.TerrainFeature feature;
                try
                {
                    feature = Game1.currentLocation.terrainFeatures[curVec];
                }
                catch (Exception ex)
                {
                    feature = null;
                }
                if (feature != null && feature.GetType().Name.Equals("Flooring"))
                {
                    Game1.player.temporarySpeedBuff += SPEED_BUFF;
                }
              
            }
        }
    }
}

