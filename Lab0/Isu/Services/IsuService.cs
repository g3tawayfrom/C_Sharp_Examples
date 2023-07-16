using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services
{
    public class IsuService
    {
        private static List<Group> groupList = new List<Group>();
        private static List<Student> studentList = new List<Student>();

        public Student AddStudent(Group group, string firstName, string lastName)
        {
            if (group.Counter > 1)
            {
                throw new OvercrowdedGroupException();
            }
            else
            {
                var student = new Student(firstName, lastName, group.GroupName);
                studentList.Add(student);
                student.Group = group.GroupName;
                group.Counter++;

                return student;
            }
        }

        public Group AddGroup(GroupName groupName)
        {
            Group? group = groupList.Where(g => g.GroupName.Name.Equals(groupName.Name)).SingleOrDefault();
            if (group != null)
            {
                throw new TakenGroupNameException();
            }
            else
            {
                group = new Group(groupName);
                groupList.Add(group);
                return group;
            }
        }

        public Student GetStudent(int id)
        {
            Student? student = studentList.Where(s => s.ID.Equals(id)).SingleOrDefault();

            if (student == null)
            {
                throw new StudentNotFoundException();
            }
            else
            {
                return student;
            }
        }

        public Student FindStudent(int id)
        {
            Student? student = studentList.Where(s => s.ID.Equals(id)).SingleOrDefault();

            if (student == null)
            {
                throw new StudentNotFoundException();
            }
            else
            {
                return student;
            }
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            var studentList_temp = studentList.Where(s => s.Group.Equals(groupName)).ToList();

            if (studentList_temp == null)
            {
                throw new EmptyGroupException();
            }
            else
            {
                return studentList_temp;
            }
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var groupList_temp = groupList.Where(g => g.CourseNumber.Equals(courseNumber)).ToList();

            if (groupList_temp == null)
            {
                throw new NoGroupsAtCourseException();
            }
            else
            {
                var studentList_temp = new List<Student>();
                foreach (Group group in groupList_temp)
                {
                    var temp = studentList.Where(s => s.Group.Equals(group.GroupName)).ToList();
                    studentList_temp.AddRange(temp);
                }

                if (studentList_temp == null)
                {
                    throw new NoStudentsAtCourseException();
                }
                else
                {
                    return studentList_temp;
                }
            }
        }

        public Group FindGroup(GroupName groupName)
        {
            Group? group = groupList.Where(g => g.GroupName.Equals(groupName)).SingleOrDefault();

            if (group == null)
            {
                throw new GroupNotFoundException();
            }
            else
            {
                return group;
            }
        }

        public List<Group> ListGroups(CourseNumber courseNumber)
        {
            var groupList_temp = groupList.Where(g => g.CourseNumber.Equals(courseNumber)).ToList();

            if (groupList_temp == null)
            {
                throw new NoGroupsAtCourseException();
            }
            else
            {
                return groupList_temp;
            }
        }

        public void ChangeStudentGroup(Student student, GroupName newGroupName)
        {
            Group group = FindGroup(student.Group);
            Group group1 = FindGroup(newGroupName);

            if (group1.Counter > 1)
            {
                throw new OvercrowdedGroupException();
            }
            else
            {
                group.Counter--;
                student.Group = newGroupName;
                group1.Counter++;
            }
        }
    }
}
