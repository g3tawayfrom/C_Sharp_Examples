using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    public class GroupExtra
    {
        public GroupExtra(SubjectName subjectName, int flowNumber, int groupNumber, int limit, Teacher teacher, int auditorium)
        {
            if (!Validation(auditorium))
            {
                throw new IncorrectInfoException();
            }

            SubjectName = subjectName;
            FlowNumber = flowNumber;
            GroupNumber = groupNumber;
            GroupId = Guid.NewGuid();
            StudentsList = new List<StudentNew>();
            Teacher = teacher;
            Limit = limit;
            Auditorium = auditorium;
        }

        public SubjectName SubjectName { get; }

        public int FlowNumber { get; }

        public int GroupNumber { get; }

        public Guid GroupId { get; }

        public List<StudentNew> StudentsList { get; }

        public Teacher Teacher { get; }

        public int Limit { get; }

        public int Auditorium { get; }

        public int GetAmount()
        {
            return StudentsList.Count;
        }

        public void AddStudent(StudentNew student)
        {
            if (GetAmount() >= Limit)
            {
                throw new IsFullException();
            }

            StudentsList.Add(student);
        }

        public void RemoveStudent(int studentId)
        {
            int studentIndex = StudentsList.Select((s, i) => new { student = s, index = i }).Where(n => n.student.Student.ID == studentId).Select(n => n.index).SingleOrDefault();

            if (studentIndex == -1)
            {
                throw new NotExistException();
            }

            StudentsList.RemoveAt(studentIndex);
        }

        public bool Validation(int auditorium)
        {
            if (auditorium < 0)
            {
                return false;
            }

            return true;
        }
    }
}
