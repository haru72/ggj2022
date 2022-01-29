using System.Collections;
using UnityEngine;

namespace FielldTestSpace
{
	public class FieldTest
	{
		GameMainSpace.PanelSpace.MasuController PanelController { get; }
		GameMainSpace.WallSpacce.WallManager WallManager{ get; }
		GameMainSpace.MasuGimicSpace.MasuGimicManager MasuGimicManager { get; }
		GameObject GameObject { get; }
		GameObject MarkObj { get; }

		public FieldTest( GameObject gameObject )
		{
			GameObject = gameObject;
			MarkObj = gameObject.transform.Find( "Mark" ).gameObject;
			PanelController = new GameMainSpace.PanelSpace.MasuController( Vector3.zero );

			var fieldTransform = gameObject.transform.Find( "Field" );

			WallManager = new GameMainSpace.WallSpacce.WallManager( fieldTransform , PanelController.CalcMasuByPos );
			MasuGimicManager = new GameMainSpace.MasuGimicSpace.MasuGimicManager( fieldTransform , PanelController.CalcMasuByPos );
		}

		public void Update()
		{
			if( Input.GetMouseButtonUp( 0 ) )
			{
				var touchScreenPosition = new Vector3() + Input.mousePosition;
				touchScreenPosition.x = Mathf.Clamp( touchScreenPosition.x , 0.0f , Screen.width );
				touchScreenPosition.y = Mathf.Clamp( touchScreenPosition.y , 0.0f , Screen.height );
				var touchPointToRay = Camera.main.ScreenPointToRay( touchScreenPosition );

				var hitInfo = new RaycastHit();
				if( Physics.Raycast( touchPointToRay , out hitInfo ) )
				{
					MarkObj.transform.position = new Vector3( hitInfo.point.x , 0 , hitInfo.point.z );

					var masu = PanelController.CalcMasuByPos( new Vector3( hitInfo.point.x , 0 , hitInfo.point.z ) );
					var pos = PanelController.CalcPosByMasu( masu );
					Debug.Log( "Masu x:" + masu.x + " Masu z:" + masu.y );
					Debug.Log( "Pos x:" + pos.x + " Pos z:" + pos.z );
					Debug.Log( "TouchPos x:" + MarkObj.transform.position.x + " TouchPos z:" + MarkObj.transform.position.z );

					bool isInWall = WallManager.IsInWall( masu );
					Debug.Log( "isInWall:" + isInWall );

					var masuGimicList = MasuGimicManager.GetMasuGimicList( masu );

					foreach( var masuGimic in masuGimicList )
					{
						if( masuGimic != null )
						{
							Debug.Log( "GimicType:" + masuGimic.GimicType );

							if( masuGimic.CanTouch() )
							{
								masuGimic.Action();
							}
						}
					}

				}
			}

			MasuGimicManager.Update();
		}
	}
}