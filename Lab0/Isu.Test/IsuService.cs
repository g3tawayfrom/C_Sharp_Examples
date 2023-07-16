using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private readonly IsuService service;
    public IsuServiceTest()
    {
        service = new IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = service.AddGroup(new GroupName("M32131"));
        Student student = service.AddStudent(group, "Novikov", "Georgii");

        Assert.Equal(student, service.GetStudent(student.ID));

        Assert.Equal(group.GroupName, student.Group);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Group group = service.AddGroup(new GroupName("M32011"));
        _ = service.AddStudent(group, "Novikov", "Georgii");
        _ = service.AddStudent(group, "Novikov", "Mikhail");

        Assert.Throws<OvercrowdedGroupException>(() => service.AddStudent(group, "Yanovich", "Elena"));
    }

    [Theory]
    [InlineData("M3114")]
    [InlineData("P32101")]
    [InlineData("M42101")]
    [InlineData("M321O1")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        Assert.Throws<GroupNameFormatException>(() => service.AddGroup(new GroupName(groupName)));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var groupNameOld = new GroupName("M32091");
        Group groupOld = service.AddGroup(groupNameOld);
        var groupNameNew = new GroupName("M32101");
        Group groupNew = service.AddGroup(groupNameNew);

        Student student = service.AddStudent(groupOld, "Novikov", "Georgii");
        GroupName studentsGroupOld = student.Group;

        service.ChangeStudentGroup(student, groupNew.GroupName);
        GroupName studentsGroupNew = student.Group;

        Assert.NotEqual(studentsGroupOld, studentsGroupNew);

        Assert.Equal(student.Group, groupNew.GroupName);
    }
}