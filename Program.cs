

using System.Net;
using System.Net.Http.Json;

var startTime = DateTime.Now;

var httpClient = new HttpClient();
var postContentAsArray = new List<string>() {
    "a",
    "b",
    "c",
    "d",
};

Console.WriteLine("Debut");


// IEnumerable<IEnumerable<char>> payloads = new List<List<char>>(500_000);
// var dict = File.ReadLines("dict-mots.txt");
var file = new StreamReader(new FileStream("dict-mots.txt", FileMode.Open));


// payloads =  (
//     from word in dict 
//     select word.ToList()
// );

// First step build the data array
// for (char i = 'a'; i <= 'z'; i++) {
//     for (char j = 'a'; j <= 'z'; j++) {
//         for (char k = 'a'; k <= 'z'; k++) {
//             // for (char m = 'a'; m <= 'z'; m++) {
//                 postContentAsArray = new List<string> { i.ToString(), j.ToString(), k.ToString() };
//                 payloads.Add(postContentAsArray);
//             // }
//         }        
//     }
// }

// payloads.ForEach(tab => {
//     tab.ForEach(letter => Console.Write(letter));
//     Console.WriteLine();

// });

var httpContent = JsonContent.Create<IEnumerable<string>>(postContentAsArray);

// Console.WriteLine(await httpContent.ReadAsStringAsync());

var response = await httpClient.PostAsync(args[0], httpContent);
// Console.WriteLine(response);
// Console.WriteLine(await response.Content.ReadAsStringAsync());

// var tasks = new List<Task>(1_000_000);
var verifyEnign05Async = new VerifyEnign05Async();
Task? task = null;

// var tasks = new List<Task>(100_000_000);
// foreach (var payload in dict)
while ( !file.EndOfStream)
{
    var payload = file.ReadLineAsync();
    task = verifyEnign05Async.executePostRequest(await payload, args[0]);
    // tasks.Add(task);
}
if (task is not null)
{
    // await Task.WhenAll(tasks.ToArray());
}
Thread.Sleep(10_000);

// Thread.Sleep(20 * 1000);
Console.WriteLine("Nb Seconds : " + ((DateTime.Now - startTime).TotalSeconds));

class VerifyEnign05Async {
    // public List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>(1000000000);

    public HttpClient httpClient = new HttpClient();

    public int nbRequest = 0;

    public async Task executePostRequest(IEnumerable<char> payload, string url) {
        nbRequest++;
        Console.WriteLine(nbRequest);

        var httpResponse = await httpClient.PostAsJsonAsync<IEnumerable<char>>(url, payload);
        // tasks.Add(task);
        // var payloadAsString = "";
        // for (int i = 0; i < payload.Count(); i++)
        // {
        //     payloadAsString += payload.ElementAt(i);
        //     // Console.Write(payload.ElementAt(i));
        // }
        // // Console.Wril
        // Console.WriteLine(payloadAsString);
        // Console.WriteLine(httpResponse);
        // Console.WriteLine("================================================");
        // Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());

        // var index = Task.WaitAny(tasks.ToArray());
        // var httpResponse = await tasks.ElementAt(index);
        
        if (httpResponse.StatusCode != HttpStatusCode.Conflict) {

            Console.WriteLine(await httpResponse.Content.ReadAsStringAsync());
            Console.WriteLine("Code trouvé");
            Environment.Exit(1);
            // throw new Exception("Code trouve");
        }
    }   
}