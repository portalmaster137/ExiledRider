using System.IO;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.Commands.Reload;

namespace ExiledRider
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }

    }
}