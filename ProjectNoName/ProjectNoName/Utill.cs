namespace ProjectNoName
{
    public class Utill
    {
        public static int EndMenu()
        {
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            return int.Parse(Console.ReadLine());
        }

        public static void ShowNextPage()
        {
            Console.WriteLine();
            Console.WriteLine("아무키나 입력하면 다음으로 넘어갑니다.");
            Console.Write(">> ");
            Console.ReadLine();
        }

        public static void ShowInventoryLine()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
        }

        public static class ConsoleColors
        {
            public const string Reset = "\x1b[0m";
            public const string Black = "\x1b[30m";
            public const string Red = "\x1b[31m";
            public const string Green = "\x1b[32m";
            public const string Yellow = "\x1b[33m";
            public const string Blue = "\x1b[34m";
            public const string Purple = "\x1b[35m";
            public const string Cyan = "\x1b[36m";
            public const string White = "\x1b[37m";
        }
    }
}
