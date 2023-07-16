using Isu.Extra.Exceptions;

namespace Isu.Extra.Models
{
    public class SubjectName
    {
        public SubjectName(string name)
        {
            if (!Validation(name))
            {
                throw new IncorrectInfoException();
            }

            Name = name;
        }

        public string Name { get; }

        private bool Validation(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return true;
        }
    }
}
