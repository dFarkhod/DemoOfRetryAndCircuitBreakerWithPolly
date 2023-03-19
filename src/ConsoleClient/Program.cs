using ConsoleClientWithRetry;

WeatherClient client = new();
bool continueLoop = true;
do
{
    Console.WriteLine("Ob-havoni haqidagi ma'umotni serverdan o'qimoqdaman...");
	string weather = client.GetWeather();
	Console.WriteLine($"Kutilayotgan ob-havo: {weather}");
    Console.WriteLine("Dastur o'z ishini yakunladi. Qaytadan boshlash uchun y ni bosing...");
    Console.WriteLine();
    var userAnswer = Console.ReadLine()?[0];
    continueLoop = userAnswer == 'y' ? true : false;
}
while (continueLoop);

