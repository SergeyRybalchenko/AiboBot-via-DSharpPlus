using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Aibo;

public class AiboCommandsModule : BaseCommandModule
{
    [Command("button")]
    public async Task SendButton(CommandContext context)
    {
        var myButton = new DiscordButtonComponent(
            ButtonStyle.Primary,
            "my_very_cool_button",
            "Very cool button!",
            false,
            new DiscordComponentEmoji("😀"));

        var builder = new DiscordMessageBuilder();
        builder.WithContent("This message has buttons! Pretty neat innit?");
        builder.AddComponents(new DiscordComponent[]
        {
            new DiscordButtonComponent(ButtonStyle.Primary, "1_top", "Blurple!"),
            new DiscordButtonComponent(ButtonStyle.Secondary, "2_top", "Grey!"),
            new DiscordButtonComponent(ButtonStyle.Success, "3_top", "Green!"),
            new DiscordButtonComponent(ButtonStyle.Danger, "4_top", "Red!"),
            new DiscordLinkButtonComponent("https://some-super-cool.site", "Link!")
        });

        await builder.SendAsync(context.Channel);
    }
}