using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 
public class DataManager
{




	/// <summary>
	/// Save the color of the path.
	/// </summary>
	/// <param name="Shape ID">Shape ID.</param>
	/// <param name="compundID">Compund ID.</param>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="color">Color value.</param>
	/// <param name="shapesManager">Shapes manager.</param>
	public static void SaveShapePathColor (int shapeID,int compundID, int from, int to, Color color,ShapesManager shapesManager)
	{
		string key = GetPathStrKey(shapeID,compundID,from,to,shapesManager);
		string value = color.r + "," + color.g + "," + color.b + "," + color.a;

		PlayerPrefs.SetString (key, value);
		PlayerPrefs.Save ();
	}


	/// <summary>
	/// Get the color of the shape path.
	/// </summary>
	/// <returns>The shape path color.</returns>
	/// <param name="Shape ID">Shape ID.</param>
	/// <param name="compundID">Compund ID.</param>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="shapesManager">Shapes manager.</param>
	public static Color GetShapePathColor (int shapeID, int compundID,int from, int to,ShapesManager shapesManager)
	{
		Color color = Color.white;
		string key = GetPathStrKey(shapeID,compundID,from,to,shapesManager);

		if (PlayerPrefs.HasKey (key)) {
			color = CommonUtil.StringRGBAToColor (PlayerPrefs.GetString (key));
		}
	
		return color;
	}


	/// <summary>
	/// Return the string key of specific path.
	/// </summary>
	/// <returns>The string key.</returns>
	/// <param name="shapeID">Shape ID.</param>
	/// <param name="compundID">Compund ID.</param>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="shapesManager">Shapes manager.</param>
	public static string GetPathStrKey(int shapeID, int compundID,int from, int to,ShapesManager shapesManager){
		return shapesManager.shapePrefix + "-Shape-" + shapeID + "-Compound-" + compundID + "-Path-" + from + "-" + to;
	}
		


	/// <summary>
	/// Reset the game.
	/// </summary>
	public static void ResetGame ()
	{
		PlayerPrefs.DeleteAll ();
	}
}