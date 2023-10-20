using OpenAI_API.Completions;
using OpenAI_API;
using WebSocialNet.Domain.Interfaces.IServices;

namespace WebSocialNet.Service
{
    public class OpenAIService : IOpenAIService
    {

        // Method for ask something to ChatGPT
        public async Task<string> UseChatGPT(string query)
        {
            string outputResult = "";
            var openai = new OpenAIAPI("");
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = query;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 1024;

            var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                outputResult += completion.Text;
            }
            return outputResult;
        }
    }
}
