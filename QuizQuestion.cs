using System.Collections.Generic;

namespace game_quiz_demo_cs
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string CorrectChoice { get; set; }
        public List<string> Choices { get; set; }
    }
}
