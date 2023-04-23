using System;
using System.Collections.Generic;
using System.IO;
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
            var game = new GameRunner(consoleSpy);

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

        [Fact(Skip = "Golden Master")]
        public void Replay()
        {
            var consoleSpy = new ConsoleSpy();
            var game = new GameRunner(consoleSpy);
            
            List<Player>playersList = new List<Player> { new Player("Chet"), new Player("Pat"), new Player("Sue") };
            
            game.PlayAGame(playersList);

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
