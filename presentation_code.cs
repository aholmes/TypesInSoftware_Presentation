using System;
namespace foo
{
	public class Program
	{
		public static void SayHi(int value) => Console.WriteLine($"Hi {value}");
		public static void SayHi(string name) => Console.WriteLine($"Hi {name}");

		public static void HowManyLettersInMyName(string name)
		{
			var character_count = name.Length;
			Console.WriteLine("There are " + character_count + " letters in \"" + name + "\"");
		}
			
		public static void Main()
		{
			{
				// C#
				var myInteger = 0;
				var myName = "Aaron";
				SayHi(myInteger);
				SayHi(myName);
			}

			{
				// C# - type error
				//var myName = "Aaron";
				var myName = 0;
				//var myName = undefined;
				HowManyLettersInMyName(myName);
			}
		}
	}
}