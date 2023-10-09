using AutoMapper;
using ChatCore.DTO;
using ChatCore.Services;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;
using Moq;

namespace ChatTests
{
    public class MessageServiceTests
    {
        private readonly Mock<IMessageRepository> _messageRepositoryMock;

        public MessageServiceTests()
        {
            _messageRepositoryMock = new Mock<IMessageRepository>();
        }

        [Fact]
        public async Task AddMessageAsync_Should_Add_Message()
        {
            // Arrange
            var message = new Message { ConverstaionId = Guid.NewGuid(),MessageText = "TestMessage", SentDateTime = DateTime.UtcNow };
            _messageRepositoryMock.Setup(repo => repo.AddMessageAsync(It.IsAny<Message>())).ReturnsAsync(Guid.NewGuid());

            var mapperMock = new Mock<IMapper>();
            var messageService = new MessageService(_messageRepositoryMock.Object, mapperMock.Object);

            // Act
            await messageService.AddMessageAsync(message);

            // Assert
            _messageRepositoryMock.Verify(repo => repo.AddMessageAsync(message), Times.Once);
        }

        [Fact]
        public async Task GetConversationMessages_Should_Return_Messages()
        {
            // Arrange
            var conversationId = Guid.NewGuid();
            var messages = new List<Message> { 
                new Message { ConverstaionId = Guid.NewGuid(),MessageText = "TestMessage1", SentDateTime = DateTime.UtcNow }, 
                new Message { ConverstaionId = Guid.NewGuid(),MessageText = "TestMessage2", SentDateTime = DateTime.UtcNow }, };

            
            _messageRepositoryMock.Setup(repo => repo.GetConversationMessages(conversationId))
                .ReturnsAsync(messages);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<MessageDTO>>(messages))
                .Returns(new List<MessageDTO>());

            var messageService = new MessageService(_messageRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await messageService.GetConversationMessages(conversationId);

            // Assert
            Assert.NotNull(result);
            _messageRepositoryMock.Verify(repo => repo.GetConversationMessages(conversationId), Times.Once);
        }
    }
}
