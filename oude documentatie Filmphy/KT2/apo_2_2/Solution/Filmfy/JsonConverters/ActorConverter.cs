using Filmfy.Models;
using FilmfyLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmfy.JsonConverters
{
	public class ActorConverter<T> : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			List<T> values = serializer.Deserialize<List<T>>(reader);
			List<IBaseActor> actors = new List<IBaseActor>(values as List<Actor>);
			return actors;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
