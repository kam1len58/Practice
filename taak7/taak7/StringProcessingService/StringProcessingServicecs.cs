namespace taak7.StringProcessingService;
public class StringProcessingService
{
    public async Task<string> RunMain(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "HTTP ошибка 400 Bad Request";
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        bool hasInvalidChars = input.Any(c => !alphabet.Contains(c));

        if (hasInvalidChars)
        {
            return "HTTP ошибка 400 Bad Request";
        }

        
        string processedString = input.ToLower();  

        return await Task.FromResult(processedString);
    }
}




