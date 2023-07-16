using Isu.Exceptions;

namespace Isu.Models
{
    public class CourseNumber
    {
        public CourseNumber(GroupName groupName)
        {
            if (int.TryParse(groupName.Name.Substring(2, 1), out int number))
            {
                if (number < 1 || number > 4)
                {
                    throw new CourseNumberException();
                }
            }
            else
            {
                throw new CourseNumberException();
            }

            Course = number;
        }

        public CourseNumber(int course)
        {
            if (course < 1 || course > 4)
            {
                throw new CourseNumberException();
            }

            Course = course;
        }

        public int Course { get; }

        public override string ToString()
        {
            return Course.ToString();
        }
    }
}