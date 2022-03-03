using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Day_7.Interface;
using Day_7.Models;
using Day_7.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MCVTest;

public class UnitTest1
{
    
    [Fact]
    public void TestGetAll_showViewOfPeopleList()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Index())
            .Returns(new List<PersonModel>{
                new PersonModel{
                    FirstName = "Duy",
                    LastName = "Pham",
                    Gender = 1,
                    DateOfBirth = "1999/12/20",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Ha noi",
                    IsGraduate = true
                },
                new PersonModel{
                    FirstName = "Hoa",
                    LastName = "Ho Thi",
                    Gender = 0,
                    DateOfBirth = "2001/10/10",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Thanh Hoa",
                    IsGraduate = true
                },
                new PersonModel{
                    FirstName = "Long",
                    LastName = "Do Tung",
                    Gender = 1,
                    DateOfBirth = "1997/02/20",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Phu Tho",
                    IsGraduate = true
                },
            });
        var controller = new PeopleController(mockPeople.Object);
        var expect = 3;

        // Act
        var listPeople = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(listPeople);
        var model = Assert.IsAssignableFrom<List<PersonModel>>(viewResult.ViewData["peopleList"]);
        Assert.Equal(expect, model.Count);
    }
    [Fact]
    public void TestAddPerson_showViewOfdeletePeople()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Index())
            .Returns(new List<PersonModel>{
                new PersonModel{
                    FirstName = "Duy",
                    LastName = "Pham",
                    Gender = 1,
                    DateOfBirth = "1999/12/20",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Ha noi",
                    IsGraduate = true
                },
                new PersonModel{
                    FirstName = "Hoa",
                    LastName = "Ho Thi",
                    Gender = 0,
                    DateOfBirth = "2001/10/10",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Thanh Hoa",
                    IsGraduate = true
                },
                new PersonModel{
                    FirstName = "Long",
                    LastName = "Do Tung",
                    Gender = 1,
                    DateOfBirth = "1997/02/20",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Phu Tho",
                    IsGraduate = true
                },
            });
        
        var controller = new PeopleController(mockPeople.Object);

        // Act
        var listPeople = controller.Add();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(listPeople);
    }
    [Fact]
    public void TestAddPerson_addNewPeople()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();
        
        var controller = new PeopleController(mockPeople.Object);

        var person = new PersonModel{
            FirstName = "Duy",
            LastName = "Pham",
            Gender = 1,
            DateOfBirth = "1999/12/20",
            PhoneNumber = "0946301025",
            BirthPlace = "Ha noi",
            IsGraduate = true
        };

        var expectActionName = "Index";

        // Act
        var deletePeople = controller.Add(person);

        // Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(deletePeople);
        Assert.Equal(expectActionName, viewResult.ActionName);
        mockPeople.Verify(r => r.Add(It.IsAny<PersonModel>()), Times.Once);
    }
    [Fact]
    public void TestUpdatePerson_showViewOfUpdatePeople_rightId()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Index())
            .Returns(new List<PersonModel>{
                new PersonModel{
                    Id = 1,
                    FirstName = "Duy",
                    LastName = "Pham",
                    Gender = 1,
                    DateOfBirth = "1999/12/20",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Ha noi",
                    IsGraduate = true
                }
            });
        
        var controller = new PeopleController(mockPeople.Object);

        // Act
        var listPeople = controller.Edit(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(listPeople);
    }
    [Fact]
    public void TestUpdatePerson_redirectToIndex_whenNotExistId()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Index())
            .Returns(new List<PersonModel>{
                new PersonModel{
                    Id = 1,
                    FirstName = "Duy",
                    LastName = "Pham",
                    Gender = 1,
                    DateOfBirth = "1999/12/20",
                    PhoneNumber = "0946301025",
                    BirthPlace = "Ha noi",
                    IsGraduate = true
                }
            });
        
        var controller = new PeopleController(mockPeople.Object);

        // Act
        var listPeople = controller.Edit(It.IsAny<int>());

        // Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(listPeople);
    }
    [Fact]
    public void TestUpdatePerson_editNewPeople()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();
        
        var controller = new PeopleController(mockPeople.Object);

        var person = new PersonModel{
            Id = 1,
            FirstName = "Duy",
            LastName = "Pham",
            Gender = 1,
            DateOfBirth = "1999/12/20",
            PhoneNumber = "0946301025",
            BirthPlace = "Ha noi",
            IsGraduate = true
        };

        var expectActionName = "Index";

        // Act
        var deletePeople = controller.Edit(person);

        // Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(deletePeople);
        Assert.Equal(expectActionName, viewResult.ActionName);
        mockPeople.Verify(r => r.Edit(It.IsAny<PersonModel>()), Times.Once);
    }
    [Fact]
    public void TestDeletePerson()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Delete(1))
            .Returns(() => true);
        
        var controller = new PeopleController(mockPeople.Object);
        var expectActionName = "Index";

        // Act
        var deletePeople = controller.Delete(1, "123");

        // Assert
        var viewResult = Assert.IsType<RedirectToActionResult>(deletePeople);
        Assert.Equal(expectActionName, viewResult.ActionName);
        mockPeople.Verify(r => r.Delete(1), Times.Once);
    }
    [Fact]
    public void TestDeletePerson_returnBadRequest_whenHaveNoString()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Delete(1))
            .Returns(() => false);
        
        var controller = new PeopleController(mockPeople.Object);
        // var expectActionName = "Index";

        // Act
        var deletePeople = controller.Delete(1, "");

        // Assert
        var viewResult = Assert.IsType<BadRequestResult>(deletePeople);
        // Assert.Equal(expectActionName, viewResult.ActionName);
        mockPeople.Verify(r => r.Delete(1), Times.Once);
    }
    [Fact]
    public void TestDeletePerson_returnOk_whenHaveNoString()
    {
        // Arrange
        var mockPeople = new Mock<PeopleInterface>();

        mockPeople
            .Setup(x=>x.Delete(1))
            .Returns(() => true);
        
        var controller = new PeopleController(mockPeople.Object);

        // Act
        var deletePeople = controller.Delete(1, "");

        // Assert
        var viewResult = Assert.IsType<OkResult>(deletePeople);
        mockPeople.Verify(r => r.Delete(1), Times.Once);
    }
}