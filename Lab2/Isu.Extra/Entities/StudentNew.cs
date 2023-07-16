using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    public class StudentNew
    {
        public StudentNew(Student student)
        {
            Student = student;
            Faculty = new Faculty(student.Group);
            ExtrasList = new List<Guid>();
        }

        public Student Student { get; }

        public Faculty Faculty { get; }

        public List<Guid> ExtrasList { get; }

        public void AddExtra(Guid groupId)
        {
            if (ExtrasList.Contains(groupId))
            {
                throw new AlreadyExistException();
            }

            ExtrasList.Add(groupId);
        }
    }
}
