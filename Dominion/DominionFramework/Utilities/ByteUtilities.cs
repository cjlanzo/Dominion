using System;

namespace DominionFramework.Utilities
{
	public static class ByteUtilities
	{
		#region Extensions
		/// <summary>
		/// Returns a byte array as a string
		/// </summary>
		/// <param name="b">Byte array</param>
		/// <returns>Byte array as string</returns>
		public static string ConvertToString(this byte[] b)
		{
			string s = "";

			foreach (byte t in b)
			{
				if (t == '\0')
				{
					return s;
				}

				s += Convert.ToChar(t);
			}

			return s;
		}
		#endregion Extensions
	}
}