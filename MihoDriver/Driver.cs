using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace MihoDriver
{
    public class Driver
    {
        private static string DriverPath;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public static async Task Setup(string path = null)
        {
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#if DEBUG
            DriverPath = path != null ? path : Path.GetFullPath(@"..\..\..\MihoDriver\miho-driver\build\miho_driver.exe");
#else
            DriverPath = path != null ? path : Path.GetTempPath() + @"MihoPCRemote\miho_driver.exe";

            var parentDirectory = Path.GetDirectoryName(DriverPath);
            if (parentDirectory != "")
            {
                await Task.Run(
                    () => Directory.CreateDirectory(parentDirectory)
                );
            }

            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "MihoDriver.miho_driver.build.miho_driver.exe";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(DriverPath))
            {
                int readCount = 0;
                char[] buffer = new char[4096];
                while ((readCount = await reader.ReadBlockAsync(buffer, 0, 4096)) > 0)
                {
                    await writer.WriteAsync(buffer, 0, readCount);
                }
            }
#endif
        }

        private Task startTask;
        private Task joinTask;
        private Process driverProcess;

        public Driver()
        {
            startTask = Task.Run(
                () =>
                {
                    driverProcess = Process.Start(new ProcessStartInfo()
                    {
                        FileName = DriverPath,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    });
                }
            );

            joinTask = startTask.ContinueWith((task) => driverProcess.WaitForExit());
        }

        public async Task WaitForStart()
        {
            await startTask;
        }

        public async Task WaitForStop()
        {
            await joinTask;
        }

        public void Stop()
        {
            Task.Run(() => driverProcess.Kill());
        }
    }
}
