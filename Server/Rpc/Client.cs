using System.Globalization;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using NFive.SDK.Server.Rpc;

namespace NFive.Server.Rpc
{
	public class Client : IClient
	{
		public int Handle { get; }

		public string Name { get; }

		public string License { get; }

		public long? SteamId { get; }

		public string EndPoint { get; }

		public int Ping
		{
			get
			{
				if (this.Handle > ushort.MaxValue) return -1;
				return API.GetPlayerPing(this.Handle.ToString());
			}
		}

		public Client(int handle)
		{
			this.Handle = handle;

			var player = new PlayerList()[this.Handle];

			this.Name = player.Name;
			this.License = player.Identifiers["license"];
			this.SteamId = player.Identifiers.Contains("steam") ? long.Parse(player.Identifiers["steam"], NumberStyles.HexNumber) : default(long?);
			this.EndPoint = player.EndPoint;
		}

		public IRpcTrigger Event(string @event) => RpcManager.Event(@event);
	}
}
