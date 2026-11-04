using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace RobotHelper
{
    /// <summary>
    /// Places the Restaurant Robot by hand on days when the game gave him no schedule
    /// (the day he is first added to a save). From the next morning the normal
    /// Content Patcher schedule runs and this mod does nothing.
    /// </summary>
    public class ModEntry : Mod
    {
        private const string RobotId = "BentleyPlayz.FastFoodUsefulStuff_RestaurantRobot";
        private const string BedroomId = "BentleyPlayz.FastFoodUsefulStuff_BentleysBedroom";
        private const string RestaurantId = "BentleyPlayz.FastFoodUsefulStuff_BentleysRestaurant";
        private const int CounterTime = 1810; // same time as the schedule entry

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += (s, e) => PlaceForNow();
            helper.Events.GameLoop.DayStarted += (s, e) => PlaceForNow();
            helper.Events.GameLoop.TimeChanged += this.OnTimeChanged;
        }

        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {
            if (e.OldTime < CounterTime && e.NewTime >= CounterTime)
                PlaceForNow();
        }

        private void PlaceForNow()
        {
            if (!Context.IsWorldReady || !Context.IsMainPlayer)
                return;

            NPC robot = Game1.getCharacterFromName(RobotId);
            if (robot == null)
                return;

            // The game gave him a schedule: let it do the work.
            if (robot.Schedule != null)
                return;

            bool counterTime = Game1.timeOfDay >= CounterTime;
            string location = counterTime ? RestaurantId : BedroomId;
            Vector2 tile = counterTime ? new Vector2(12, 4) : new Vector2(2, 9);

            GameLocation target = Game1.getLocationFromName(location);
            if (target == null)
                return;
            if (robot.currentLocation == target && robot.TilePoint == new Point((int)tile.X, (int)tile.Y))
                return;

            robot.Halt();
            Game1.warpCharacter(robot, target, tile);
            robot.faceDirection(2);
            this.Monitor.Log($"Placed the robot in {location} at {tile.X},{tile.Y} (no schedule today).", LogLevel.Trace);
        }
    }
}
