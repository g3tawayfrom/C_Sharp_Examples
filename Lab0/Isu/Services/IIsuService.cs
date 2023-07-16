using Isu.Entities;
using Isu.Models;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(GroupName groupName, CourseNumber courseNumber);
        Student AddStudent(Group group, string firstName, string lastName, int id);

        Student GetStudent(int id);
        Student FindStudent(int id);
        List<Student> FindStudents(GroupName groupName);
        List<Student> FindStudents(CourseNumber courseNumber);

        Group FindGroup(GroupName groupName);
        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}
