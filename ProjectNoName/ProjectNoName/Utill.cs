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
            Console.Write(">>");
            Console.ReadLine();
        }
    }
}
