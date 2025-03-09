using ChatBotAI.Application.UseCases.Commands.UpdateChatMessage;
using ChatBotAI.Domain.Entities;
using ChatBotAI.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace ChatBotAI.Application.Tests.UseCases
{
    public class UpdateChatMessageCommandHandlerTests
    {
        [Test]
        public void Handle_NotExistingChatMessage_ShouldThrowArgumentException()
        {
            //Arrange
            var request = new UpdateChatMessageCommand { ChatMessageId = Guid.NewGuid(), IsLiked = true };
            var chatMessageRepositoryMock = new Mock<IChatMessageRepository>();
            chatMessageRepositoryMock.Setup(x => x.Get(request.ChatMessageId.Value)).Returns(Task.FromResult((ChatMessage)null));
            var sut = new UpdateChatMessageCommandHandler(chatMessageRepositoryMock.Object);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await sut.Handle(request, new CancellationToken()));
        }

        [Test]
        public async Task Handle_ExistingChatMessageHasTheSameIsLikeValue_ShouldNotInvokeCommitOnRepo()
        {
            //Arrange
            var chatMessage = new ChatMessage { IsLiked = true };
            var request = new UpdateChatMessageCommand { ChatMessageId = Guid.NewGuid(), IsLiked = chatMessage.IsLiked };
            var chatMessageRepositoryMock = new Mock<IChatMessageRepository>();
            chatMessageRepositoryMock.Setup(x => x.Get(request.ChatMessageId.Value)).Returns(Task.FromResult(chatMessage));
            var sut = new UpdateChatMessageCommandHandler(chatMessageRepositoryMock.Object);

            //Act
            await sut.Handle(request, new CancellationToken());

            //Assert
            chatMessageRepositoryMock.Verify(x => x.Commit(), Times.Never());
        }

        [Test]
        public async Task Handle_ExistingChatMessageHasDifferentIsLikeValue_ShouldInvokeCommitOnRepo()
        {
            //Arrange
            var chatMessage = new ChatMessage { IsLiked = true };
            var request = new UpdateChatMessageCommand { ChatMessageId = Guid.NewGuid(), IsLiked = !chatMessage.IsLiked };
            var chatMessageRepositoryMock = new Mock<IChatMessageRepository>();
            chatMessageRepositoryMock.Setup(x => x.Get(request.ChatMessageId.Value)).Returns(Task.FromResult(chatMessage));
            var sut = new UpdateChatMessageCommandHandler(chatMessageRepositoryMock.Object);

            //Act
            await sut.Handle(request, new CancellationToken());

            //Assert
            chatMessageRepositoryMock.Verify(x => x.Commit(), Times.Once());
        }
    }
}
