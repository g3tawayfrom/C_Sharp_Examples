using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class Teacher
    {
        public Teacher(string firstName, string lastName)
        {
            if (!Validation(firstName, lastName))
            {
                throw new IncorrectInfoException();
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private bool Validation(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }
    }
}
