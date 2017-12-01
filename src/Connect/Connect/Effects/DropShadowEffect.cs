using System;
using Xamarin.Forms;

namespace Connect.Effects
{
	public class DropShadowEffect : RoutingEffect
    {
		public float Radius { get; set; }

		public Color Color { get; set; }

		public float DistanceX { get; set; }

		public float DistanceY { get; set; }

		public DropShadowEffect() : base("Connect.DropShadowEffect")
        {
		}
	}
}