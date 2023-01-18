using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using MEC;

namespace ExiledRider.Events
{
    public class ShyHandler
    {
        private Config config;
        public ShyHandler(Config config)
        {
            this.config = config;
        }

        public void AddTarget(AddingTargetEventArgs ev)
        {
            if (ev.IsAllowed)
            {
                ev.Target.ShowHint("You are a target for 096!", 5);
            }
        }

        public void UsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.Adrenaline)
            {
                ev.Player.Heal(15, true);
                Timing.RunCoroutine(AdrenelinDrain(ev));
            }
        }

        public IEnumerator<float> AdrenelinDrain(UsedItemEventArgs ev)
        {
            yield return Timing.WaitForSeconds(10f);
            //deal 20 damage over 10 seconds
            for (int i = 0; i < 10; i++)
            {
                ev.Player.Health -= 2;
                yield return Timing.WaitForSeconds(1);
            }
            
            
        }
    }
}