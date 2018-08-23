namespace _5051.Models
{
    /// <summary>
    /// Emotion Status
    /// </summary>
    public enum EmotionStatusEnum
    {
        VeryHappy = 5,
        Happy = 4,
        Neutral = 3,
        Sad = 2,
        VerySad = 1,
    }


    public static class Emotion
    {

        // Return the path to the emotion
        public static string GetEmotionURI(EmotionStatusEnum EmotionCurrent)
        {
            var myReturn = "";

            switch (EmotionCurrent)
            {
                case EmotionStatusEnum.VeryHappy:
                    myReturn = "EmotionVeryHappy.png";
                    break;
                case EmotionStatusEnum.Happy:
                    myReturn = "EmotionHappy.png";
                    break;
                case EmotionStatusEnum.Neutral:
                    myReturn = "EmotionNeutral.png";
                    break;
                case EmotionStatusEnum.Sad:
                    myReturn = "EmotionSad.png";
                    break;
                case EmotionStatusEnum.VerySad:
                    myReturn = "EmotionVerySad.png";
                    break;

                default:
                    myReturn = "placeholder.png";
                    break;
            }
            return "/content/img/" + myReturn;
        }
    }
}