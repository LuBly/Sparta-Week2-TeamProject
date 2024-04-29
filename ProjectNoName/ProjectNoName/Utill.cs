namespace ProjectNoName
{
    internal class Utill
    {
        public static int EndMenu()
        {
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            return int.Parse(Console.ReadLine());
        }
    }
}
