using System;
using UnityEngine;

namespace External_assets.StarSparrow.Readme.Readme_Files.Scripts
{
	public class ReadmeStarSparrow : ScriptableObject {
		public Texture2D icon;
		public string title;
		public Section[] sections;
		public bool loadedLayout;
	
		[Serializable]
		public class Section {
			public string heading, text, linkText, url;
		}
	}
}
