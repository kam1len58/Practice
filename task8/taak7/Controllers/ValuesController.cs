using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using taak7.StringProcessingService;

public class StringProcessingService
{
    private readonly Task8 _config;

    public StringProcessingService(IOptions<Task8> options)
    {
        _config = options.Value;
    }
    public async Task<string> RunMain(string input)
    {
        
        if (string.IsNullOrEmpty(input))
        {
            return "HTTP ошибка 400 Bad Request";
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        string s = input.ToLower();

        bool f = false;
        foreach (char c in s)
        {
            if (!alphabet.Contains(c))
            {
                f = true;
                break;
            }
        }

        if (f)
        {
            var List = new List<char>();

            foreach (char c in s)
            {

                if (!alphabet.Contains(c) && !List.Contains(c))
                {
                    List.Add(c);
                }
            }

            string invalidChars = string.Join(" ", List);

            return $"HTTP ошибка 400 Bad Request. Недопустимые символы: {invalidChars}";
        }

        if (_config.BlackList != null && _config.BlackList.Any(word =>
                input.Contains(word, StringComparison.OrdinalIgnoreCase)))
        {
            return ("HTTP ошибка 400 Bad Request. Введённое слово находится в чёрном списке.");
        }
        string g = "";
        foreach (char c in s)
        {
            if (alphabet.Contains(c))
            {
                g += c;
            }
        }

        string sum;
        if (g.Length % 2 == 0)
        {
            string n = new string(g.Substring(0, g.Length / 2).Reverse().ToArray());
            string m = new string(g.Substring(g.Length / 2).Reverse().ToArray());
            sum = n + m;
        }
        else
        {
            sum = new string(g.Reverse().ToArray()) + g;
        }

        string result = $"Обработанная строка: {sum}\n";

        result += "\nКол-во символов, которые встречаются в строке:\n";
        for (char c = 'a'; c <= 'z'; c++)
        {
            int k = sum.Count(ch => ch == c);
            if (k > 0)
            {
                result += $"{c} встречается {k} раз(a)\n";
            }
        }

        string ss = "aeiouy";
        int h = -1;
        int z = -1;
        for (int i = 0; i < sum.Length; i++)
        {
            if (ss.Contains(sum[i]))
            {
                if (h == -1) h = i;
                z = i;
            }
        }

        if (h != -1 && z != -1)
        {
            string y = sum.Substring(h, z - h + 1);
            result += $"\nСамая длинная подстрока начинающаяся и заканчивающаяся на гласную: {y}\n";
        }
        else
        {
            result += "\nВ строке только согласные буквы\n";
        }



        char[] Sort = sum.ToCharArray();
        QuickSort(Sort, 0, Sort.Length - 1);
        string quickSorted = new string(Sort);
        result += $"\nбыстрая сортировка: {quickSorted}\n";

        string treeSorted = TreeSort(sum);
        result += $"\nсортировка деревом: {treeSorted}\n";


        using var client = new HttpClient();
        string url = "https://www.randomnumberapi.com/api/v1.0/random?min=1&max=100&count=1";
        int number = await GetRandomNumber(client, url, sum);




        string Quick = quickSorted;
        if (quickSorted.Length > 0 && number < quickSorted.Length)
        {
            Quick = quickSorted.Remove(number, 1);
        }


        string Tree = treeSorted;
        if (treeSorted.Length > 0 && number < treeSorted.Length)
        {
            Tree = treeSorted.Remove(number, 1);
        }
        result += $"\nНомер символа,который надо удалить {number}:\n";
        result += $"\nРезультаты после удаления:\n";
        result += $"Быстрая сортировка: {Quick}\n";
        result += $"Сортировка деревом: {Tree}\n";

        return result;
    }

    static async Task<int> GetRandomNumber(HttpClient client, string url, string sum)
    {
        try
        {
            string r = await client.GetStringAsync(url);
            return int.Parse(r.Trim('[', ']'));
        }
        catch
        {
            return new Random().Next(0, Math.Max(0, sum.Length - 1));
        }
    }

    static void Swap(char[] a, int i, int j)
    {
        char t = a[i];
        a[i] = a[j];
        a[j] = t;
    }

    static int Partition(char[] a, int l, int m)
    {
        int k = l + 1;
        char pivot = a[l];
        for (int i = l + 1; i <= m; i += 1)
        {
            if (a[i] < pivot)
            {
                Swap(a, k, i);
                k++;
            }
        }
        Swap(a, l, k - 1);
        return k - 1;
    }

    static void QuickSort(char[] a, int l, int m)
    {
        if (l < m)
        {
            var p1 = Partition(a, l, m);
            QuickSort(a, l, p1 - 1);
            QuickSort(a, p1 + 1, m);
        }
    }

    class Node
    {
        public char Value;
        public Node left, right;
        public Node(char value)
        {
            Value = value;
            left = right = null;
        }
    }

    static Node Insert(Node root, char value)
    {
        if (root == null)
        {
            return new Node(value);
        }
        if (value < root.Value)
            root.left = Insert(root.left, value);
        else
            root.right = Insert(root.right, value);

        return root;
    }

    static string IOT(Node root)
    {
        if (root == null)
            return "";

        return IOT(root.left) + root.Value + IOT(root.right);
    }

    static string TreeSort(string input)
    {
        Node root = null;
        foreach (char c in input)
        {
            root = Insert(root, c);
        }
        return IOT(root);
    }

    enum SortMethod
    {
        quickSort = 1,
        treeSort,
    }
}

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringProcessingController : ControllerBase
    {
        private readonly StringProcessingService _service;

        public StringProcessingController(StringProcessingService service)
        {
            _service = service;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessString([FromBody] StringRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Input))
            {
                return BadRequest("HTTP ошибка 400 Bad Request");
            }

            string result = await _service.RunMain(request.Input);

            if (result.StartsWith("HTTP ошибка 400 Bad Request"))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

namespace taak7.StringProcessingService
{
    public class StringRequest
    {
        public string Input { get; set; }
    }
}