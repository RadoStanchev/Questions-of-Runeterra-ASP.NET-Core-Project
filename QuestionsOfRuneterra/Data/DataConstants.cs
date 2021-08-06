namespace QuestionsOfRuneterra.Data
{
    public class DataConstants
    {
        public class Answer
        {
            public const int ContentMaxLength = 20;
        }

        public class ApplicationUser
        {
            public const int FirstNameMaxLength = 30;
            public const int LastNameMaxLength = 30;
            public const int ProfileImagePathMaxLength = 32767;
        }

        public class Message
        {
            public const int ContentMaxLength = 300;
        }

        public class Question
        {
            public const int ContentMaxLength = 250;
        }

        public class Room
        {
            public const int NameMaxLength = 30;
        }
    }
}
