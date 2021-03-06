using System;
using System.Runtime.Serialization;
using Sandbox.Common.ObjectBuilders;

namespace SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock
{
	[DataContract]
	public class InteriorLightEntity : LightEntity
	{
		#region "Attributes"

		public static string InteriorLightNamespace = "";
		public static string InteriorLightClass = "Sandbox.Game.Entities.Cube.MyInteriorLight";

		#endregion "Attributes"

		#region "Constructors and Initializers"

		public InteriorLightEntity( CubeGridEntity parent, MyObjectBuilder_InteriorLight definition )
			: base( parent, definition )
		{
		}

		public InteriorLightEntity( CubeGridEntity parent, MyObjectBuilder_InteriorLight definition, Object backingObject )
			: base( parent, definition, backingObject )
		{
		}

		#endregion "Constructors and Initializers"
	}
}