using ADODesigner.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.Cmd
{
    public static class WindAnimationCommand
    {
        public static void Run()
        {
            WindAnimationArgs animationArgs = new WindAnimationArgs();

            Directory.CreateDirectory("config");
            if (!File.Exists(@"config\wind_animation.json"))
            {
                File.WriteAllText(@"config\wind_animation.json", JsonSerializer.Serialize(animationArgs));
            }
            else
            {
                animationArgs = JsonSerializer.Deserialize<WindAnimationArgs>(File.ReadAllText(@"config\wind_animation.json"));
            }
        }
    }
}
