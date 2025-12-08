using System;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{

    Mock<SeatsService> serviceMock;
    Mock<SeatsController> controllerMock;

    public SeatsControllerTests()
    {
        serviceMock = new Mock<SeatsService>();
        controllerMock = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        controllerMock.Setup(c => c.UserId).Returns("11111");
    }



    [TestMethod]
    public void ReserveSeatOK()
    {
    

        // Mock du service pour renvoyer une Seat
        serviceMock.Setup(s => s.ReserveSeat("1", 1))
            .Returns(new Seat { Id = 1, Number = 47, ExamenUserId = "1" });

        // Mock du controller avec CallBase = true
        var controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        // Mock de la propriété UserId
        controller.Setup(c => c.UserId).Returns("1");

        // Act
        var actionResult = controller.Object.ReserveSeat(1);

        // Assert
        var result = actionResult.Result as OkObjectResult; // Vérification que le résultat est Ok
        Assert.IsNotNull(result); // Vérifie que le résultat n'est pas null

        // Vérifie que la Seat retournée dans le OkObjectResult est celle attendue
        var seat = result!.Value as Seat;
        Assert.IsNotNull(seat); // Vérifie que l'objet est bien une Seat
        Assert.AreEqual(47, seat!.Number); // Vérifie que le numéro du siège est correct
        Assert.AreEqual("1", seat.ExamenUserId); // Vérifie que l'ExamenUserId est correct
    }


    [TestMethod]
    public void ReserveSeat_SeatAlreadyTaken()
    {
        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatAlreadyTakenException());

        var actionresult = controllerMock.Object.ReserveSeat(1);

        var result = actionresult.Result as UnauthorizedResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void SeatOutOfBounds()
    {
        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatOutOfBoundsException());

        var actionresult = controllerMock.Object.ReserveSeat(5);

        var result = actionresult?.Result as NotFoundObjectResult;
        Assert.AreEqual(result.Value, "Could not find 5");


    }

    [TestMethod]
    public void UserAlreadySeated()
    {
        serviceMock.Setup(s => s.ReserveSeat("11111", 1)).Throws(new UserAlreadySeatedException());
       // serviceMock.Setup(s => s.ReserveSeat("11111", 1)).Throws(new UserAlreadySeatedException());
        var actionresult = controllerMock.Object.ReserveSeat(1);
        var actionresult2 = controllerMock.Object.ReserveSeat(1);

        var result = actionresult2?.Result as BadRequestResult;
        Assert.IsNotNull(result);
    }
}
