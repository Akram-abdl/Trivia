using System;
using System.IO;
using System.Net;
using Tests.Utilities;
using Trivia;
using Xunit;

namespace Tests
{
    public class GoldenMasterTest
    {
        [Fact(Skip = "Golden Master")]
        public void Record()
        {
            var consoleSpy = new ConsoleSpy();
            var game = new Game(consoleSpy);

            var recordedContent = consoleSpy.Content;
            // get path to record.txt
            if (File.Exists(@"C:\temp\record.txt"))
            {
                File.WriteAllText(@"C:\temp\record.txt", recordedContent);
            }
            else
            {
                File.Create(@"C:\temp\record.txt");
                File.WriteAllText(@"C:\temp\record.txt", recordedContent);
            }
        }

        [Fact]
        public void Replay()
        {
            var consoleSpy = new ConsoleSpy();
            var game = new Game(consoleSpy);

            var recordedContent = consoleSpy.Content;
            string expectedContent = String.Empty;
            if (File.Exists(@"C:\temp\record.txt"))
            {
                expectedContent = File.ReadAllText(@"C:\temp\record.txt");
            }
            else
            {
                File.Create(@"C:\temp\record.txt"); 
                expectedContent = File.ReadAllText(@"C:\temp\record.txt");
            }

            Assert.Equal(expectedContent, recordedContent);
        }
    }
}
