using stackattack.Persistence;
using stackattack.Questions;
using stackattack.Users;

namespace stackattack.Core
{
    /// <summary>
    /// General purpose cache to hold global instances. Not very fault tolerant.
    /// Ensure that the Load method is called during boot.
    /// </summary>
    public class Cache
    {
        private static bool loaded = false;

        public static IConfig Config { get; private set; }
        public static IUserStore UserStore { get; private set; }
        public static IQuestionStore QuestionStore { get; private set; }

        public static void Load(IConfig config)
        {
            if (!loaded)
            {
                FileBasedStorage storage = new FileBasedStorage(config.SQLiteDatabasePath);

                UserStore = new UserStore(storage);
                QuestionStore = new QuestionStore(storage, config.MaxQuestions);

                loaded = true;
            }
        }
    }
}