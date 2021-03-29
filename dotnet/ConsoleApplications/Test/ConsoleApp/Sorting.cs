using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	class Sorting
	{
		public static void TestSorting()
		{
			Dictionary<long, decimal> votes = new Dictionary<long, decimal>();
			votes[2] = 1;
			votes[3] = 2;
			List<long> candidates = votes.Keys.ToList();
			candidates.Sort((a, b) => votes[a] > votes[b] ? 1 : (votes[a] < votes[b] ? -1 : 0));

			foreach(var c in candidates)
			{
				Console.WriteLine(c);
			}

			List<int> numbers = new List<int>(new int[] { 5, 4, 7, 2, 1, 9});
			//numbers.Sort((a, b) => a > b ? 1 : (a < b ? -1 : 0));
			numbers.Sort((a, b) => {
				Console.WriteLine($"Got {a}, {b}. Returning {a - b}");
				return a - b;
			});

			Console.WriteLine("--------------");
			foreach (var c in numbers)
			{
				Console.WriteLine(c);
			}
			Console.WriteLine("--------------");
			numbers = new List<int>(new int[] { 5, 4, 7, 2, 1, 9 });

			numbers.Sort((a, b) => {
				var x = (a > b ? 1 : (a < b ? -1 : 0));
				Console.WriteLine($"Got {a}, {b}. Returning {x}");
				return x;
			});
			//numbers.Sort((a, b) => (a > b ? 1 : (a < b ? -1 : 0)));
			foreach (var c in numbers)
			{
				Console.WriteLine(c);
			}
		}
	}
}
