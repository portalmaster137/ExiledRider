using System;
using System.Collections;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems;
using Exiled.CustomItems.API.Features;
using ExiledRider.Events;
using ExiledRider.Items;
using HarmonyLib;

namespace ExiledRider
{
    public class Plugin : Plugin<Config>
    {
        private ShyHandler _shyHandler;
        private Harmony harmonyInstance;

        public override PluginPriority Priority { get; } = PluginPriority.Last;

        public override void OnEnabled()
        {
            harmonyInstance = new Harmony($"com.{nameof(CustomItems)}.galaxy-{DateTime.Now.Ticks}");
            harmonyInstance.PatchAll();
            Log.Info("Patch done");
            Log.Info("Loading Item Handlers");
            CustomItem.RegisterItems();
            _shyHandler = new ShyHandler(Config);
            Register();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Unregister();
            base.OnDisabled();
        }

        private void Register()
        {
            Exiled.Events.Handlers.Scp096.AddingTarget += _shyHandler.AddTarget;
            Exiled.Events.Handlers.Player.UsedItem += _shyHandler.UsedItem;
        }
        private void Unregister()
        {
            Exiled.Events.Handlers.Scp096.AddingTarget -= _shyHandler.AddTarget;
            Exiled.Events.Handlers.Player.UsedItem -= _shyHandler.UsedItem;
        }
        
        
    }
}