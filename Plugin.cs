using System;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.ShotEvents;
using PluginAPI.Core.Items;
using UnityEngine;
using UnityEngine.Windows;


namespace JumpTester
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "chmo";
        public override string Name => "JumpTester";
        public override string Prefix => "Jt";
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Shooting += OnShooting;

            base.OnDisabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;

            base.OnEnabled();
        }

        

        public void OnShooting(ShootingEventArgs ev)
        {
            ev.Player.Broadcast(1, "Shooting");

            Ray ray = new Ray(ev.Player.CameraTransform.position, ev.Player.CameraTransform.forward);

            bool result = Physics.Raycast(ray, out RaycastHit hit, 20, LayerMask.GetMask("Default"));

            if (!result)
                return;

            Primitive.Create(PrimitiveType.Cube, hit.point);
        }
    }
}