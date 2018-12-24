namespace Harbour.UtilsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<User> userList = new List<User>();
            //userList.IsValuable();
            //userList.TryForEach((e, i) => { i = 1; });

            var fds = default(int[]);

            User user = null;
            user.dfs(user?.Name);


            TypeParseExtTest.TryToIntArray();
            TypeParseExtTest.TryToIntArrayTest();
            TypeParseExtTest.TryToIntTest();
            TypeParseExtTest.TryToJsonTest();
            TypeParseExtTest.TryToStringArrayTest();
        }
    }
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

}
