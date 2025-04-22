using Microsoft.Extensions.Options;


namespace taak7.StringProcessingService
{
    public class StringProcessingService
    {
        private readonly Task8 _config;


        public StringProcessingService(IOptions<Task8> options)
        {
            _config = options.Value;
        }


        public async Task<string> RunMain(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "HTTP ошибка 400 Bad Request. Введённая строка пуста.";
            }


            if (_config.BlackList != null && _config.BlackList.Any(word => input.Contains(word, StringComparison.OrdinalIgnoreCase)))
            {
                return "HTTP ошибка 400 Bad Request. Введённое слово находится в чёрном списке.";
            }

            string processedString = input.ToLower();


            return await Task.FromResult(processedString);
        }
    }
}
