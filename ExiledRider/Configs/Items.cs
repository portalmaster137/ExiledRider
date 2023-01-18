using System.Collections.Generic;
using ExiledRider.Events;
using ExiledRider.Items;


namespace ExiledRider.Configs
{
    public class Items
    {
        public List<LethalInjection> LethalInjection { get; private set; } = new List<LethalInjection>
        {
            new LethalInjection(),
        };
    }
}