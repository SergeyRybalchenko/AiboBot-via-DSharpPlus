using DSharpPlus;
using Microsoft.Extensions.Configuration;

namespace Aibo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await AiboBot.Start();
        }
    }
}