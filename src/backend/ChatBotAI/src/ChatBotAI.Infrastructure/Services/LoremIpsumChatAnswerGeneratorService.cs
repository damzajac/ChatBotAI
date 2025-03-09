using ChatBotAI.Application.Services;
using NLipsum.Core;
using System.Runtime.CompilerServices;

namespace ChatBotAI.Infrastructure.Services
{
    public class LoremIpsumChatAnswerGeneratorService : IGenerateChatAnswerService
    {
        private const int NumberOfCharsGeneratedPerCycle = 20;

        public async IAsyncEnumerable<string> Generate(string question, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var numberOfParagraphs = new Random().Next(1, 3);
            var answer = LipsumGenerator.Generate(numberOfParagraphs, Features.Paragraphs, null, Lipsums.LoremIpsum);

            for (int i = 0; i < answer.Length; i += NumberOfCharsGeneratedPerCycle)
            {
                //Simulate generating process
                await Task.Delay(1000);
                var length = i + NumberOfCharsGeneratedPerCycle > answer.Length ? answer.Length - i : NumberOfCharsGeneratedPerCycle;
                yield return await Task.FromResult(answer.Substring(i, length));
            }
        }
    }
}
