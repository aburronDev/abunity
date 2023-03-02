/*
**			Created by Christian Viñolo on 28/02/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEditor;
using UnityEditor.Compilation;
using System;
using System.IO;

namespace abunity.Editor
{
	public class ScriptPostProcessor : AssetPostprocessor
	{
		/*#####################################################################
		#############################  CONSTANTS  #############################
		#####################################################################*/
		#region Constants

		private const string NEW_KEYWORD = "#NEW#";
		private const string USER_KEYWORD = "#USER#";
		private const string DATE_KEYWORD = "#DATE#";
		private const string NAMESPACE_KEYWORD = "#NAMESPACE#";

		#endregion

		/*#####################################################################
		##############################  FIELDS  ###############################
		#####################################################################*/
		#region Fields

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		#endregion

		/*#####################################################################
		###############################  UNITY  ###############################
		#####################################################################*/
		#region Unity Methods

		private void OnPreprocessAsset()
		{
			if (assetPath.EndsWith(".cs") && !assetPath.Contains(nameof(ScriptPostProcessor)))
			{
				string file = string.Empty;

				using (StreamReader reader = new(assetPath))
				{
					file = reader.ReadToEnd();
				}

				if (!file.Contains(NEW_KEYWORD))
				{
					return;
				}

				using StreamWriter writer = new(assetPath, false);

				file = file.Replace(NEW_KEYWORD, string.Empty);
				file = file.Replace(USER_KEYWORD, Environment.UserName);
				file = file.Replace(DATE_KEYWORD, DateTime.Today.ToString("dd/MM/yyyy"));
				file = file.Replace(NAMESPACE_KEYWORD, CompilationPipeline
					.GetAssemblyRootNamespaceFromScriptPath(assetPath));

				writer.Write(file);
			}
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		#endregion
	}
}