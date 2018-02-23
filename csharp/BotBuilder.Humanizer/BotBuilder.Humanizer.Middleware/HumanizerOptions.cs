namespace BotBuilder.Humanizer.Middleware
{
    public struct HumanizerOptions
    {
        private double _misspelling;
        private double _wrongPerson;
        private double _transpositions;
        private double _typingSpeed;

        public HumanizerOptions(double misspellingRate = .25, double wrongPersonRate = .001, double transpositionFrequency = .008, double typingSpeedWpm = 300)
        {
            _misspelling = misspellingRate;
            _wrongPerson = wrongPersonRate;
            _transpositions = transpositionFrequency;
            _typingSpeed = typingSpeedWpm;
        }

        public double Misspelling { get => _misspelling; set => _misspelling = value; }
        public double WrongPerson { get => _wrongPerson; set => _wrongPerson = value; }
        public double Transpositions { get => _transpositions; set => _transpositions = value; }
        public double TypingSpeed { get => _typingSpeed; set => _typingSpeed = value; }
    }
}
