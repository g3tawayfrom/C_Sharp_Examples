using Isu.Models;

namespace Isu.Extra.Entities
{
    public enum FacultyDesc
    {
        ФИТИП = 'M',
        ВТ = 'P',
    }

    public class Faculty
    {
        public Faculty(FacultyDesc facultyName)
        {
            FacultyName = facultyName;
        }

        public Faculty(GroupName groupName)
        {
            char[] array = groupName.Name.Substring(0, 1).ToCharArray();
            switch (array[0])
            {
                case 'M':
                    FacultyName = FacultyDesc.ФИТИП;
                    break;
                case 'P':
                    FacultyName = FacultyDesc.ВТ;
                    break;
            }
        }

        public FacultyDesc FacultyName { get; }

        public bool Equals(Faculty faculty)
        {
            if (faculty.FacultyName == FacultyName)
            {
                return true;
            }

            return false;
        }
    }
}
