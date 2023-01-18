using System.Collections.Generic;
using System.ComponentModel;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerStatsSystem;
using Player = Exiled.Events.Handlers.Player;

namespace ExiledRider.Items
{
    public class LethalInjection : CustomItem
    {
        public override uint Id { get; set; } = 3;
        public override string Name { get; set; } = "LJ-119";
        public override string Description { get; set; } = "This is a Lethal Injection that, when used, will cause SCP-096 to immediately leave his enrage, regardless of how many targets he currently has, if you are one of his current targets. You always die when using this, even if there's no enrage to break, or you are not a target.";
        public override float Weight { get; set; } = 1f;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint
                {
                    Chance = 100,
                    Location = SpawnLocationType.InsideHczArmory,
                },

            },
        };
        
        [Description("Whether the Lethal Injection should always kill the user, regardless of if they stop SCP-096's enrage.")]
        public bool KillOnFail { get; set; } = true;
        
        protected override void SubscribeEvents()
        {
            Player.UsingItem += OnUsingItem;

            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsingItem;

            base.UnsubscribeEvents();
        }

        private IEnumerator<float> SlowKill(UsedItemEventArgs ev)
        {
            while (ev.Player.Health > 0)
            {
                ev.Player.Health -= 1;
                yield return Timing.WaitForSeconds(0.1f);
                
            }
        }

        private void OnUsingItem(UsedItemEventArgs ev)
        {
            if(!Check(ev.Player.CurrentItem))
                return;
            Timing.CallDelayed(2, () => { Timing.RunCoroutine(SlowKill(ev)); });
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }


    }
}