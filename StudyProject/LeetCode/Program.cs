using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //var str = "tndsewnllhrtwsvxenkscbivijfqnysamckzoyfnapuotmdexzkkrpmppttficzerdndssuveompqkemtbwbodrhwsfpbmkafpwyedpcowruntvymxtyyejqtajkcjakghtdwmuygecjncxzcxezgecrxonnszmqmecgvqqkdagvaaucewelchsmebikscciegzoiamovdojrmmwgbxeygibxxltemfgpogjkhobmhwquizuwvhfaiavsxhiknysdghcawcrphaykyashchyomklvghkyabxatmrkmrfsppfhgrwywtlxebgzmevefcqquvhvgounldxkdzndwybxhtycmlybhaaqvodntsvfhwcuhvuccwcsxelafyzushjhfyklvghpfvknprfouevsxmcuhiiiewcluehpmzrjzffnrptwbuhnyahrbzqvirvmffbxvrmynfcnupnukayjghpusewdwrbkhvjnveuiionefmnfxao";


            //var a = ExerciseEveryDay.ReorganizeString(str);

            int[] b = new int[7] { 2, 3, 4, 3, 7, 1, 2 };
            var a = ExerciseEveryDay.PickMax(b, 3);
            Console.WriteLine(a);
            Console.WriteLine("program execute finish！");
            Console.Read();
        }
    }
}
