using System;
using System.Threading;

namespace ASD
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//CreativeDraw ();

			SSSGame ();
		}

		static void CreativeDraw(){
			int count = 30;
			//int idle = 0;
			//int tile = 5;

			flag:

			for (int j = 0; j < count; j++) {
				string a = "";
				for (int i = 0; i < j; i++) {
					//if (i % tile == 0)
					//a+="\\__(-__-)__/";
					a += " ";
				}
				/*for (int i = 0; i < count-j; i++) {
					//if (i % tile == 0)
					//a+="\\__(-__-)__/";
					a += " ";
				}*/
				a += "\\";
				Console.WriteLine (a);
				//Thread.Sleep (idle);
				Console.Beep (20000, 10);
			}

			//Console.Clear ();



			for (int j = count; j > -1; j--) {
				string a = "";
				for (int i = 0; i < j; i++) {
					//if (i % tile == 0)
					//a+="\\__(-__-)__/";
					a += " ";
				}
				/*for (int i = 0; i < count-j; i++) {
					//if (i % tile == 0)
					//a+="\\__(-__-)__/";
					a += "#";
				}*/
				a += "/";
				Console.WriteLine (a);
				Console.Beep (20000, 10);
				//Thread.Sleep (idle);
			}

			//Console.Clear ();



			goto flag; 

			//Console.WriteLine ("Hello World!");
		}
	
		/*static void SSSGame(){

			int counter = 0;
			float speed = 20;

			flag:

			Console.Clear();

			WorkLine(ref counter);

			Thread.Sleep (500);

			goto flag;
		}

		static void WorkLine(ref int counter){
			Draw (counter, 2);

			counter++;
			if (counter > 9)
				counter = 0;
		}

		static void Draw(int result, int additionalSymbolsCount){
			for (int i = 0; i < (additionalSymbolsCount<<1)+1; i++) {
				var posValue = i - additionalSymbolsCount;
				var drawValue = result + posValue;
				drawValue %= 10;
				if (drawValue < 0)
					drawValue += 10;

				if (posValue == 0) {
					Console.Write (" ");
					Console.ForegroundColor = ConsoleColor.Red;

					Console.Write (drawValue);

					Console.ResetColor ();
					Console.Write (" ");
				} else {
					Console.Write (drawValue);
				}
			}
		}*/

		static void SSSGame(){

			flag:

			Console.Clear ();

			int visLen = 5;

			int checkPos = (int)(visLen / 2) + (visLen & 1);

			float startSpeed = 20;

			int first,second,third;


			bool stop = false;
			float speed = startSpeed;
			int counter = 0;

			int num = 0;

			//while (true) {
				while (Ans (false, ref counter, visLen, checkPos, ref speed, ref stop, out  first)) {
					//num++;
				}

				stop = false;
				speed = startSpeed;
				counter = 0;

				while (Ans (false, ref counter, visLen, checkPos, ref speed, ref stop, out second)) {
				}

				stop = false;
				speed = startSpeed;
				counter = 0;

				while (Ans (false, ref counter, visLen, checkPos, ref speed, ref stop, out  third)) {
				}
			//}

			if (first == second && second == third && third == 7) {
				Console.WriteLine ("You win! 777");
			} else {
				Console.WriteLine ("You lose! " + first + second+third);
			}
			Console.ReadLine ();

			goto flag;
		}

		static bool Ans(bool DrawOnly,ref int counter, int visLen, int checkPos,ref float speed,ref bool stop, out int ansver){
			//bool stop = false;
			int lastVal = -1;

			if (!DrawOnly && Console.KeyAvailable) {
				stop = true;
				Console.ReadKey ();
			}

			Console.Clear ();

			for (int i =0; i < visLen; i++){
				var curValue = counter - (int)(visLen / 2) + i;
				curValue %= 10;
				if (curValue < 0)
					curValue = 10 + curValue;

				if (i == checkPos - 1) {
					Console.Write (" ");
					Console.ForegroundColor = ConsoleColor.Red;

					lastVal = curValue;
					Console.Write (curValue);

					Console.ResetColor ();
					Console.Write (" ");
				}
				else {
					Console.Write (curValue);
				}
			}
			Console.WriteLine ();

			if (!DrawOnly) {
                var tv = (int)(500 / speed);
                if (tv < 1) tv = 1;

                Thread.Sleep (tv);
				counter++;
				if (counter > 9)
					counter = 0;

				if (stop) {
					speed -= 0.7f;
					if (speed < 0) {
						ansver = lastVal;
						return false;
					}
				}
			}

			ansver = lastVal;
			return !DrawOnly;//true;
		}
	
	}
}
