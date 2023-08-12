using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Configuration;

namespace Aibo;

public class AiboBot
{
    public static async Task Start()
    {
        var discord = new DiscordClient(new DiscordConfiguration
        {
            Token = GetToken(),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.All,
            LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
        });

        discord.UseVoiceNext();

        discord.ComponentInteractionCreated += async (s, e) =>
        {
            await e.Interaction.CreateResponseAsync(
                InteractionResponseType.UpdateMessage,
                new DiscordInteractionResponseBuilder()
                    .WithContent("No more buttons for you >:)"));
        };

        AddMessageHandlers(discord);

        var commands = discord.UseCommandsNext(new CommandsNextConfiguration { StringPrefixes = new[] { "!" } });
        commands.RegisterCommands<AiboCommandsModule>();

        await discord.ConnectAsync();
        await Task.Delay(-1);
    }

    /// <summary>
    /// </summary>
    /// <param name="discord"></param>
    private static void AddMessageHandlers(DiscordClient discord)
    {
        discord.MessageCreated += async (s, e) =>
        {
            if (e.Message.Content.ToLower().StartsWith("aibo"))
                await e.Message.RespondAsync("Test");
            if (e.Message.Content.ToLower().StartsWith("file"))
            {
                var msg = new DiscordMessageBuilder()
                    .WithContent("Here is a really dumb file that I am testing with.");
                await using var fs = new FileStream("testfile.txt", FileMode.Open, FileAccess.Read);

                msg.AddFile("testfile.txt", fs);

                await msg.SendAsync(e.Channel);
            }

            if (e.Message.Content.ToLower().StartsWith("tts"))
            {
                var msg = new DiscordMessageBuilder()
                    .WithContent("Here is a really dumb file that I am testing with.");

                msg.IsTTS = true;

                await msg.SendAsync(e.Channel);
            }

            if (e.Message.Content.ToLower().StartsWith("ping"))
            {
                var msg = await new DiscordMessageBuilder()
                    .WithContent($"{e.Message.Author.Mention}")
                    .WithAllowedMentions(new IMention[] { new UserMention(e.Message.Author) })
                    .SendAsync(e.Message.Channel);
            }
        };
    }


    private static string GetToken()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        return config["Token"];
    }
}