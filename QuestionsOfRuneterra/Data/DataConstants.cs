namespace QuestionsOfRuneterra.Data
{
    public class DataConstants
    {
        public class Answer
        {
            public const int ContentMinLength = 5;
            public const int ContentMaxLength = 20;
        }

        public class ApplicationUser
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 30;
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 30;
            public const int LastNameMinLength = 4;
            public const int LastNameMaxLength = 30;
            public const int ProfileImagePathMaxLength = 32767;
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 5;
        }

        public class Message
        {
            public const int ContentMinLength = 5;
            public const int ContentMaxLength = 300;
        }

        public class Question
        {
            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 250;
        }

        public class Room
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }
    }
}
