using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;
public class IsuExtraServiceTest
{
    private readonly IsuExtraService service;

    public IsuExtraServiceTest()
    {
        service = new IsuExtraService();
    }

    [Fact]
    public void AddNewExtra_ExtraHasFlowsAndGroups()
    {
        string name = "Уроки составления нормального расписания";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ФИТИП);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Assert.Equal(subject, service.GetSubject(subjectName));

        Flow flow = service.AddFlowToExtra(30, subject);

        Assert.Contains(flow, subject.FlowList);

        var teacher = new Teacher("Ilya", "Edrets");

        GroupExtra groupExtra = service.AddGroupToFlow(teacher, 289, 15, flow);

        Assert.Contains(groupExtra, flow.GroupExtraList);
    }

    [Fact]
    public void AddStudentToExtra_StudentHasExtraAndExtraContainsStudent()
    {
        string name = "lesson 1";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ВТ);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Flow flow = service.AddFlowToExtra(30, subject);

        var teacher = new Teacher("fn1", "ls1");

        GroupExtra groupExtra = service.AddGroupToFlow(teacher, 289, 15, flow);

        var groupName = new GroupName("M32131");

        GroupNew group = service.AddGroupNew(groupName);
        StudentNew student = service.AddStudentNew(groupName, "Novikov", "Georgii");

        service.AddLessonToGroup(group, Lesson.First, Day.Friday, Week.Even);
        service.AddLessonToGroup(group, Lesson.Second, Day.Friday, Week.Even);

        service.AddLessonToFlow(flow, Lesson.Third, Day.Friday, Week.Even);
        service.AddLessonToFlow(flow, Lesson.Fourth, Day.Friday, Week.Even);

        service.SubStudentToExtra(student, subject);

        Assert.Contains(groupExtra.GroupId, student.ExtrasList);

        Assert.Contains(student, groupExtra.StudentsList);
    }

    [Fact]
    public void StudentReachedMaxOfExtras_ThrowException()
    {
        string name = "lesson 2";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ВТ);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Flow flow = service.AddFlowToExtra(30, subject);

        var teacher = new Teacher("fn2", "ls2");

        GroupExtra groupExtra = service.AddGroupToFlow(teacher, 289, 15, flow);

        string name1 = "lesson 3";
        var subjectName1 = new SubjectName(name1);

        Subject subject1 = service.AddExtra(subjectName1, faculty, 60);

        Flow flow1 = service.AddFlowToExtra(30, subject1);

        var teacher1 = new Teacher("fn3", "ls3");

        GroupExtra groupExtra1 = service.AddGroupToFlow(teacher1, 289, 15, flow1);

        string name2 = "lesson 4";
        var subjectName2 = new SubjectName(name2);

        Subject subject2 = service.AddExtra(subjectName2, faculty, 60);

        Flow flow2 = service.AddFlowToExtra(30, subject2);

        var teacher2 = new Teacher("fn4", "ls4");

        GroupExtra groupExtra2 = service.AddGroupToFlow(teacher2, 289, 15, flow2);

        var groupName = new GroupName("M32141");

        _ = service.AddGroupNew(groupName);
        StudentNew student = service.AddStudentNew(groupName, "Novikov", "Mikhail");

        service.SubStudentToExtra(student, subject);
        service.SubStudentToExtra(student, subject1);

        Assert.Throws<NoMoreOptionsException>(() => service.SubStudentToExtra(student, subject2));
    }

    [Fact]
    public void RemoveStudentFromExtra()
    {
        string name = "lesson 5";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ВТ);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Flow flow = service.AddFlowToExtra(30, subject);

        var teacher = new Teacher("fn5", "ls5");

        GroupExtra groupExtra = service.AddGroupToFlow(teacher, 289, 15, flow);

        var groupName = new GroupName("M32111");

        _ = service.AddGroupNew(groupName);
        StudentNew student = service.AddStudentNew(groupName, "Novikova", "Varvara");

        service.SubStudentToExtra(student, subject);

        Assert.Contains(groupExtra.GroupId, student.ExtrasList);

        Assert.Contains(student, groupExtra.StudentsList);

        service.UnsubStudentFromExtra(student, subject);

        bool result1 = student.ExtrasList.Contains(groupExtra.GroupId);
        bool result2 = groupExtra.StudentsList.Contains(student);

        Assert.False(result1);

        Assert.False(result2);
    }

    [Fact]
    public void GetFlowsFromExtras_ThrowExceptionIfOverflowed()
    {
        string name = "lesson 6";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ВТ);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Flow flow1 = service.AddFlowToExtra(25, subject);

        Flow flow2 = service.AddFlowToExtra(25, subject);

        List<Flow> flows = service.GetAllFlows(subject);

        Assert.Contains(flow1, flows);
        Assert.Contains(flow2, flows);

        Assert.Throws<IsFullException>(() => service.AddFlowToExtra(25, subject));
    }

    [Fact]
    public void GetStudentListFromGroupExtra()
    {
        string name = "lesson 7";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ВТ);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Flow flow = service.AddFlowToExtra(30, subject);

        var teacher = new Teacher("fn7", "ls7");

        _ = service.AddGroupToFlow(teacher, 289, 15, flow);

        var groupName = new GroupName("M32091");

        _ = service.AddGroupNew(groupName);
        StudentNew student = service.AddStudentNew(groupName, "Novikov", "Georgii");

        service.SubStudentToExtra(student, subject);

        List<StudentNew> studentNews = service.FindBusyStudents(student.ExtrasList[0]);

        Assert.Contains(student, studentNews);
    }

    [Fact]
    public void GetLazyStudents()
    {
        string name = "lesson 8";
        var subjectName = new SubjectName(name);
        var faculty = new Faculty(FacultyDesc.ВТ);

        Subject subject = service.AddExtra(subjectName, faculty, 60);

        Flow flow = service.AddFlowToExtra(30, subject);

        var teacher = new Teacher("fn8", "ls8");

        _ = service.AddGroupToFlow(teacher, 289, 15, flow);

        var groupName = new GroupName("M32081");

        GroupNew group = service.AddGroupNew(groupName);
        StudentNew student1 = service.AddStudentNew(groupName, "Novikov", "Georgii");
        StudentNew student2 = service.AddStudentNew(groupName, "Novikov", "Georgii");

        service.SubStudentToExtra(student1, subject);

        List<StudentNew> lazyStudents = service.FindFreeStudent(group);

        Assert.Contains(student2, lazyStudents);

        bool result = lazyStudents.Contains(student1);

        Assert.False(result);
    }
}
