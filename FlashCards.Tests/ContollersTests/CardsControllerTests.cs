using FlashCards.Controllers;
using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlashCards.Tests.ControllerTests.CardsControllerTests
{

    public class CardsControllerTests
    {
        [Fact]
        public void GetCardById_CardExistInDb_ReturnsCard()
        {
            // Arrange
            Card[] cards =
            {

                new ()
                {
                    Id = 1,
                    CardListId = 1,
                    FrontSide = "SomeSide",
                    BackSide = "AnotherSide"
                },
                new ()
                {
                    Id = 2,
                    CardListId = 1,
                    FrontSide = "OneSide",
                    BackSide = "OtherSide",
                }
            };
            var repoStub = new Mock<ICardRepository>();
            repoStub.Setup(rep => rep.Cards).Returns(cards.AsQueryable());
            var controller = new CardsController(repoStub.Object);

            // Act
            var result = controller.GetCardById(cards.First().Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(objectResult.Value as Card, cards.First());
        }

        [Fact]
        public void GetCardById_CardDoesntExistInDb_ReturnsNotFound()
        {
            Card[] cards =
           {

                new ()
                {
                    Id = 1,
                    CardListId = 1,
                    FrontSide = "SomeSide",
                    BackSide = "AnotherSide"
                },
                new ()
                {
                    Id = 2,
                    CardListId = 1,
                    FrontSide = "OneSide",
                    BackSide = "OtherSide",
                }
            };
            var repoStub = new Mock<ICardRepository>();
            repoStub.Setup(rep => rep.Cards).Returns(cards.AsQueryable());
            var controller = new CardsController(repoStub.Object);

            // Act
            var result = controller.GetCardById(cards.Last().Id + 1);

            // Arrange
            var objectResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateCard_CorrectData_ReturnsOkWithCard()
        {
            // Arrange
            var repoStub = new Mock<ICardRepository>();
            var idToUpdate = 1;
            var cardUpdate = new Card() { FrontSide = "Something new", BackSide = "Something new 2" };
            var fakeUpdatedCard = new Card()
            {
                Id = idToUpdate,
                FrontSide = cardUpdate.FrontSide,
                BackSide = cardUpdate.BackSide
            };
            repoStub.Setup(repo => repo.UpdateCard(It.IsAny<long>(), cardUpdate)).Returns(fakeUpdatedCard);
            var controller = new CardsController(repoStub.Object);

            // Act
            IActionResult result = controller.UpdateCard(idToUpdate, cardUpdate);

            // Arrange
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var responseCard = Assert.IsType<Card>(objectResult.Value);
            Assert.Equal(fakeUpdatedCard, responseCard);
        }

        [Fact]
        public void UpdateCard_IncorrectId_ReturnsNotFound()
        {
            // Arrange
            var repoStub = new Mock<ICardRepository>();
            var idToUpdate = 5;
            var cardUpdate = new Card()
            {
                CardListId = 1,
                FrontSide = "Something new",
                BackSide = "Something new 2"
            };
            repoStub.Setup(repo => repo.UpdateCard(It.IsAny<long>(), cardUpdate))
                .Returns(default(Card));
            var controller = new CardsController(repoStub.Object);

            // Act
            var result = controller.UpdateCard(idToUpdate, cardUpdate);

            // Arrange
            var objectResult = Assert.IsType<NotFoundResult>(result);
            repoStub.Verify(
                repo => repo.UpdateCard(It.IsAny<long>(), It.IsAny<Card>()),
                Times.Once());
        }

        [Fact]
        public void AddCard_CorrectData_ReturnsCreatedWithCard()
        {
            // Arrange
            var cardToAdd = new Card()
            {
                CardListId = 1,
                FrontSide = "Some Text",
                BackSide = "Another text"
            };
            var stubRepo = new Mock<ICardRepository>();
            stubRepo.Setup(repo => repo.InsertCard(It.IsAny<Card>())).Returns(cardToAdd);
            var controller = new CardsController(stubRepo.Object);
            string expectedActionName = nameof(controller.GetCardById);

            // Act
            var result = controller.AddCard(cardToAdd);

            // Assert
            var createdObject = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(createdObject.ActionName, expectedActionName);
        }

        [Fact]
        public void AddCard_IncorrectData_ReturnsBadRequest()
        {
            // Arrange
            var cardToAdd = new Card()
            {
                FrontSide = "Some Text",
            };
            var stubRepo = new Mock<ICardRepository>();
            stubRepo.Setup(repo => repo.InsertCard(It.IsAny<Card>())).Returns<Card>((card) =>
            {
                if (card.BackSide == String.Empty || card.FrontSide == String.Empty
                    || card.BackSide == null || card.FrontSide == null)
                {
                    throw new Exception();
                }
                return card;
            });
            var controller = new CardsController(stubRepo.Object);
            controller.ModelState.AddModelError("BackSide", "Backside required");

            // Act
            var result = controller.AddCard(cardToAdd);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void RemoveCard_CardExists_ReturnsOk()
        {
            // Arrange
            Card[] cards =
            {

                new ()
                {
                    Id = 1,
                    CardListId = 1,
                    FrontSide = "SomeSide",
                    BackSide = "AnotherSide"
                },
                new ()
                {
                    Id = 2,
                    CardListId = 1,
                    FrontSide = "OneSide",
                    BackSide = "OtherSide",
                }
            };
            var stubRepo = new Mock<ICardRepository>();
            stubRepo.Setup(repo => repo.Cards).Returns(cards.AsQueryable());
            var controller = new CardsController(stubRepo.Object);

            // Act
            var result = controller.RemoveCard(cards.First().Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RemoveCard_IncorrectId_ReturnsNotFound()
        {
            Card[] cards =
                {

                new ()
                {
                    Id = 1,
                    CardListId = 1,
                    FrontSide = "SomeSide",
                    BackSide = "AnotherSide"
                },
                new ()
                {
                    Id = 2,
                    CardListId = 1,
                    FrontSide = "OneSide",
                    BackSide = "OtherSide",
                }
            };
            var stubRepo = new Mock<ICardRepository>();
            stubRepo.Setup(repo => repo.Cards).Returns(cards.AsQueryable());
            var controller = new CardsController(stubRepo.Object);

            // Act
            var result = controller.RemoveCard(cards.Last().Id + 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
