using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroMovie : MonoBehaviour {

	public  GameObject IntroPanel;
	public MovieTexture movie;
	void Awake ()
	{
		MovieTexture movie = GetComponent<RawImage>().texture as MovieTexture;
		movie.Play ();
	}

}
