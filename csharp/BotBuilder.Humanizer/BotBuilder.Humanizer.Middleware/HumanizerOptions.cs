namespace BotBuilder.Humanizer.Middleware
{
    /// <summary>
    /// Options for BotBuilder Humanizer middleware
    /// </summary>
    public struct HumanizerOptions
    {
        private double _misspelling;
        private double _wrongPerson;
        private double _transpositions;
        private double _typingSpeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanizerOptions"/> struct.
        /// </summary>
        /// <param name="misspellingRate">The misspelling rate.</param>
        /// <param name="wrongPersonRate">The wrong person rate.</param>
        /// <param name="transpositionFrequency">The transposition frequency.</param>
        /// <param name="typingSpeedWpm">The typing speed WPM.</param>
        public HumanizerOptions(double misspellingRate = .25, double wrongPersonRate = .001, double transpositionFrequency = .008, double typingSpeedWpm = 300)
        {
            _misspelling = misspellingRate;
            _wrongPerson = wrongPersonRate;
            _transpositions = transpositionFrequency;
            _typingSpeed = typingSpeedWpm;
        }

        /// <summary>
        /// Gets or sets the misspelling frequency.
        /// </summary>
        /// <value>
        /// A number between 0 &amp; 1 as a frequency of how often common words will be misspelled.
        /// </value>
        public double Misspelling { get => _misspelling; set => _misspelling = value; }
        /// <summary>
        /// Gets or sets the wrong person frequency.
        /// </summary>
        /// <value>
        /// A number between 0 &amp; 1 as a frequency of how often the bot will "mistakenly" send a message not intended for the current user
        /// </value>
        public double WrongPerson { get => _wrongPerson; set => _wrongPerson = value; }
        /// <summary>
        /// Gets or sets the transposition frequency.
        /// </summary>
        /// <value>
        /// A number between 0 &amp; 1 as a frequency of how often the bot will transpose characters in its response.
        /// </value>
        public double Transpositions { get => _transpositions; set => _transpositions = value; }
        /// <summary>
        /// Gets or sets the typing speed.
        /// </summary>
        /// <value>
        /// A value in Words Per Minute indicating how fast your bot "types"
        /// </value>
        public double TypingSpeed { get => _typingSpeed; set => _typingSpeed = value; }
    }
}
