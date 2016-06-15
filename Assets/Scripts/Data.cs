using System;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class mapping the content of a QR-code.
    /// </summary>
    [Serializable]
    public class Data
    {
        public int id;

        public DataType type;

        public string question;

        public string position;

        public string[] answers;

        public int correctAnswer;

        public string[] arrows;

         // relevant for particle systems
        //public string[] particleArrows;
        public string startColor;
        public string endColor;

        public Color StartColor
        {
            get
            {
                Color result = new Color();
                ColorUtility.TryParseHtmlString(startColor, out result);
                return result;
            }
        }

        public Color EndColor
        {
            get
            {
                Color result = new Color();
                ColorUtility.TryParseHtmlString(endColor, out result);
                return result;
            }
        }

        /// <summary>
        /// Map QR-code data to Question object.
        /// </summary>
        /// <returns>Question corresponding to QR-code data.</returns>
        public Question ToQuestion()
        {
            return new Question(id, question, answers, correctAnswer);
        }

        /// <summary>
        /// Map QR-code data to Position object.
        /// </summary>
        /// <returns>Position corresponding to QR-code data.</returns>
        public Position ToPosition()
        {
            return new Position(id, position, arrows);
        }

        /// <summary>
        /// Map QR-code data to Particle object.
        /// </summary>
        /// <returns>Particlesystem corresponding to QR-code data.</returns>
        public Particle ToParticle()
        {
            return new Particle(id, startColor, endColor);
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Type: {1}, Question: {2}, Answers: {3}, CorrectAnswer: {4}, StartColor: {5}, EndColor: {6}", id, type,
                question, answers, correctAnswer, StartColor, EndColor);
        }
    }
}